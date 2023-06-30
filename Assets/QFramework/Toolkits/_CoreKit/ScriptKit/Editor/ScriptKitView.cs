#if UNITY_EDITOR
using System;
using MoonSharp.Interpreter;
using UnityEditor;
using UnityEngine;

namespace QFramework
{
    [PackageKitGroup("QFramework.Alpha")]
    [DisplayNameCN("ScriptKit")]
    [DisplayNameEN("ScriptKit")]
    public class ScriptKitView : IPackageKitView
    {
        
        [MoonSharpUserData]
        class MyClass
        {
            public double calcHypotenuse(double a, double b)
            {
                return Math.Sqrt(a * a + b * b);
            }
        }

        
        public EditorWindow EditorWindow { get; set; }
        private Script mScript;
        

        public void Init()
        {

        }

        public void OnUpdate()
        {
        }

        private string mScriptText = "print \"Hello ScriptKit(Lua)!\"\nprint(MyClass:calcHypotenuse(10,20))";
        private string mOutput = string.Empty;

        public void OnGUI()
        {
            mScriptText = EditorGUILayout.TextArea(mScriptText, GUILayout.MinHeight(50));

            if (GUILayout.Button("Run"))
            {
                mScript.DoString(mScriptText);
            }

            GUILayout.Label(mOutput);
        }

        public void OnWindowGUIEnd()
        {
        }

        public void OnDispose()
        {
        }

        public void OnShow()
        {
            mScript = new Script();
            UserData.RegisterAssembly(typeof(ScriptKitView).Assembly);
            UserData.RegisterType<Application>();
            // UserData.RegisterType<EditorApplication>();
            mScript.Globals["MyClass"] = new MyClass();
            mScript.Globals["Application"] = typeof(Application);
            // mScript.Globals["EditorApplication"] = typeof(EditorApplication);
            mScript.Options.DebugPrint = s => { mOutput = mOutput + s + "\n"; };
        }

        public void OnHide()
        {
            mScript = null;
        }
    }
}
#endif