﻿using TMPro;
using Unity.Netcode;

namespace Visualization.UI.PopUps
{
    public abstract class AbstractClassPopUp : AbstractPopUp
    {
        protected const string ErrorEmptyName = "Name can not be empty!";
        protected ulong _networkClassId;

        public TMP_InputField inp;
        public TMP_Text errorMessage;
        protected TMP_Text className;

        protected void Awake()
        {
            inp.onValueChanged.AddListener(delegate(string arg)
            {
                if (string.IsNullOrEmpty(arg))
                    return;
                if (arg.Length == 1 && (char.IsLetter(arg[0]) || arg[0] == '_'))
                    inp.text = arg;
                else if (arg.Length > 1 && char.IsLetterOrDigit(arg[^1]) || arg[^1] == '_')
                    inp.text = arg;
                else
                    inp.text = arg[..^1];
            });
            
            inp.onSubmit.AddListener(delegate { Confirmation(); });
        }

        protected void DisplayError(string message)
        {
            errorMessage.SetText(message);
            errorMessage.gameObject.SetActive(true);
        }

        public virtual void ActivateCreation(TMP_Text classTxt)
        {
            ActivateCreation();
            className = classTxt;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            inp.text = "";
            errorMessage.gameObject.SetActive(false);
        }
    }
}
