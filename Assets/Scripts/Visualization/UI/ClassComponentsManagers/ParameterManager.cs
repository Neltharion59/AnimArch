using System;
using TMPro;
using UnityEngine;
using Visualization.UI.PopUps;

namespace Visualization.UI.ClassComponentsManagers
{
    public class ParameterManager : MonoBehaviour
    {
        public TMP_Text parameterTxt;

        public void OpenParameterEditPopUp()
        {
            AbstractMethodPopUp originatingPopup = GetOriginatingPopup();
            originatingPopup.panel.SetActive(false);

            UIEditorManager.Instance.editParameterPopUp.ActivateCreation(parameterTxt);
        }

        public void DeleteParameter()
        {
            AbstractMethodPopUp originatingPopup = GetOriginatingPopup();

            originatingPopup.panel.SetActive(false);
            UIEditorManager.Instance.confirmPopUp.ActivateCreation(delegate
            {
                originatingPopup.RemoveArg(name);
                originatingPopup.panel.SetActive(true);
            });

            UIEditorManager.Instance.confirmPopUp.cancelButton.onClick.AddListener(delegate
            {
                originatingPopup.panel.SetActive(true);
            });

            UIEditorManager.Instance.confirmPopUp.exitButton.onClick.AddListener(delegate
            {
                originatingPopup.panel.SetActive(true);
            });
        }

        private AbstractMethodPopUp GetOriginatingPopup()
        {
            AbstractMethodPopUp originatingPopup;

            if (UIEditorManager.Instance.editMethodPopUp.panel.activeSelf)
            {
                originatingPopup = UIEditorManager.Instance.editMethodPopUp;
            }
            else if (UIEditorManager.Instance.addMethodPopUp.panel.activeSelf)
            {
                originatingPopup = UIEditorManager.Instance.addMethodPopUp;
            }
            else
            {
                throw new Exception("Parameter editor popup: \"No idea who opened me.\"");
            }

            return originatingPopup;
        }
    }
}
