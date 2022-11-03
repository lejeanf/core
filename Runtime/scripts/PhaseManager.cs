using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace jeanf.core
{
    public class PhaseManager : MonoBehaviour
    {
        [Serializable]
        public class Phase
        {
            public string sceneToLoad;
            public int id;
            public float transitionTime;
            public InputAction inputAction;
            public Phase(string sceneToLoad, float transitionTime, InputAction inputAction, int id)
            {
                this.sceneToLoad = sceneToLoad;
                this.transitionTime = transitionTime;
                this.inputAction = inputAction;
                this.id = id;
            }
        }
        Phase currentPhase;
        int currentId;

        public SceneLoader sceneLoader;
        public TransitionManager transitionManager;
        public CameraManager cameraManager;

        [SerializeField] public List<Phase> listOfPhases = new List<Phase>();

        public InputAction nextPhase;
        public InputAction previousPhase;

        //public delegate void BroadcastSceneName(string sceneName);
        //public static event BroadcastSceneName broadcastSceneName;
        private void Awake()
        {
            Subsribe();
            //sceneLoader = this.gameObject.AddComponent<SceneLoader>();
            //transitionManager = this.gameObject.AddComponent<TransitionManager>();
            currentPhase = listOfPhases[0];
            cameraManager.currentScene = currentPhase.sceneToLoad;
            LoadPhase(0);
        }

        void Subsribe()
        {
            foreach (Phase i in listOfPhases)
            {
                i.inputAction.Enable();
                i.inputAction.performed += ctx => LoadPhase(i.id);
            }
            nextPhase.Enable();
            previousPhase.Enable();
            nextPhase.performed += ctx => Next();
            previousPhase.performed += ctx => Previous();
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            foreach (Phase i in listOfPhases)
            {
                i.inputAction.performed -= ctx => LoadPhase(i.id);
                i.inputAction.Disable();
            }
            nextPhase.performed -= ctx => Next();
            previousPhase.performed -= ctx => Previous();
            nextPhase.Disable();
            previousPhase.Disable();
        }

        void LoadPhase(int phaseId)
        {
            //Debug.Log($"listOfPhases.Count: {listOfPhases.Count}, phaseId: {phaseId}");
            Phase phase = listOfPhases[phaseId];
            currentPhase = phase;
            currentId = phase.id;
            cameraManager.currentScene = phase.sceneToLoad;
            //broadcastSceneName?.Invoke(phase.sceneToLoad);
            StartCoroutine(sceneLoader.LoadScene(phase.sceneToLoad, listOfPhases.Count, transitionManager));
        }

        public void Next()
        {
            if (listOfPhases.Count == 0) return;
            int index = currentId;
            LoadPhase((index + 1) % listOfPhases.Count);
            Debug.Log($"phase = {index}");
        }

        public void Previous() 
        {
            if (listOfPhases.Count == 0) return;
            int index = currentId;
            int currentIndex = (index - 1) % listOfPhases.Count;
            if (currentIndex < 0) currentIndex = listOfPhases.Count - 1;
            LoadPhase(currentIndex);
            Debug.Log($"phase = {currentIndex}");
        }
    }
}