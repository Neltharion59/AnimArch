using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Parsers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visualization.ClassDiagram;
using Visualization.UI;
using Visualization.UI.ClassComponentsManagers;

namespace Visualization.Animation
{

public class MaskingHandler : Singleton<MaskingHandler>
{
    [SerializeField] private TMP_Text MaskingFileLabel;
    [SerializeField] private Button RemoveMaskingBtn;

    private ClassDiagram.Diagrams.ClassDiagram classDiagram;

    private void Awake()
    {
        classDiagram = GameObject.Find("ClassDiagram").GetComponent<ClassDiagram.Diagrams.ClassDiagram>();
    }

    private void changeLabel(string filePath) {
        MaskingFileLabel.text = filePath;
    }

    private void setRemoveMaskingBtnInteractability(bool interactable) {
        RemoveMaskingBtn.interactable = interactable;
    }

    private void setObjectsActive(GameObject node, bool active) {
        node.transform.Find("Attributes").gameObject.SetActive(active);
        node.transform.Find("VLine").gameObject.SetActive(active);
        node.transform.Find("Methods").gameObject.SetActive(active);
    }

    private void beginMasking(string filePath) {
        Dictionary<string, string> maskingRules = JsonParser.LoadMaskingFile(filePath);
        foreach (KeyValuePair<string, string> rule in maskingRules) {
            try {
                GameObject node = classDiagram.FindNode(rule.Key).transform.GetChild(0).gameObject;
                setObjectsActive(node, false);

                var mask = UnityEngine.Object.Instantiate(DiagramPool.Instance.classMaskingPrefab, node.transform, false);
                mask.name = "MaskingLabel";
                mask.transform.Find("MaskingText").GetComponent<TextMeshProUGUI>().text += rule.Value;
            } catch (NullReferenceException) {
                Debug.LogError("Node: " + rule.Key + " is null!");
            }
        }
    }

    public void HandleMaskingFile(string filePath) {
        RemoveMasking();
        changeLabel(Path.GetFileName(filePath));
        setRemoveMaskingBtnInteractability(true);
        beginMasking(filePath);
    }

    public void RemoveMasking() {
        changeLabel("");
        setRemoveMaskingBtnInteractability(false);
        foreach (var diagramClass in classDiagram.Classes) {
            try {
                GameObject node = diagramClass.VisualObject.transform.GetChild(0).gameObject;
                setObjectsActive(node, true);

                Destroy(node.transform.Find("MaskingLabel").gameObject);
            } catch (NullReferenceException) {}
        }
    }

}

}