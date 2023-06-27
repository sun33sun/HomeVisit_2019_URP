using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZTools
{
    public class ZCalendarModel : MonoBehaviour
    {
        [Header("根据当前时间自动初始化")]
        public bool awake2Init = true;
        [Header("自动补充前后月份的日期")]
        public bool autoFillDate = true;
        [Header("超过当前时间是否可以点击")]
        public bool isUnexpiredTimeCanClick = true;
        [Header("显示农历日期")]
        public bool lunar = true;
        [Header("如果为true，本对象显示状态不能关闭，可通过子集bak的显示状态控制默认状态")]
        [Header("当前是否为弹窗日历")]
        public bool isPopupCalendar = false;
        [Header("当前是否为静态日历")]
        public bool isStaticCalendar = false;
        [Header("自动修改日期尺寸")]
        public bool autoSetItemSize = true;
        [Header("是否可以选择时间范围")]
        public bool rangeCalendar = false;
        [Header("--------------------------------------------------------------------")]
        public GameObject bak;
        public Button btnLastYear;
        public Button btnNextYear;
        public Button btnLastMonth;
        public Button btnNextMonth;
        public Text txtYear;
        public Text txtMonth;
        public Transform dayContent;
        public ZCalendarDayItem dayItem;
        [HideInInspector]
        public Button btnClose;
        [HideInInspector]
        public ZCalendarController zCalendarController;
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            if (!bak.activeInHierarchy)
            {
                bak.SetActive(true);
                this.GetComponent<ZCalendar>().Hide();
            }
            if (autoSetItemSize)
            {
                SetItemSize();
            }
            if (isPopupCalendar)
            {
                AddCloseBtn();
            }
            if (isStaticCalendar)
            {
                btnLastYear.gameObject.SetActive(false);
                btnNextYear.gameObject.SetActive(false);
                btnLastMonth.gameObject.SetActive(false);
                btnNextMonth.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 生成一个日期对象
        /// </summary>
        /// <returns></returns>
        public ZCalendarDayItem Instantiate()
        {
            return Instantiate(dayItem, dayContent);
        }
        /// <summary>
        /// 根据日历尺寸，设置宽高
        /// </summary>
        public void SetItemSize()
        {
            Vector2 bakSize = this.GetComponent<RectTransform>().sizeDelta;
            Vector2 dayContentSize = dayContent.GetComponent<RectTransform>().sizeDelta;
            //Debug.Log(bakSize.x +":::"+ dayContentSize.y);
            GridLayoutGroup layoutGroup = dayContent.GetComponent<GridLayoutGroup>();
            float itemSizeWidth = (bakSize.x - layoutGroup.spacing.x * layoutGroup.constraintCount - layoutGroup.padding.left - layoutGroup.padding.right) / layoutGroup.constraintCount;
            float itemSizeHeight = (bakSize.y - Mathf.Abs(dayContentSize.y) - layoutGroup.padding.top - layoutGroup.padding.bottom - layoutGroup.spacing.y * 6) / 6;
            dayContent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(itemSizeWidth, itemSizeHeight);
        }
        /// <summary>
        /// 添加空白处可关闭功能
        /// </summary>
        public void AddCloseBtn()
        {
            GameObject btnCloseObj = new GameObject();
            RectTransform btnCloseRect = btnCloseObj.AddComponent<RectTransform>();
            btnCloseObj.transform.SetParent(transform);
            btnCloseObj.transform.SetAsFirstSibling();
            btnCloseRect.sizeDelta = new Vector2(Screen.width, Screen.height);
            btnCloseObj.transform.position = Vector3.zero + new Vector3(Screen.width / 2, Screen.height / 2, 0);
            btnCloseObj.AddComponent<Image>().color = new Color(0,0,0,0);
            this.btnClose = btnCloseObj.AddComponent<Button>();
        }
        /// <summary>
        /// 设置年月文字
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        public void SetTimeTxt(int year, int month)
        {
            txtYear.text = year + "年";
            txtMonth.text = month + "月";
        }
    }
}
