using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jeanf.core
{
    public class TransitionManager : MonoBehaviour
    {
        public delegate void Transition(Scene scene);

        public static event Transition fadeIn;
        public static event Transition fadeOut;
        public void FadeIn(Scene sceneToFadeIn)
        {
            fadeIn?.Invoke(sceneToFadeIn);
        }
        public void FadeOut(Scene sceneToFadeOut)
        {
            fadeOut?.Invoke(sceneToFadeOut);
        }
    }
}
