using System;
using System.Collections;
using DredPack.UIWindow.TNRD.Autohook;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DredPack.UIWindow.Addons.Tabs
{
    public class TabCallback : MonoBehaviour, ITabCallback
    {
        [AutoHook]
        public WindowTabs WindowTabs;
        public int ElementIndex = 0;
        
        public UnityEvent SelectedEvent;
        public UnityEvent UnSelectedEvent;
        
        public Transitions Transition = Transitions.Off;
        
        public enum Transitions { Off, ObjectActive, ObjectsSwap, Animator, ColorSwap }



        public ObjectActiveTransitionTrigger objectActiveTransitionTrigger;
        public ObjectsSwapTransitionTrigger objectsSwapTransitionTrigger;
        public AnimatorTransitionTrigger animatorTransitionTrigger;
        public ColorSwapTransitionTrigger colorSwapTransitionTrigger;
        
        
        public WindowTabs.Element connectedElement { get; protected set; }
        public bool inited { get; private set; }


        protected virtual void Start()
        {
            colorSwapTransitionTrigger.SetOwner(this);
            if(WindowTabs && ElementIndex >= 0)
                WindowTabs.RegisterCallback(this, ElementIndex);
        }

        public virtual void SetElement(WindowTabs.Element element)
        {
            connectedElement = element;
        }

        public virtual void SelectThis()
        {
            connectedElement.Select();
        }

        public virtual void OnSelect(bool state)
        {
            if(state)
                SelectedEvent?.Invoke();
            else
                UnSelectedEvent?.Invoke();
            TriggerTransition(state);
        }

        protected virtual void TriggerTransition(bool state)
        {
            switch (Transition)
            {
                case TabButton.Transitions.ObjectActive:
                    objectActiveTransitionTrigger.SetState(state);
                    break;
                case TabButton.Transitions.ObjectsSwap:
                    objectsSwapTransitionTrigger.SetState(state);
                    break;
                case TabButton.Transitions.Animator:
                    animatorTransitionTrigger.SetState(state);
                    break;
                case TabButton.Transitions.ColorSwap:
                    colorSwapTransitionTrigger.SetState(state);
                    break;
            }
        }


        private interface ITransitionTrigger
        {
            public void SetState(bool state);
        }
        [Serializable]
        public class ObjectActiveTransitionTrigger : ITransitionTrigger
        {
            public GameObject Object;
            public void SetState(bool state)
            {
                if(Object)
                    Object.SetActive(state);
            }
        }
        [Serializable]
        public class ObjectsSwapTransitionTrigger : ITransitionTrigger
        {
            public GameObject SelectedObject;
            public GameObject UnSelectedObject;
            public void SetState(bool state)
            {
                if(SelectedObject)
                    SelectedObject.SetActive(state);
                if(UnSelectedObject)
                    UnSelectedObject.SetActive(!state);
            }
        }
        [Serializable]
        public class AnimatorTransitionTrigger : ITransitionTrigger
        {
            public Animator Animator;
            public string BoolParameterName = "State";
            public void SetState(bool state)
            {
                if(Animator)
                    Animator.SetBool(BoolParameterName, state);
            }
        }
        [Serializable]
        public class ColorSwapTransitionTrigger : ITransitionTrigger
        {
            public Graphic Graphic;
            [Space]
            public Color SelectedColor;
            public Color UnSelectedColor;
            [Space]
            public float SwapDuration = 1f;
            public AnimationCurve SwapCurve = new AnimationCurve(new[] { new Keyframe(0f, 0f), new Keyframe(1f, 1f) });

            private TabCallback owner;
            private Coroutine _coroutine;

            public void SetOwner(TabCallback owner)
            {
                this.owner = owner;
            }
            public void SetState(bool state)
            {
                if (owner)
                {
                    if(_coroutine != null)
                        owner.StopCoroutine(_coroutine);
                    _coroutine = owner.StartCoroutine(SwapIE(state));
                }
                else
                {
                    Graphic.color = state ? SelectedColor : UnSelectedColor;
                }
            }

            private IEnumerator SwapIE(bool state)
            {
                var startVal = Graphic.color;
                var endVal = state ? SelectedColor : UnSelectedColor;
                for (float i = 0f; i < 1f; i += Time.deltaTime * 1f/SwapDuration)
                {
                    Graphic.color = Color.Lerp(startVal, endVal, SwapCurve.Evaluate(i));
                    yield return null;
                }

                Graphic.color = endVal;
            }
        }
    }

    
    public interface ITabCallback
    {
        public void SetElement(WindowTabs.Element element);
        public void OnSelect(bool state);
    }
}