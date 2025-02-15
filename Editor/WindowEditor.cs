using System;
using System.Collections.Generic;
using System.Linq;
using DredPack.UIWindow;
using DredPack.UIWindow.Tabs;
using UnityEditor;
using UnityEngine;

namespace DredPack.UIWindowEditor
{

    [CustomEditor(typeof(Window)), CanEditMultipleObjects]
    public class WindowEditor : Editor
    {
        private static int currentWindowTab;
        private TabDrawer currentWindowTabDrawer;
        
        public Window T
        {
            get
            {
                if (_t == null)
                    _t = (Window)target;
                return _t;
            }
        }
        
        public Window[] Ts
        {
            get
            {
                Window[] targs = new Window[targets.Length];
                for (var i = 0; i < targets.Length; i++)
                {
                    targs[i] = (Window)targets[i];
                }
                return targs;
            }
        }
        private Window _t;
        

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            
            GUILayout.BeginVertical();
            
            GUIStyle myStyle = new GUIStyle();
            myStyle.fontSize = 25;
            myStyle.fontStyle = FontStyle.Bold;
            myStyle.normal.textColor =  Color.white;
            GUILayout.Label("Window - v4", myStyle);
            
            GUILayout.EndVertical();
            
            
            EditorGUILayout.BeginVertical(GUI.skin.box);
            try
            {
                Tabs();
                currentWindowTabDrawer.Draw();
            }
            catch (Exception e)
            {
                GUILayout.Label("Can't draw the tab");
                Debug.LogException(e);
            }
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        
        private void Tabs()
        {
            GUIContent[] toolbarTabs = new GUIContent[3]
                { new GUIContent("General"), new GUIContent("Events"), new GUIContent("Animation") };
            
            var curTab = GUILayout.Toolbar(currentWindowTab, toolbarTabs);
            
            if (curTab != currentWindowTab || currentWindowTabDrawer == null)
            {
                currentWindowTab = curTab;
                
                CreateNewTabDrawer();
            }
            
        }

        private void CreateNewTabDrawer()
        {
            currentWindowTabDrawer = null;
            switch (currentWindowTab)
            {
                case 0:
                    currentWindowTabDrawer = new GeneralTabDrawer(this,serializedObject.FindProperty("General"));
                    break;
                case 1: 
                    currentWindowTabDrawer = new EventsTabDrawer(this,serializedObject.FindProperty("Events"));
                    break;
                case 2:
                    currentWindowTabDrawer = new AnimationTabDrawer(this,serializedObject.FindProperty("Animation"));
                    break;
            }
        }

    }
}