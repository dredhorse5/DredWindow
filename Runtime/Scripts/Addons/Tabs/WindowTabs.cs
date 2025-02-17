using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DredPack.UIWindow.Addons.Tabs
{
    public class WindowTabs : MonoBehaviour
    {
        public int SelectOnStart = -1;
        
        public List<Element> Elements;
        
        public UnityEvent<Element> SelectedEvent;
        public Element nowSelectedElement { get; set; }

        protected virtual void Start()
        {
            Elements.ForEach(_ => _.Init(this));
            if(SelectOnStart >= 0)
                Select(SelectOnStart);
        }



        public virtual void Select(TabButton tabButton)
        {
            if(tabButton == null)
                return;
            Select(tabButton.connectedElement);
        }
        
        public virtual void Select(int index)
        {
            if(index >= Elements.Count || index < 0)
                return;
            Select(Elements[index]);
        }
        
        public virtual void Select(Element element)
        {
            if(element == null)
                return;
            if(element.owner != this)
                return;
            if(!CanSelect(element))
                return;
            
            if(nowSelectedElement != element)
                nowSelectedElement?.OnSelect(false);
            nowSelectedElement = element;
            nowSelectedElement.OnSelect(true);
            
            SelectedEvent?.Invoke(nowSelectedElement);
            OnSelected(nowSelectedElement);
        }
        
        protected virtual bool CanSelect(Element element) => true; 
        
        public virtual void UnSelect()
        {
            nowSelectedElement?.Select(false);
            nowSelectedElement = null;
            OnSelected(nowSelectedElement);
        }

        public virtual void UnSelect(Element element)
        {
            if(element.IsSelected)
                UnSelect();
        }

        public virtual void OnSelected(Element element) { }

        public virtual void RegisterCallback(ITabCallback callback, int elementIndex)
        {
            if(elementIndex >= Elements.Count || elementIndex < 0)
                return;
            Elements[elementIndex].RegisterCallback(callback);
        }
        
        
        [Serializable]
        public class Element
        {
            public Window Window;
            public TabButton TabButton;
            public List<TabCallback> Callbacks;

            public UnityEvent SelectedEvent;
            public UnityEvent UnSelectedEvent;
            public UnityEvent<bool> ChangedStateEvent;

            public WindowTabs owner { get; private set; }
            public bool IsSelected => owner.nowSelectedElement == this;
            private HashSet<ITabCallback> callbacks = new HashSet<ITabCallback>();

            public void Init(WindowTabs owner)
            {
                this.owner = owner;
                if (TabButton)
                    RegisterCallback(TabButton);
                foreach (var cal in Callbacks)
                    RegisterCallback(cal);
            }

            public void RegisterCallback(ITabCallback callback)
            {
                if(callback == null)
                    return;
                callbacks.Add(callback);
                callback.SetElement(this);
                callback.OnSelect(IsSelected);
            }

            public void OnSelect(bool state)
            {
                if(state)
                    SelectedEvent?.Invoke();
                else
                    UnSelectedEvent?.Invoke();
                ChangedStateEvent.Invoke(state);
                Window.Switch(state);
            }

            public void Select()
            {
                owner.Select(this);
            }

            public void UnSelect()
            {
                owner.UnSelect(this);
            }

            public void Select(bool state)
            {
                if(state)
                    Select();
                else
                    UnSelect();
            }
        }
    }
}