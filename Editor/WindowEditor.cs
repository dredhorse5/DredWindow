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
        [SerializeReference] private TabDrawer[] tabs;
        
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
        public void InitTabsDrawers()
        {
            tabs = new TabDrawer[T.AllTabs.Count];
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            foreach (var ass in assemblies)
                types.AddRange(ass.GetTypes());
            List<Type> tabDrawsTypes = new List<Type>();
            foreach (var type in types)
            {
                if(type.IsSubclassOf(typeof(TabDrawer)))
                    tabDrawsTypes.Add(type);
            }
            //types.Where(t => t.IsSubclassOf(typeof(TabDrawer))).ToList(); 
            
            TabDrawer[] rawTabsDraws = new TabDrawer[tabDrawsTypes.Count];
            for (var i = 0; i < tabDrawsTypes.Count; i++)
            {
                rawTabsDraws[i] = (TabDrawer)Activator.CreateInstance(tabDrawsTypes[i]);
                int index = -1;
                for (var j = 0; j < T.AllTabs.Count; j++)
                {
                    if (rawTabsDraws[i].DrawerOfTab == T.AllTabs[j].GetType())
                    {
                        index = j;
                        break;
                    }
                }
                if(index > -1)
                    rawTabsDraws[i].Init(this,serializedObject.FindProperty(nameof(T.AllTabs)).GetArrayElementAtIndex(index));
            }

            
            for (int i = 0; i < T.AllTabs.Count; i++)
            {
                for (int j = 0; j < rawTabsDraws.Length; j++)
                {
                    var memberInfo = T.AllTabs[i].GetType();
                    
                    if (rawTabsDraws[j].DrawerOfTab == memberInfo)
                    {
                        tabs[i] = rawTabsDraws[j];
                        break;
                    }
                }
            }
        }
        private void OnEnable()
        { 
            T.FindAllTabs();
            InitTabsDrawers();
        }

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
            Tabs();
            if(T.AllTabs == null || T.AllTabs.Count == 0)
                return;
            EditorGUILayout.BeginVertical(GUI.skin.box);
            
            if(tabs[currentWindowTab] != null)
            {
                tabs[currentWindowTab].Draw();
            }
            else
            {
                var property = serializedObject.FindProperty(nameof(T.AllTabs))
                    .GetArrayElementAtIndex(currentWindowTab);
                foreach (SerializedProperty o in property)
                    EditorGUILayout.PropertyField(o, true);
            }
            
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }

        
        private void Tabs()
        {
            if(T.AllTabs == null || T.AllTabs.Count == 0)
                return;
            GUIContent[] toolbarTabs = new GUIContent[T.AllTabs.Count];
            for (var i = 0; i < T.AllTabs.Count; i++)
                toolbarTabs[i] = new GUIContent(T.AllTabs[i].TabName);
            
            currentWindowTab = GUILayout.Toolbar(currentWindowTab, toolbarTabs);
        }

    }
}