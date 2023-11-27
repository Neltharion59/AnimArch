using TMPro;
using UnityEngine;
using System.Linq;
using AnimArch.Extensions;
using UnityEngine.UI;

namespace Visualization.UI
{
    public class ConsolePanel : Singleton<ConsolePanel>
    {
        public GameObject inputField;
        public GameObject outputField;

        private TMP_InputField tmpInpField;
        private TMP_InputField tmpOutpField;
        private Scrollbar tmpScrollbar;

        private void Awake()
        {
            tmpInpField = inputField.GetComponent<TMP_InputField>();
            tmpOutpField = outputField.GetComponent<TMP_InputField>();
            tmpScrollbar = tmpOutpField.verticalScrollbar.GetComponent<Scrollbar>();
            DeActivateInputField();
        }


        public void YieldOutput(string output)
        {
            tmpOutpField.text += output + "\n";

            // Scroll to bottom
            tmpScrollbar.value = 1.0f;
        }

        public void ActivateInputField()
        {
            tmpInpField.interactable = true;
        }
        private void DeActivateInputField()
        {
            tmpInpField.interactable = false;
        }

        public void InputEntered(string enteredText)
        {
            DeActivateInputField();

            tmpOutpField.text += enteredText + "\n";
            tmpInpField.text = "";

            Visualization.Animation.Animation.Instance.ReadValue = enteredText;
            Visualization.Animation.Animation.Instance.IncrementBarrier();
        }
    }
}
