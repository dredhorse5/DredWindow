﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DredPack.UIWindowEditor
{
    public class AnimationTabDrawer : TabDrawer
    {
 
        int _selectedOpen = 0;
        int _selectedClose = 0;
        int selectedDualMode = 0;


        public AnimationTabDrawer(WindowEditor window, SerializedProperty tabProperty) : base(window, tabProperty)
        {
            _selectedOpen = UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.IndexOf(window.T.Animation.CurrentOpenAnimationName);
            _selectedClose = UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.IndexOf(window.T.Animation.CurrentCloseAnimationName);
        }

        public override void Draw()
        {
            base.Draw();
            var dualModeProp = tabProperty.FindPropertyRelative("DualMode");
            EditorGUILayout.PropertyField(dualModeProp);
            if (dualModeProp.boolValue)
            {
                selectedDualMode = GUILayout.Toolbar(selectedDualMode, new string[]{"Open","Close"});
                EditorGUILayout.Space();
            }
            if(!dualModeProp.boolValue)
                DrawSingleMode();
            else
                DrawDualMode();
        }

        private void DrawSingleMode()
        {
            var sel = EditorGUILayout.Popup("Animation Type", _selectedOpen, UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.ToArray());
 
            if (sel != _selectedOpen)
            {
                _selectedOpen = sel;
                window.T.Animation.CurrentOpenAnimationName = UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames[_selectedOpen];
                EditorUtility.SetDirty(window.T);
            }
            
            var labelStyle = new GUIStyle();
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontSize = 15;
            GUILayout.Label(window.T.Animation.CurrentOpenAnimationName,labelStyle);
            var anim = window.T.Animation.CurrentOpenAnimation;
            EditorGUILayout.Space(-20);
            SerializedProperty findPropertyRelative = tabProperty.FindPropertyRelative("_currentOpenAnimation");
            EditorGUILayout.PropertyField(findPropertyRelative,true);
            
        }

        private void DrawDualMode()
        {
            if(selectedDualMode == 0)
            {
                var sel = EditorGUILayout.Popup("Animation Open Type", _selectedOpen,
                    UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.ToArray());

                if (sel != _selectedOpen)
                {
                    _selectedOpen = sel;
                    window.T.Animation.CurrentOpenAnimationName =
                        UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames[_selectedOpen];
                    EditorUtility.SetDirty(window.T);
                }
            }
            else
            {
                var sel = EditorGUILayout.Popup("Animation Close Type", _selectedClose,
                    UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.ToArray());

                if (sel != _selectedClose)
                {
                    _selectedClose = sel;
                    window.T.Animation.CurrentCloseAnimationName =
                        UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames[_selectedClose];
                    EditorUtility.SetDirty(window.T);
                }
            }
            
            var labelStyle = new GUIStyle();
            labelStyle.fontStyle = FontStyle.Bold;
            labelStyle.normal.textColor = Color.white;
            labelStyle.fontSize = 15;
            GUILayout.Label(selectedDualMode == 1 ? window.T.Animation.CurrentCloseAnimationName : window.T.Animation.CurrentOpenAnimationName,labelStyle);
            var anim = window.T.Animation.CurrentOpenAnimation;
            var anim1 = window.T.Animation.CurrentCloseAnimation;
            EditorGUILayout.Space(-20); 
            SerializedProperty findPropertyRelative = tabProperty.FindPropertyRelative(selectedDualMode == 1 ? "_currentCloseAnimation": "_currentOpenAnimation");
            EditorGUILayout.PropertyField(findPropertyRelative,true);
        }
    }
}