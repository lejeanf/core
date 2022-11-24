using jeanf.core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core 
{
    [RequireComponent(typeof(PhaseManager))]
    public class PhaseManagerInterface : MonoBehaviour
    {
        private PhaseManager phaseManager;
        private void Awake()
        {
            phaseManager = this.GetComponent<PhaseManager>();
        }
        void OnEnable()
        {
            PhaseManagerInstructionsSender.loadEvent += LoadNext;
            PhaseManagerInstructionsSender.loadSceneEvent += LoadSpecific;

        }
        void OnDisable() => Unsubscribe();

        void OnDestroy() => Unsubscribe();
        void Unsubscribe()
        {
            PhaseManagerInstructionsSender.loadEvent -= LoadNext;
            PhaseManagerInstructionsSender.loadSceneEvent -= LoadSpecific;

        }

        private void LoadNext()
        {
            phaseManager.Next();
        }

        private void LoadSpecific(int id)
        {
            phaseManager.LoadPhase(id);
        }

    }
}

