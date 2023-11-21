using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Animation = Visualization.Animation.Animation;
using Color = UnityEngine.Color;

namespace UMSAGL.Scripts
{
    public class ClassTextHighligter : MonoBehaviour
    {
        public LayoutGroup methodLayoutGroup;

        private TextMeshProUGUI GetLineText(string line)
        {
            line = Regex.Replace(line, "[()]", "");
            return methodLayoutGroup.transform
                .GetComponentsInChildren<TextMeshProUGUI>()
                .First(x => x.text.Contains(line));
        }

        public void HighlightClassLine(string line)
        {
            RainbowHighlightClassLine(line);
            GetLineText(line).color =
                Animation.Instance.methodColor;
        }

        public void HighlightMaskLine(string line) {
            RainbowHighlightClassLine(line);
        }

        public void UnhighlightClassLine(string line)
        {
            GetLineText(line).color = Color.black;
        }

        public void UnhighlightMaskLine(string line) {
            line = Regex.Replace(line, "[()]", "");
            // var maskingLabel = methodLayoutGroup.transform.parent.parent.Find("MaskingLabel");
            var maskingLabel = methodLayoutGroup.transform.parent.parent.Find("HeaderLayout");
            if (maskingLabel != null) {
                TextMeshProUGUI textComponent = maskingLabel.gameObject.GetComponentsInChildren<TextMeshProUGUI>().First();
                textComponent.overrideColorTags = false;
                RainbowColoringHelper.ActiveMasks[line] = false;
            }
        }

        public void RainbowHighlightClassLine(string line) {
            line = Regex.Replace(line, "[()]", "");
            // var maskingLabel = methodLayoutGroup.transform.parent.parent.Find("MaskingLabel");
            var maskingLabel = methodLayoutGroup.transform.parent.parent.Find("HeaderLayout");
            if (maskingLabel != null) {
                TextMeshProUGUI textComponent = maskingLabel.gameObject.GetComponentsInChildren<TextMeshProUGUI>().First();
                textComponent.overrideColorTags = true;
                if (!RainbowColoringHelper.ActiveMasks.TryAdd(line, true)) {
                    RainbowColoringHelper.ActiveMasks[line] = true;
                }
                StartCoroutine(RainbowColoringHelper.ColorRainbow(textComponent, line));
            }
        }
    }
}
