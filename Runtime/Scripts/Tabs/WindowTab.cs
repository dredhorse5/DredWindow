using System;
using UnityEngine;

namespace DredPack.UIWindow.Tabs
{
    [Serializable]
    public class WindowTab
    {
        public virtual int InspectorDrawSort => 1000;
        public virtual string TabName => GetType().Name.Replace("Tab", "");
        [HideInInspector]
        public Window window;
        public virtual void Init(Window owner) => window = owner;
    }
}