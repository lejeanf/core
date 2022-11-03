using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace jeanf.core
{
    public abstract class SceneTransitions : MonoBehaviour
    {
        void OnEnable()
        {
            TransitionManager.fadeIn += ctx => FadeIn(ctx);
            TransitionManager.fadeOut += ctx => FadeOut(ctx);
        }
        void OnDisable() { Unsubscribe(); }
        void OnDestroy() { Unsubscribe(); }
        void Unsubscribe()
        {
            TransitionManager.fadeIn -= ctx => FadeIn(ctx);
            TransitionManager.fadeOut -= ctx => FadeOut(ctx);
        }

        public abstract void FadeIn(Scene scene);
        public abstract void FadeOut(Scene scene);
    }
}