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

        List<Phase> listOfPhases = new List<Phase>();
        [SerializeField] List<string> scenesToIgnore = new List<string>();

        public InputAction nextPhase;
        public InputAction previousPhase;
            
        private void Awake()
        {
            FindAllScenesExcept(scenesToIgnore);
            Subsribe();
            LoadPhase(0);
        }

        void Subsribe()
        {
            nextPhase.Enable();
            previousPhase.Enable();
            nextPhase.performed += ctx => Next();
            previousPhase.performed += ctx => Previous();

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
            nextPhase.performed -= ctx => Next();
            previousPhase.performed -= ctx => Previous();
            nextPhase.Disable();
            previousPhase.Disable();

            foreach (Phase i in listOfPhases)
            {
                i.inputAction.performed -= ctx => LoadPhase(i.id);
                i.inputAction.Disable();
            }
        }

        void LoadPhase(int phaseId)
        {
            Phase phase = listOfPhases[phaseId];
            currentPhase = phase;
            currentId = phase.id;
            cameraManager.currentScene = phase.sceneToLoad;

            StartCoroutine(sceneLoader.LoadScene(phase.sceneToLoad, listOfPhases.Count, transitionManager));
        }

        public void Next()
        {
            if (listOfPhases.Count == 0) return;
            int currentIndex = currentId;
            currentIndex = (currentId + 1) % (listOfPhases.Count);
            if (currentIndex > listOfPhases.Count - 1 || currentIndex < 0) currentIndex = 0;
            LoadPhase(currentIndex);
            Debug.Log($"next: phase  = {currentIndex}");
        }

        public void Previous() 
        {
            if (listOfPhases.Count == 0) return;
            int currentIndex = (currentId + 1) % listOfPhases.Count;
            LoadPhase(currentIndex);
            Debug.Log($"previous: phase = {currentIndex}");
        }

        void FindAllScenesExcept(List<string> scenesToIgnore) {
            List<string> scenesInBuild = new List<string>();
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            for (int i = 0; i < sceneCount; i++)
            {
                string scene = SceneUtility.GetScenePathByBuildIndex(i);
                scene = scene.Replace(".unity", "");
                if (scene.Contains("/"))
                {
                    var split = scene.Split("/");
                    scene = split[split.Length - 1];
                }
                if (!scenesInBuild.Contains(scene) && scene != "" && scene != "persistent" && !scenesToIgnore.Contains(scene))
                {
                    scenesInBuild.Add(scene);
                    Debug.Log($"adding {scene} to phases");
                }
            }

            List<InputAction> listOfInputAction = new List<InputAction>();
            int id = 0;
            foreach (string s in scenesInBuild)
            {
                InputAction inputAction = new InputAction();
                if (id <= 9)
                {
                    inputAction.AddBinding($"<Keyboard>/{id + 1}");
                    inputAction.AddBinding($"<Keyboard>/numpad{id + 1}");
                }
                listOfInputAction.Add(inputAction);

                listOfPhases.Add(new Phase(s, 0, listOfInputAction[id], id));
                id++;
            }
        }
    }
}