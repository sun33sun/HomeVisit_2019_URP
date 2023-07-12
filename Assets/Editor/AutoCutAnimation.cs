using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class AutoCutAnimation : Editor
{
    //[MenuItem("Assets/切割动画")]
    //public static void CutAnimation()
    //{
    //    UnityEngine.Object[] objs = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);

    //    ReadConfig(AssetDatabase.GetAssetPath(objs[0]), ReadConfigCallback);
    //}

    //private static void ReadConfigCallback(string filePath, List<ConfigItem> obj)
    //{
    //    CutAnimation(filePath, obj);
    //}
    ///// <summary>
    ///// 读取配置文件
    ///// </summary>
    ///// <param name="path"></param>
    ///// <param name="callback"></param>
    //private static void ReadConfig(string path, Action<string, List<ConfigItem>> callback)
    //{
    //    var tempPath = path.Split('.');
    //    var filePath = tempPath[0] + ".json";

    //    if (File.Exists(filePath))
    //    {
    //        string json = File.ReadAllText(filePath);

    //        if (json != null && json != "")
    //        {
				//List<ConfigItem> list = JsonConvert.DeserializeObject<List<ConfigItem>>(json);

				//callback?.Invoke(path, list);
    //        }
    //    }
    //}

    ///// <summary>
    ///// 切割动画
    ///// </summary>
    ///// <param name="list"></param>
    //private static void CutAnimation(string path, List<ConfigItem> list)
    //{
    //    ModelImporter model = AssetImporter.GetAtPath(path) as ModelImporter;

    //    List<ModelImporterClipAnimation> clipAnimations = new List<ModelImporterClipAnimation>();

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        ModelImporterClipAnimation clip = new ModelImporterClipAnimation();

    //        clip.name = list[i].m_Name;
    //        clip.firstFrame = list[i].m_StartFrame;
    //        clip.lastFrame = list[i].m_EndFrame;
    //        clip.loopPose = false;
    //        clipAnimations.Add(clip);
    //        Debug.Log("动画 " + clip.name + " 切割完成");
    //    }

    //    model.clipAnimations = clipAnimations.ToArray();

    //    model.SaveAndReimport();

    //    AssetDatabase.ImportAsset(path);

    //    AssetDatabase.Refresh();

    //    Debug.Log(list.Count + "条动画切割完成");
    //}

    [MenuItem("Assets/提取动画")]
    private static void ExtractFbxAnimationClips()
    {
        GameObject[] selectedObjects = Selection.gameObjects;
        foreach (GameObject obj in selectedObjects)
        {
            var subAssets = AssetDatabase.LoadAllAssetRepresentationsAtPath(AssetDatabase.GetAssetPath(obj));
            var outputDir = Path.GetDirectoryName(AssetDatabase.GetAssetPath(obj));
            foreach (var item in subAssets)
            {
                if (item is AnimationClip animClip)
                {
                    AnimationClip newClip = new AnimationClip();
                    EditorUtility.CopySerialized(animClip, newClip);
                    string animFile = string.Format("{0}/{1}.anim", outputDir, animClip.name);
                    AssetDatabase.CreateAsset(newClip, animFile);
                }
            }
        }
        AssetDatabase.Refresh();
    }
}
public class ConfigItem
{
    public string m_Name;
    public float m_StartFrame;
    public float m_EndFrame;
}
