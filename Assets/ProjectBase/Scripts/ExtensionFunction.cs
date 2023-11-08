using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using HighlightPlus;
using HomeVisit.UI;
using QFramework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ProjectBase
{
    public static class ExtensionFunction
    {
        public static float HideTime = 0.5f;
        public static float ShowTime = 0.5f;

        #region UI扩展

        static void LoadUIAsync(PanelSearchKeys panelSearchKeys, Action<IPanel> onLoad)
        {
            var retPanel = UIKit.Table.GetPanelsByPanelSearchKeys(panelSearchKeys).FirstOrDefault();

            if (retPanel == null)
            {
                UIManager.Instance.CreateUIAsync(panelSearchKeys, (panel) =>
                {
                    retPanel = panel;
                    onLoad?.Invoke(retPanel);
                });
            }
        }

        /// <summary>
        ///  预加载UI，不会触发OnShow和OnInit
        /// </summary>
        /// <param name="canvasLevel"></param>
        /// <param name="uiData"></param>
        /// <param name="assetBundleName"></param>
        /// <param name="prefabName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerator PreLoadPanelAsync<T>(UILevel canvasLevel = UILevel.Common, IUIData uiData = null,
            string assetBundleName = null,
            string prefabName = null) where T : UIPanel
        {
            var panelSearchKeys = PanelSearchKeys.Allocate();
            panelSearchKeys.OpenType = PanelOpenType.Single;
            panelSearchKeys.Level = canvasLevel;
            panelSearchKeys.PanelType = typeof(T);
            panelSearchKeys.AssetBundleName = assetBundleName;
            panelSearchKeys.GameObjName = prefabName;
            panelSearchKeys.UIData = null;
            bool loaded = false;

            LoadUIAsync(panelSearchKeys, panel => { loaded = true; });

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            while (!loaded)
            {
                yield return wait;
            }

            panelSearchKeys.Recycle2Cache();
        }

        public static Func<bool> GetAnimatorEndFunc(this Button btn)
        {
            Animator animator = btn.animator;
            return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
        }

        public static Func<bool> GetAnimatorEndFunc(this Toggle tog)
        {
            Animator animator = tog.animator;
            return () => animator.GetCurrentAnimatorStateInfo(0).IsName("Normal");
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Button btn, Func<UniTask> invoke)
        {
            CancellationToken token = btn.GetCancellationTokenOnDestroy();
            UnityAction asyncAction = async () =>
            {
                if (token.IsCancellationRequested) return;
                UIRoot.Instance.GraphicRaycaster.enabled = false;
                await btn.animator.GetAsyncAnimatorMoveTrigger().FirstAsync(btn.GetCancellationTokenOnDestroy());
                await invoke();
                UIRoot.Instance.GraphicRaycaster.enabled = true;
            };
            btn.onClick.AddListener(asyncAction);
        }

        /// <summary>
        /// 执行异步函数过程中会屏蔽所有UI交互
        /// </summary>
        /// <param name="tog"></param>
        /// <param name="invoke"></param>
        public static void AddAwaitAction(this Toggle tog, Func<bool, UniTask> invoke)
        {
            Func<bool> animFunc = tog.GetAnimatorEndFunc();
            CancellationToken token = tog.GetCancellationTokenOnDestroy();
            UnityAction<bool> asyncAction = null;
            //有group的情况下，会同时触发两个toggle，因此屏蔽由isOn的Toggle管理。
            if (tog.group != null && !tog.group.allowSwitchOff)
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await UniTask.WaitUntil(animFunc);
                    await invoke(isOn);
                    if (isOn)
                        UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }
            else
            {
                asyncAction = async isOn =>
                {
                    if (token.IsCancellationRequested) return;
                    UIRoot.Instance.GraphicRaycaster.enabled = false;
                    await UniTask.WaitUntil(animFunc);
                    await invoke(isOn);
                    UIRoot.Instance.GraphicRaycaster.enabled = true;
                };
            }

            tog.onValueChanged.AddListener(asyncAction);
        }

        public static async UniTask ShowAwaitPanel(this UIPanel panel)
        {
            UIRoot.Instance.GraphicRaycaster.enabled = false;
            panel.Show();
            await panel.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
            UIRoot.Instance.GraphicRaycaster.enabled = true;
        }

        public static async UniTask ShowAsyncPanel(this UIPanel panel)
        {
            panel.Show();
            await panel.transform.DOLocalMoveY(0, ShowTime).AsyncWaitForCompletion();
        }

        public static async UniTask HideAsyncPanel(this UIPanel panel)
        {
            await panel.transform.DOLocalMoveY(1080, ShowTime).AsyncWaitForCompletion();
            panel.Hide();
        }

        #endregion

        #region 3D物体扩展

        public static async UniTask HightlightClickAsync(this GameObject obj, CancellationToken cancellationToken)
        {
            HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
            if (highlightEffect == null)
                highlightEffect = obj.AddComponent<HighlightEffect>();
            highlightEffect.highlighted = true;
            highlightEffect.outlineColor = Color.red;

            await obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(cancellationToken);
            highlightEffect.highlighted = false;
        }

        public static async UniTask HightlightClickAsync(this List<GameObject> objs,
            CancellationToken cancellationToken, Action<HighlightEffect> callBack = null)
        {
            List<HighlightEffect> objsHighlight = new List<HighlightEffect>();
            int count = objs.Count;
            for (int i = 0; i < objs.Count; i++)
            {
                GameObject obj = objs[i];
                HighlightEffect highlightEffect = obj.GetComponent<HighlightEffect>();
                if (highlightEffect == null)
                    highlightEffect = obj.AddComponent<HighlightEffect>();
                highlightEffect.highlighted = true;
                highlightEffect.outlineColor = Color.red;
                objsHighlight.Add(highlightEffect);
                obj.GetAsyncPointerClickTrigger().FirstOrDefaultAsync(d =>
                {
                    highlightEffect.highlighted = false;
                    count--;
                    callBack?.Invoke(highlightEffect);
                    return true;
                }, cancellationToken).Forget();
            }

            await UniTask.WaitUntil(() => count == 0, cancellationToken: cancellationToken);
        }

        #endregion
    }
}