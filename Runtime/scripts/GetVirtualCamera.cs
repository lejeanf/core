using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core
{
    public class GetVirtualCamera : MonoBehaviour
    {
        [SerializeField] string currentScene;
        private void OnEnable()
        {
            CameraManager.getVirtualCameras += AddCameraToList;
        }
        private void OnDisable() => Unsubsribe();
        private void OnDestroy() => Unsubsribe();
        void Unsubsribe()
        {
            CameraManager.getVirtualCameras -= AddCameraToList;
        }

        void AddCameraToList(List<CinemachineVirtualCamera> virtualCameras, string sceneToSearch)
        {
            //Debug.Log("Ping is for this scene, going forward");
            CinemachineVirtualCamera currentVirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            if (currentScene != sceneToSearch) return;
            if (!virtualCameras.Contains(currentVirtualCamera)) virtualCameras.Add(currentVirtualCamera);
        }
    }
}
