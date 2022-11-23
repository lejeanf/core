using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace jeanf.core{
    public class TeleportOnEvent : MonoBehaviour
    {
        public GameObject objectToTeleport;

        private void OnEnable()
        {
            SendTeleportTarget.teleportPlayer += ctx => Teleport(ctx);
        }
        private void OnDisable() => Unsubscribe();
        private void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            SendTeleportTarget.teleportPlayer -= ctx => Teleport(ctx);
        }

        public void Teleport(Transform teleportTarget)
        {
            if (!teleportTarget) return;
            Transform chosenTarget = teleportTarget;
            if (!objectToTeleport) return;

            objectToTeleport.transform.position = chosenTarget.position;
            objectToTeleport.transform.rotation = chosenTarget.rotation;
        }

    }

}
