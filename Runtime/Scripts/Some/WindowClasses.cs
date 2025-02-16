using System;
using UnityEngine;
using UnityEngine.UI;

namespace DredPack.UIWindow
{


    [Serializable]
    public class Components
    {
        public GameObject DisableableObject;
        public Canvas Canvas;
        public GraphicRaycaster Raycaster;
        public Graphic BackgroundImage;
        public CanvasGroup CanvasGroup;
        public Selectable SelectableOnOpen;
        public UnityEngine.Camera CanvasCamera => Canvas.worldCamera;
    }


    #region Enums



    public enum StatesRead
    {
        Opened,
        Opening,
        Closed,
        Closing
    }

    public enum StatesAwake
    {
        Open,
        Close
    }

    public enum StatesAwakeMethod
    {
        Start,
        Awake,
        OnEnable,
        Nothing
    }

    #endregion


    //вызывать ли ивенты при старте
    //возможность задать направление анимации
    //новая анимация - список трансформов, которые можно как угодно двигать, скейлить и вращать
    //загрузка профилей
    //запуск совершенно разных анимаций через передачу их как параметр
    //возможность у каждой анимации задавать открытое / закрытое состояние моментально

}