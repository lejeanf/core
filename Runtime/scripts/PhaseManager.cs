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

        public SceneLoader sceneLoader;
        public TransitionManager transitionManager;
        public CameraManager cameraManager;

        [SerializeField] public List<Phase> listOfPhases = new List<Phase>();

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
        }

        void LoadPhase(int phaseId)
        {
            Debug.Log($"listOfPhases.Count: {listOfPhases.Count}, phaseId: {phaseId}");
            Phase phase = listOfPhases[phaseId];
            currentPhase = phase;
            cameraManager.currentScene = phase.sceneToLoad;
            //broadcastSceneName?.Invoke(phase.sceneToLoad);
            StartCoroutine(sceneLoader.LoadScene(phase.sceneToLoad, listOfPhases.Count, transitionManager));
        }
    }
}