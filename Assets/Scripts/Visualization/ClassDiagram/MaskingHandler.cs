using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    private Dictionary<string, string> maskingRules = null;

    private ClassDiagram.Diagrams.ClassDiagram classDiagram;

    private void Awake()
    {
        classDiagram = DiagramPool.Instance.ClassDiagram.GetComponent<ClassDiagram.Diagrams.ClassDiagram>();
    }

    private void changeLabel(string filePath) 
    {
        MaskingFileLabel.text = filePath;
    }

    private void setRemoveMaskingBtnInteractability(bool interactable) 
    {
        RemoveMaskingBtn.interactable = interactable;
    }

    private void setObjectsActive(GameObject node, bool active)
    {
        node.transform.Find("Attributes").gameObject.SetActive(active);
        node.transform.Find("Methods").gameObject.SetActive(active);
        foreach (Transform child in node.transform) 
        {
            if (child != null && "VLine".Equals(child.name))
            {
                child.gameObject.SetActive(active);
            }
        }
    }

    private void beginMasking(string filePath) 
    {
        maskingRules = JsonParser.LoadMaskingFile(filePath);
        foreach (KeyValuePair<string, string> rule in maskingRules) 
        {
            try
            {
                GameObject node = classDiagram.FindNode(rule.Key).transform.GetChild(0).gameObject;
                setObjectsActive(node, false);
                node.transform.Find("HeaderLayout").gameObject.GetComponentsInChildren<TextMeshProUGUI>().First().text = rule.Value;
            }
            catch (NullReferenceException)
            {
                Debug.LogError("Node: " + rule.Key + " is null!");
            }

        }
    }

    public void HandleMaskingFile(string filePath)
    {
        RemoveMasking();
        changeLabel(Path.GetFileName(filePath));
        setRemoveMaskingBtnInteractability(true);
        beginMasking(filePath);
    }

    public void RemoveMasking() 
    {
        changeLabel("");
        setRemoveMaskingBtnInteractability(false);
        if (maskingRules != null)
        {
            foreach (KeyValuePair<string, string> rule in maskingRules)
            {
                try
                {
                    GameObject node = classDiagram.FindNode(rule.Key).transform.GetChild(0).gameObject;
                    setObjectsActive(node, true);
                    node.transform.Find("HeaderLayout").gameObject.GetComponentsInChildren<TextMeshProUGUI>().First().text = rule.Key;
                }
                    catch (NullReferenceException)
                {
                    Debug.LogError("Node: " + rule.Key + " is null!");
                }
            }
        }
    }

}

}