using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace jeanf.core 
{
    public class UpdateTimer : MonoBehaviour
    {
        TextMeshProUGUI timeText;
        float lastTimerValue = 0;

        public delegate void HideUI();
        public static HideUI hideUI;
        private void Awake()
        {
            timeText = this.GetComponent<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            FunctionTimer.timerValue += UpdateValue;
        }
        private void OnDisable() => Unsubsribe();
        private void OnDestroy() => Unsubsribe();

        private void Unsubsribe()
        {
            FunctionTimer.timerValue -= UpdateValue;
        }
        private void UpdateValue(float value)
        {
            if (value <= 0) 
            { 
                value = 0;
                hideUI?.Invoke();
            }
            value = Mathf.Round(value);

            if (lastTimerValue == value) return;
            //Debug.Log($"timer: {value}");
            timeText.text = $"{value}s";
            lastTimerValue = value;
        }
    }
}

