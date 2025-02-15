using System;
using UnityEngine;

namespace DredPack.UIWindow.Tabs
{
    [Serializable]
    public class WindowTab
    {
        [HideInInspector]
        public Window window;
        public virtual void Init(Window owner) => window = owner;
    }
}