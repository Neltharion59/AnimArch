using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visualization.UI;

public class MaskingHandler : Singleton<MaskingHandler>
{
    [SerializeField] private TMP_Text MaskingFileLabel;
    [SerializeField] private Button RemoveMaskingBtn;

    private void changeLabel(string filePath) {
        MaskingFileLabel.text = filePath;
    }

    private void setRemoveMaskingBtnInteractability(bool interactable) {
        RemoveMaskingBtn.interactable = interactable;
    }

    public void HandleMaskingFile(string filePath) {
        changeLabel(Path.GetFileName(filePath));
        setRemoveMaskingBtnInteractability(true);
    }

    public void RemoveMasking() {
        changeLabel("");
        setRemoveMaskingBtnInteractability(false);
    }

}
