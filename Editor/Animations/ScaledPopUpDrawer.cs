using DredPack.UIWindow.Animations;
using UnityEditor;
using UnityEngine;

namespace DredPack.UIWindowEditor.Animations
{
    [CustomPropertyDrawer(typeof(ScaledPopUp))]
    public class ScaledPopUpDrawer : WindowAnimationDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawDefaultFields(position, property,label);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("ScaleOpenCurve"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("ScaleCloseCurve"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("ScaledObject"));
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Alpha", EditorStyles.boldLabel);
            EditorGUILayout.CurveField(property.FindPropertyRelative("AlphaOpenCurve"), Color.green, new Rect(0,0,1,1));
            EditorGUILayout.CurveField(property.FindPropertyRelative("AlphaCloseCurve"), Color.red, new Rect(0,0,1,1));

            var alphaTypeProp = property.FindPropertyRelative("AlphaType");
            EditorGUILayout.PropertyField(alphaTypeProp);
            if(alphaTypeProp.enumValueIndex == 1)
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Image"));
            else if(alphaTypeProp.enumValueIndex == 2)
                EditorGUILayout.PropertyField(property.FindPropertyRelative("GraphicModule"));
        }
    }
}