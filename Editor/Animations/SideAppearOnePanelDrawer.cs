using DredPack.UIWindow.Animations;
using UnityEditor;
using UnityEngine;

namespace DredPack.UIWindowEditor.Animations
{
    [CustomPropertyDrawer(typeof(SideAppearOnePanel))]
    public class SideAppearOnePanelDrawer : WindowAnimationDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawDefaultFields(position, property,label);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("OpenCurve"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("CloseCurve"));
            
            EditorGUILayout.PropertyField(property.FindPropertyRelative("OpenDirection"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("CloseDirection"));
            var boolVal = property.FindPropertyRelative("CustomReferenceTransform");
            EditorGUILayout.PropertyField(boolVal);
            if(boolVal.boolValue)
                EditorGUILayout.PropertyField(property.FindPropertyRelative("ReferenceTransform"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Panel"));
            
            EditorGUILayout.PropertyField(property.FindPropertyRelative("Graphics"));
            
        }
    }
}