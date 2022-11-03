using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jeanf.core
{
    public class Phase1 : SceneTransitions
    {
        public override void FadeIn(Scene scene)
        {
            Debug.Log($"Received instruction to fade in:{scene.name}");
        }

        public override void FadeOut(Scene scene)
        {
            Debug.Log($"Received instruction to fade out: {scene.name}");
        }
    }
}
