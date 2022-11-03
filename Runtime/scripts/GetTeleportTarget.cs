using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core{
    public class GetTeleportTarget : MonoBehaviour
    {
        [SerializeField] string currentScene;
        private void OnEnable()
        {
            TeleportOnEvent.getTeleportTarget += ReturnTarget;
        }
        private void OnDisable() => Unsubsribe();
        private void OnDestroy() => Unsubsribe();
        void Unsubsribe()
        {
            TeleportOnEvent.getTeleportTarget -= ReturnTarget;
        }

        void ReturnTarget(string sceneName, List<Transform> targets) 
        {
            if (currentScene != sceneName)
            targets.Add(this.transform);
            Debug.Log($"assigning targetTansform for {currentScene}");
        }
    }

}
