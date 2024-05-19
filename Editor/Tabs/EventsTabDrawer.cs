using System;
using DredPack.UIWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DredPack.UIWindowEditor
{
    public class EventsTabDrawer : TabDrawer
    {
        private SerializedProperty startOpenProperty;
        private SerializedProperty startCloseProperty;
        private SerializedProperty startSwitchProperty;
        private SerializedProperty endOpenProperty;
        private SerializedProperty endCloseProperty;
        private SerializedProperty endSwitchProperty;
        private SerializedProperty stateChangedProperty;
        public override Type DrawerOfTab => typeof(DredPack.UIWindow.Tabs.EventsTab);

        
        public override void Init(WindowEditor window, SerializedProperty tabProperty)
        {
            base.Init(window, tabProperty);
            startOpenProperty = tabProperty.FindPropertyRelative("StartOpen");
            startCloseProperty = tabProperty.FindPropertyRelative("StartClose");
            startSwitchProperty = tabProperty.FindPropertyRelative("StartSwitch");
            endOpenProperty = tabProperty.FindPropertyRelative("EndOpen");
            endCloseProperty = tabProperty.FindPropertyRelative("EndClose");
            endSwitchProperty = tabProperty.FindPropertyRelative("EndSwitch");
            stateChangedProperty = tabProperty.FindPropertyRelative("StateChanged");
        }

        public override void Draw()
        {
            base.Draw();

            Label(" Start");
            EditorGUILayout.PropertyField(startOpenProperty, true);
            EditorGUILayout.PropertyField(startCloseProperty, true);
            EditorGUILayout.PropertyField(startSwitchProperty, true);
            Label(" End");
            EditorGUILayout.PropertyField(endOpenProperty, true);
            EditorGUILayout.PropertyField(endCloseProperty, true);
            EditorGUILayout.PropertyField(endSwitchProperty, true);
            Label(" State Changed");
            EditorGUILayout.PropertyField(stateChangedProperty, true);
        }

        private void Label(string _name)
        {
            
            var labelStyle = new GUIStyle();
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontSize = 15;
            GUILayout.Label(_name,labelStyle);
        }
    }
}