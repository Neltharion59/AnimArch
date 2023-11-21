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
            GetLineText(line).color =
                Animation.Instance.methodColor;
        }

        public void HighlightClassNameLine() {
            RainbowHighlightClassLine(true);
        }

        public void UnhighlightClassLine(string line)
        {
            GetLineText(line).color = Color.black;
        }

        public void UnhighlightClassNameLine() {
            RainbowHighlightClassLine(false);
        }

        public void RainbowHighlightClassLine(bool shouldBeHighlighted) {
            var background = methodLayoutGroup.transform.parent.parent;
            string className = background.parent.name;
            var headerLayout = background.Find("HeaderLayout");
            if (headerLayout != null) {
                TextMeshProUGUI textComponent = headerLayout.gameObject.GetComponentsInChildren<TextMeshProUGUI>().First();
                if (shouldBeHighlighted) {
                    if (RainbowColoringHelper.ActiveRainbows[className]) {return;}
                    textComponent.overrideColorTags = true;
                    if (!RainbowColoringHelper.ActiveRainbows.TryAdd(className, true)) {
                        RainbowColoringHelper.ActiveRainbows[className] = true;
                    }
                    StartCoroutine(RainbowColoringHelper.ColorRainbow(textComponent, className));
                } else {
                    textComponent.overrideColorTags = false;
                    RainbowColoringHelper.ActiveRainbows[className] = false;
                }
            }
        }
    }
}
