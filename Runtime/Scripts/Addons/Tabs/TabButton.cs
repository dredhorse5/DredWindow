using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DredPack.UIWindow.Addons.Tabs
{
    [RequireComponent(typeof(Button))]
    public class TabButton : TabCallback
    {
        protected override void Start()
        {
            base.Start();
            GetComponent<Button>().onClick.AddListener(SelectThis);
        }
    }
}