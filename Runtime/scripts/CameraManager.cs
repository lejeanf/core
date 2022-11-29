using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

namespace jeanf.core{
    public class CameraManager : MonoBehaviour
    {
        [HideInInspector] public List<CinemachineVirtualCamera> _virtualCameras = new();
        CinemachineBrain brain;
        public bool autoSetEndTime;
        //Only used if autoSetEndTime is false
        public float customEndTime;

        [SerializeField] InputAction input_nextCam;

        public delegate CinemachineBrain GetBrain();
        public static event GetBrain getBrain;

        public delegate void GetVirtualCameras(List<CinemachineVirtualCamera> virtualCameras, string sceneToSearch);
        public static event GetVirtualCameras getVirtualCameras;
        public bool isDebug = false;

        public string currentScene;

        void Awake()
        {
            brain = getBrain?.Invoke();
            if (!brain) return;
            if (autoSetEndTime) brain.m_DefaultBlend.m_Time = 1.0f/*GetComponent<DynamicScene>().endTime*/;
            else { brain.m_DefaultBlend.m_Time = customEndTime; }
            //SetRandomCam();
        }

        private void OnEnable()
        {
            //PhaseManager.broadcastSceneName += UpdateVirtualCamerasList;
            input_nextCam.Enable();
            input_nextCam.performed += ctx => NextCam();
            SceneLoader.initScene += Init;
        }
        private void OnDisable() => UnSubscribe();
        private void OnDestroy() => UnSubscribe();
        void UnSubscribe()
        {
            //PhaseManager.broadcastSceneName -= UpdateVirtualCamerasList;
            SceneLoader.initScene -= ctx => Init(ctx);
            input_nextCam.performed -= ctx => NextCam();
            input_nextCam.Disable();
        }

        private void Init(string sceneName)
        {
            //Debug.Log($"Init scene: {sceneName}");
            MoveToCam(0);
        }

        public void UpdateVirtualCamerasList() 
        {
            this._virtualCameras.Clear();
            this._virtualCameras.TrimExcess();
            getVirtualCameras?.Invoke(_virtualCameras, currentScene);
            //Debug.Log($"found {_virtualCameras.Count} _virtualCameras");
            MoveToCam(0);
        }


        public void SetRandomCam() { MoveToCam(Random.Range(0, _virtualCameras.Count)); }
        //public void SetCam(int camId) { moveToCam(camId); }


        public void MoveToCam(int camIndex)
        {
            for (int i = 0; i < _virtualCameras.Count; i++)
            {
                if (!_virtualCameras[i]) return;
                _virtualCameras[i].enabled = (i == camIndex);
            }
            if (isDebug) Debug.Log($"Moving to cam {camIndex}");
        }

        public int getCurrentCamIndex()
        {
            for (int i = 0; i < _virtualCameras.Count; i++)
            {
                if (_virtualCameras[i].enabled) return i;
            }
            return 0;
        }

        public void NextCam()
        {
            //Debug.Log("moving to next cam");
            //if (_virtualCameras == null || _virtualCameras.Count == 0) UpdateVirtualCamerasList(currentScene);
            int index = getCurrentCamIndex();
            if (!_virtualCameras[index]) return;
            MoveToCam((index + 1) % _virtualCameras.Count);
        }

        public void PreviousCam()
        {
            if (_virtualCameras == null || _virtualCameras.Count == 0) return;
            int index = getCurrentCamIndex();
            int currentIndex = (index - 1) % _virtualCameras.Count;
            if (!_virtualCameras[currentIndex]) return;
            if (currentIndex < 0) currentIndex = _virtualCameras.Count - 1;
            MoveToCam(currentIndex);
        }
    }
}