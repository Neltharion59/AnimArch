using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RainbowColoringHelper : MonoBehaviour
{
    public static Dictionary<string, bool> ActiveRainbows { get; set; } = new Dictionary<string, bool>();

    public static IEnumerator ColorRainbow(TextMeshProUGUI textComponent, string line)
    {
        int count = 0;
        while (ActiveRainbows[line])
        {
            for (int i = 0; i < textComponent.textInfo.characterCount; ++i)
            {
                if (textComponent.text[i].ToString().Trim().Length == 0) {continue;}
                string hexcolor = Rainbow(textComponent.textInfo.characterCount * 5, i + count + (int)Time.deltaTime);
                Color32 myColor32 = hexToColor(hexcolor);
                int meshIndex = textComponent.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textComponent.textInfo.characterInfo[i].vertexIndex;
                Color32[] vertexColors = textComponent.textInfo.meshInfo[meshIndex].colors32;
                vertexColors[vertexIndex + 0] = myColor32;
                vertexColors[vertexIndex + 1] = myColor32;
                vertexColors[vertexIndex + 2] = myColor32;
                vertexColors[vertexIndex + 3] = myColor32;
            }
            count++;
            textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            yield return new WaitForSeconds(.1f);
        }
    }

    private static Color32 hexToColor(string hex)
    {
        hex = hex.Replace("0x", "");//in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", "");//in case the string is formatted #FFFFFF
        byte a = 255;//assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }

    private static string Rainbow(int numOfSteps, int step)
    {
        var r = 0.0;
        var g = 0.0;
        var b = 0.0;
        var h = (double)step / numOfSteps;
        var i = (int)(h * 6);
        var f = h * 6.0 - i;
        var q = 1 - f;
        switch (i % 6)
        {
            case 0:
                r = 1;
                g = f;
                b = 0;
                break;
            case 1:
                r = q;
                g = 1;
                b = 0;
                break;
            case 2:
                r = 0;
                g = 1;
                b = f;
                break;
            case 3:
                r = 0;
                g = q;
                b = 1;
                break;
            case 4:
                r = f;
                g = 0;
                b = 1;
                break;
            case 5:
                r = 1;
                g = 0;
                b = q;
                break;
        }
        return "#" + ((int)(r * 255)).ToString("X2") + ((int)(g * 255)).ToString("X2") + ((int)(b * 255)).ToString("X2");
    }
}
