using System;
using UnityEngine;

namespace DredPack.UIWindow.TNRD.Autohook
{
    /// <summary>
    /// Taken from here https://github.com/Thundernerd/Unity3D-AutoHook
    /// Thank you!
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AutoHookAttribute : PropertyAttribute
    {
        public AutoHookSearchArea SearchArea;
        /// <summary>
        /// Reduces the size of the property to 0 when a matching component has been found
        /// </summary>
        public bool HideWhenFound;
        /// <summary>
        /// Marks the property as read-only when a matching component has been found
        /// </summary>
        public bool ReadOnlyWhenFound;
        /// <summary>
        /// Stops calling GetComponent when a matching component has been found. This is useful if you have many [AutoHook] usages in your file
        /// </summary>
        public bool StopSearchWhenFound;

        public AutoHookAttribute()
        {
            SearchArea = AutoHookSearchArea.Default;
        }

        public AutoHookAttribute(AutoHookSearchArea searchArea)
        {
            SearchArea = searchArea;
        }
    }
    
    public enum AutoHookSearchArea
    {
        Default,
        Parent,
        Children,
        DirectChildrenOnly,
        AllChildrenOnly,
    }
}
