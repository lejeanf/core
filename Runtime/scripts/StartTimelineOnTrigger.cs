using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace jeanf.core
{
    public class StartTimelineOnTrigger : MonoBehaviour
    {
        [SerializeField] InputAction input;
        [SerializeField] PlayableDirector playableDirector;
        private bool isTimeLineOn = false;

        private void OnEnable()
        {
            input.Enable();
            input.performed += ctx => StartTimeline();
        }

        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();

        void Unsubscribe()
        {
            input.performed -= ctx => StartTimeline();
            input.Disable();
        }

        void StartTimeline()
        {
            if (isTimeLineOn)
            {
                Unsubscribe();
                return;
            }
            //Debug.Log($"Starting timeline at {DateTime.Now}");
            playableDirector.Play();
            isTimeLineOn = true;
        }

    }
}