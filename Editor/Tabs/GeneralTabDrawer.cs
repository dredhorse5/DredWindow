﻿using System;
using DredPack.UIWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DredPack.UIWindowEditor
{
    public class GeneralTabDrawer : TabDrawer
    {
        private SerializedProperty currentStateProperty;
        private SerializedProperty stateOnAwakeMethodProperty;
        private SerializedProperty stateOnAwakeProperty;
        private SerializedProperty animationOnAwakeProperty;
        private SerializedProperty closeButtonProperty;
        private SerializedProperty openButtonProperty;
        private SerializedProperty switchButtonProperty;
        private SerializedProperty disableableProperty;
        private SerializedProperty disableableObjectProperty;
        private SerializedProperty enableableCanvasProperty;
        private SerializedProperty canvasProperty;
        private SerializedProperty enableableRaycasterProperty;
        private SerializedProperty enableableCanvasGroupInteractable;
        private SerializedProperty enableableCanvasGroupRaycasts;
        private SerializedProperty raycasterProperty;
        private SerializedProperty closeIfAnyWindowOpenProperty;
        private SerializedProperty closeIfAnyWindowOpenTypeProperty;
        private SerializedProperty closeOnOutsideClickProperty; 
        private SerializedProperty autoCloseProperty; 
        private SerializedProperty autoCloseDelayProperty; 
        private SerializedProperty selectObjectOnOpenProperty; 
        private SerializedProperty selectableOnOpenProperty;


        public GeneralTabDrawer(WindowEditor window, SerializedProperty tabProperty) : base(window,tabProperty)
        {
            currentStateProperty = tabProperty.FindPropertyRelative("CurrentState");
            stateOnAwakeMethodProperty = tabProperty.FindPropertyRelative("StateOnAwakeMethod");
            stateOnAwakeProperty = tabProperty.FindPropertyRelative("StateOnAwake");
            animationOnAwakeProperty = tabProperty.FindPropertyRelative("AnimationOnAwake");
            closeButtonProperty = tabProperty.FindPropertyRelative("CloseButton");
            openButtonProperty = tabProperty.FindPropertyRelative("OpenButton");
            switchButtonProperty = tabProperty.FindPropertyRelative("SwitchButton");
            disableableProperty = tabProperty.FindPropertyRelative("Disableable");
            disableableObjectProperty = componentsProperty.FindPropertyRelative("DisableableObject");
            enableableCanvasProperty = tabProperty.FindPropertyRelative("EnableableCanvas");
            canvasProperty = componentsProperty.FindPropertyRelative("Canvas");
            enableableRaycasterProperty = tabProperty.FindPropertyRelative("EnableableRaycaster");
            enableableCanvasGroupInteractable = tabProperty.FindPropertyRelative("EnableableCanvasGroupInteractable");
            enableableCanvasGroupRaycasts = tabProperty.FindPropertyRelative("EnableableCanvasGroupRaycasts");
            raycasterProperty = componentsProperty.FindPropertyRelative("Raycaster");
            closeIfAnyWindowOpenProperty = tabProperty.FindPropertyRelative("CloseIfAnyWindowOpen");
            closeIfAnyWindowOpenTypeProperty = tabProperty.FindPropertyRelative("CloseIfAnyWindowOpenType");
            closeOnOutsideClickProperty = tabProperty.FindPropertyRelative("CloseOnOutsideClick");
            autoCloseProperty = tabProperty.FindPropertyRelative("AutoClose");
            autoCloseDelayProperty = tabProperty.FindPropertyRelative("AutoCloseDelay");
            selectObjectOnOpenProperty = tabProperty.FindPropertyRelative("SelectObjectOnOpen");
            selectableOnOpenProperty = componentsProperty.FindPropertyRelative("SelectableOnOpen");
        }

        public override void Draw()
        {
            base.Draw();

            Label(" States");

            EditorGUILayout.LabelField("Current", currentStateProperty.enumDisplayNames[currentStateProperty.enumValueIndex]);

            EditorGUILayout.PropertyField(stateOnAwakeMethodProperty, new GUIContent("Awake Method"), true);

            if (window.T.General.StateOnAwakeMethod != StatesAwakeMethod.Nothing)
            {
                EditorGUI.indentLevel++;
                string propName = "OnAwake";
                switch (window.T.General.StateOnAwakeMethod)
                {
                    case StatesAwakeMethod.Awake:
                        propName = "On Awake";
                        break;
                    case StatesAwakeMethod.Start:
                        propName = "On Start";
                        break;
                    case StatesAwakeMethod.OnEnable:
                        propName = "OnEnable";
                        break;
                }
                EditorGUILayout.PropertyField(stateOnAwakeProperty, new GUIContent("Set State " + propName), true);
                
                var nowVal = UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.IndexOf(animationOnAwakeProperty.stringValue);
                var nextVal = EditorGUILayout.Popup("Animation", nowVal, UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames.ToArray());

                if (nowVal != nextVal || (nowVal == -1 || nextVal == -1))
                {
                    animationOnAwakeProperty.stringValue = UIWindow.Tabs.AnimationTab.RegisteredAnimationsNames[nextVal > -1 ? nextVal : 0];
                    EditorUtility.SetDirty(window.T);
                }
                EditorGUI.indentLevel--;
            }
            if (GUILayout.Button("Switch State (alt + shift + Q)"))
                window.T.Switch("Instantly");
            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Switch with animation"))
                window.T.Switch();
            GUI.enabled = true;


            Label(" Buttons");
            EditorGUILayout.PropertyField(closeButtonProperty, true);
            EditorGUILayout.PropertyField(openButtonProperty, true);
            EditorGUILayout.PropertyField(switchButtonProperty, true);

            
            Label(" Some");
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(selectObjectOnOpenProperty);
            if (window.T.General.SelectObjectOnOpen)
            {
                EditorGUILayout.PropertyField(selectableOnOpenProperty, GUIContent.none);
                if (GUILayout.Button("Find"))
                    window.T.Components.SelectableOnOpen = window.T.gameObject.GetComponentInChildren<Selectable>();
            }
            EditorGUILayout.EndHorizontal();
            
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(autoCloseProperty);
            if (window.T.General.AutoClose)
            {
                EditorGUILayout.LabelField("After Open Delay", GUILayout.MaxWidth(120));
                EditorGUILayout.PropertyField(autoCloseDelayProperty, GUIContent.none);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(disableableProperty, true);
            if (window.T.General.Disableable) EditorGUILayout.PropertyField(disableableObjectProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(enableableCanvasProperty, new GUIContent("Enableable Canvas"));
            if (window.T.General.EnableableCanvas) EditorGUILayout.PropertyField(canvasProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(enableableRaycasterProperty, new GUIContent("Enableable Raycaster"));
            if (window.T.General.EnableableRaycaster) EditorGUILayout.PropertyField(raycasterProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();
            
            
            EditorGUILayout.BeginHorizontal();
            


            EditorGUILayout.LabelField("");
            var rect = GUILayoutUtility.GetLastRect();
            var dataRect = EditorGUI.PrefixLabel(rect, new GUIContent("CanvasGroup Control :"));
            var leftRect  =new Rect(dataRect.x, dataRect.y, dataRect.width / 2f + 7f, dataRect.height);
            var rightRect = new Rect(dataRect.x + dataRect.width / 2f, dataRect.y, dataRect.width / 2f, dataRect.height);
            GUI.Box(dataRect,GUIContent.none,GUI.skin.box);
            GUI.Box(dataRect,GUIContent.none,GUI.skin.box);
            EditorGUI.LabelField(leftRect,"Interactable", EditorStyles.boldLabel);
            EditorGUI.LabelField(rightRect,"Raycasts", EditorStyles.boldLabel);
            var toggle1Rect = new Rect(leftRect.x + 76, leftRect.y, leftRect.width - 76, leftRect.height);
            var toggle2Rect = new Rect(rightRect.x + 60, rightRect.y, rightRect.width - 60, rightRect.height);
            EditorGUI.PropertyField(toggle1Rect, enableableCanvasGroupInteractable, GUIContent.none);
            EditorGUI.PropertyField(toggle2Rect, enableableCanvasGroupRaycasts, GUIContent.none);
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(closeIfAnyWindowOpenProperty, new GUIContent("Close If Any Window Open"), true);
            if (window.T.General.CloseIfAnyWindowOpen) EditorGUILayout.PropertyField(closeIfAnyWindowOpenTypeProperty, GUIContent.none);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(closeOnOutsideClickProperty, true);
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
