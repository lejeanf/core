using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace jeanf.core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnLoadBegin = new();
        [SerializeField] private UnityEvent OnLoadEnd = new();

        public delegate void InitScene(string sceneName);
        public static event InitScene initScene;

        [SerializeField] private List<string> loadedScenes = new();

        public IEnumerator LoadScene(string sceneToLoad, int phaseCount, TransitionManager transitionManager)
        {
            if (!loadedScenes.Contains(sceneToLoad))
            {
                OnLoadBegin?.Invoke();

                Scene scene = SceneManager.GetSceneByName(sceneToLoad);

                Application.backgroundLoadingPriority = ThreadPriority.Normal;
                var loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
                initScene?.Invoke(sceneToLoad);

                yield return new WaitUntil(() => loadingOperation.isDone); 
                loadedScenes.Add(sceneToLoad);
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));

                if (loadedScenes.Count > 1) UnloadAllExept(sceneToLoad, phaseCount);

                transitionManager.FadeIn(scene);

                OnLoadEnd?.Invoke();
            }
        }
        public IEnumerator UnloadScene(string sceneToUnload, AsyncOperation unloadOperation)
        {
            if (sceneToUnload != "")
            {
                if (SceneManager.GetSceneByName(sceneToUnload).isLoaded)
                {
                    unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
                    //while (unloadOperation.isDone) yield return null;

                    yield return new WaitUntil(() => unloadOperation.isDone);
                    loadedScenes.Remove(sceneToUnload);
                }
            }
        }

        public void UnloadAllExept(string sceneToKeep, int phaseCount)
        {
            AsyncOperation[] unloadOperation = new AsyncOperation[phaseCount];

            for (int i = 0; i < loadedScenes.Count; i++)
            {
                if (loadedScenes[i] != sceneToKeep) StartCoroutine(UnloadScene(loadedScenes[i], unloadOperation[i]));
            }
            loadedScenes.TrimExcess();
        }
    }
}
