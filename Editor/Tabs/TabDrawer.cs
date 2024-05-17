﻿using System;
using DredPack.UIWindow;
using DredPack.UIWindow.Tabs;
using UnityEditor;

namespace DredPack.UIWindowEditor
{
    public class TabDrawer
    {
        protected WindowEditor window;
        protected SerializedProperty tabProperty;
        protected SerializedProperty componentsProperty;
        public virtual Type DrawerOfTab => null;
        
        public virtual void Init(WindowEditor window, SerializedProperty tabProperty)
        {
            this.window = window;
            this.tabProperty = tabProperty;
            componentsProperty = this.window.serializedObject.FindProperty("Components");
        }

        
        public virtual void Draw()
        {
        }

    }
}