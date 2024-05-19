using DredPack.UIWindow;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DredPack.UIWindowEditor
{
    public class MenuEditor : MonoBehaviour
    {
        
        [MenuItem("GameObject/DredPackUI/Window", false, 10)]
        public static void CreateWindow(MenuCommand menuCommand)
        {
            var rectTransform = CreateUiElement("Window");


            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            rectTransform.sizeDelta = Vector2.zero;

            rectTransform.anchoredPosition = Vector2.zero;


            rectTransform.gameObject.AddComponent<Window>();


            var image = rectTransform.gameObject.AddComponent<Image>();
            image.color = new Color(0, 0, 0, 0.5f);


            GameObjectUtility.SetParentAndAlign(rectTransform.gameObject, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(rectTransform.gameObject, "Create " + rectTransform.gameObject.name);

            Selection.activeObject = rectTransform.gameObject;
        }
        
        
        
        public static RectTransform CreateUiElement(string name, Transform parent = null)
        {
            GameObject go = new GameObject(name);
            go.layer = 5;
            if (!parent)
            {
                //CanvasSettings
                var canvas = GetCanvas();
                go.transform.parent = canvas.transform;
            }
            else
            {
                go.transform.parent = parent;
            }

            //rect transform settings
            var rectTransform = go.AddComponent<RectTransform>();

            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;


            return rectTransform;
        }
        
        
        private static Canvas GetCanvas()
        {
            var canvas = FindObjectOfType<Canvas>();
            if (!canvas)
            {
                GameObject canvasGO = new GameObject("Canvas");
                canvasGO.layer = 5;
                canvas = canvasGO.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGO.AddComponent<CanvasScaler>();
                canvasGO.AddComponent<GraphicRaycaster>();
            }

            return canvas;
        }
        
        

        [MenuItem("Edit/Toggle DredWindow #&q")]
        static void ToggleSelectedWindow()
        {
            int toggleTo = -1;
            if ((Selection.activeObject is GameObject gm))
            {
                if (gm.TryGetComponent(out Window window))
                {
                    if (toggleTo == -1)
                        toggleTo = (window.General.CurrentState == StatesRead.Closed ||
                                    window.General.CurrentState == StatesRead.Closing)
                            ? 1
                            : 0;
                    window.Switch(toggleTo == 1,"Instantly");
                }
            }
        }

    }
}