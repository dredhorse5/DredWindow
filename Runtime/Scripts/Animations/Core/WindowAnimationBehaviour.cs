﻿using System.Collections;
using DredPack.UIWindow.Animations;
using UnityEngine;

namespace DredPack.UIWindow.Animations
{
    public abstract class WindowAnimationBehaviour : MonoBehaviour, IWindowAnimation
    {
        public Window window { get; private set; }

        
        public virtual void Init(Window owner)
        {
            if (!window)
            {
                window = owner;
                OnInit(owner);
            }
        }

        public virtual void OnInit(Window owner) { }

        public virtual IEnumerator UpdateOpen(AnimationParameters parameters) {yield break;}
        public virtual void SetOpenTime(float time, AnimationParameters parameters) { }
        
        public virtual IEnumerator UpdateClose(AnimationParameters parameters) {yield break;}
        public virtual void SetCloseTime(float time, AnimationParameters parameters) { }

    }
}