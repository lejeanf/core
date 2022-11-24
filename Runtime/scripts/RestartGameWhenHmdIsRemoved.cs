using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

namespace jeanf.core 
{
    public class RestartGameWhenHmdIsRemoved : MonoBehaviour
    {
        public PhaseManager phaseManager;
        public bool hmdLastStatus = true;

        public float cancelTime = 6f;

        public Canvas cancelCanvas;
        public Canvas restartCanvas;

        public InputAction cancelButton;
        public InputAction restartButton;

        private void OnEnable()
        {
            restartCanvas.gameObject.SetActive(false);
            PhaseManager.hideUIevent += HideUI;
            hmdLastStatus = true;
            cancelCanvas.gameObject.SetActive(false);
            BroadcastHmdStatus.hmdStatus += ResetGame;
            cancelButton.Enable();
            cancelButton.performed += ctx => CancelReset();
            restartButton.Enable();
            restartButton.performed += ctx => ChangeRestartUIState();
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            PhaseManager.hideUIevent -= HideUI;
            cancelButton.performed -= ctx => CancelReset();
            cancelButton.Disable();
            restartButton.performed -= ctx => ChangeRestartUIState();
            restartButton.Disable();
            BroadcastHmdStatus.hmdStatus -= ResetGame;
        }

        public void ResetGame(bool state)
        {
            if (state && !hmdLastStatus)
            {
                cancelCanvas.gameObject.SetActive(true);
                FunctionTimer.Create(ResetExperience, cancelTime, "resetTimer");
                hmdLastStatus = state;
            }

            if (!state && hmdLastStatus) hmdLastStatus = false;
        }

        public void CancelReset()
        {
            cancelCanvas.gameObject.SetActive(false);
            Debug.Log("canceling reset");
            FunctionTimer.StopTimer("resetTimer");
        }

        private void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetSceneByBuildIndex(0).name);
        }

        public void ResetExperience() 
        {
            phaseManager.LoadPhase(0);
        }

        void ChangeRestartUIState() 
        {
            restartCanvas.gameObject.SetActive(!restartCanvas.gameObject.activeSelf);
        }

        void HideUI() 
        {
            if(hmdLastStatus) cancelCanvas.gameObject.SetActive(false);
        }
    }
}
