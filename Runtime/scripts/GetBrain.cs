using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core
{
    public class GetBrain : MonoBehaviour
    {

        void OnEnable() { CameraManager.getBrain += ReturnBrain; }
        void OnDisable() => Unsubscribe();
        void OnDestroy() => Unsubscribe();
        void Unsubscribe() { CameraManager.getBrain -= ReturnBrain; }
        Cinemachine.CinemachineBrain ReturnBrain()
        {
            return this.gameObject.GetComponent<Cinemachine.CinemachineBrain>();
        }
    }
}
