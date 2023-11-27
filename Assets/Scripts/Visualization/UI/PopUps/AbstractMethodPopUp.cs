using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Visualization.ClassDiagram;
using Visualization.UI.ClassComponentsManagers;

namespace Visualization.UI.PopUps
{
    public class AbstractMethodPopUp : AbstractTypePopUp
    {
        public TMP_Text confirm;
        public TMP_Text options;
        public TMP_Text isArrayText;

        protected const string Void = "void";

        [SerializeField] protected Transform parameterContent;
        protected List<GameObject> _parameters = new();

        private new void Awake()
        {
            base.Awake();
            dropdown.onValueChanged.AddListener(delegate
            {
                if (dropdown.options[dropdown.value].text == Void)
                {
                    options.transform.gameObject.SetActive(false);
                    isArray.transform.gameObject.SetActive(false);
                    isArrayText.transform.gameObject.SetActive(false);
                }
                else
                {
                    options.transform.gameObject.SetActive(true);
                    isArray.transform.gameObject.SetActive(true);
                    isArrayText.transform.gameObject.SetActive(true);
                }
            });
        }

        public bool ArgExists(string parameter)
        {
            return _parameters.Any(paramObject => string.Equals(parameter, paramObject.name));
        }

        public void AddArg(string parameter)
        {
            GameObject instance = Instantiate(DiagramPool.Instance.parameterMethodPrefab, parameterContent, false);
            SetArgName(instance, parameter);
            _parameters.Add(instance);
        }

        public void EditArg(string formerParam, string newParam)
        {
            GameObject argToBeEdited = _parameters.FirstOrDefault(arg => string.Equals(formerParam, arg.name));
            if (argToBeEdited != null)
            {
                SetArgName(argToBeEdited, newParam);
            }
        }

        public void RemoveArg(string parameter)
        {
            GameObject argToBeRemoved = _parameters.FirstOrDefault(arg => string.Equals(parameter, arg.name));
            if (argToBeRemoved != null)
            {
                _parameters.Remove(argToBeRemoved);
                argToBeRemoved.transform.SetParent(null);
                DestroyImmediate(argToBeRemoved);
            }
        }

        private void SetArgName(GameObject argObject, string newName)
        {
            argObject.name = newName;
            argObject.GetComponent<ParameterManager>().parameterTxt.SetText(newName);
        }
    }
}
