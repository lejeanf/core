using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jeanf.core
{
    public class PhaseManagerInstructionsSender : MonoBehaviour
    {
        public delegate void LoadNext();
        public static LoadNext loadEvent;
        public delegate void LoadSpecificScene(int id);
        public static LoadSpecificScene loadSceneEvent;

        public void Load()
        {
            loadEvent?.Invoke();

        }

        public void LoadSpecific(int id)
        {
            loadSceneEvent?.Invoke(id);

        }
    }
}
