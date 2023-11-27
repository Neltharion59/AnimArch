using UnityEngine;

namespace AnimArch.Extensions
{
    public static class TextureExtensions
    {
        public static Texture Rotated90Degrees(this Texture texture)
        {
            return texture.ToTexture2D().rotated90();
        }
        public static Texture Rotated180Degrees(this Texture texture)
        {
            return texture.ToTexture2D().rotated180();
        }
        public static Texture Rotated270Degrees(this Texture texture)
        {
            return texture.ToTexture2D().rotated270();
        }
        // https://forum.unity.com/threads/converting-texture-to-texture2d.25991/
        public static Texture2D ToTexture2D(this Texture texture)
        {
            return Texture2D.CreateExternalTexture(
                texture.width,
                texture.height,
                TextureFormat.RGB24,
                false, false,
                texture.GetNativeTexturePtr());
        }
        // https://forum.unity.com/threads/webcamtexture-flipping-and-rotating-90-degree-ios-and-android.143856/
        public static Texture2D rotated90(this Texture2D orig)
        {
            orig = orig.duplicateTexture();

            Color32[] origpix = orig.GetPixels32(0);
            Color32[] newpix = new Color32[orig.width * orig.height];
            for (int c = 0; c < orig.height; c++)
            {
                for (int r = 0; r < orig.width; r++)
                {
                    newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] =
                      origpix[orig.width * orig.height - (orig.width * c + orig.width) + r];
                }
            }
            Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
            newtex.SetPixels32(newpix, 0);
            newtex.Apply();
            return newtex;
        }
        public static Texture2D rotated180(this Texture2D orig)
        {
            orig = orig.duplicateTexture();

            Color32[] origpix = orig.GetPixels32(0);
            Color32[] newpix = new Color32[orig.width * orig.height];
            for (int i = 0; i < origpix.Length; i++)
            {
                newpix[origpix.Length - i - 1] = origpix[i];
            }
            Texture2D newtex = new Texture2D(orig.width, orig.height, orig.format, false);
            newtex.SetPixels32(newpix, 0);
            newtex.Apply();
            return newtex;
        }
        public static Texture2D rotated270(this Texture2D orig)
        {
            orig = orig.duplicateTexture();

            Color32[] origpix = orig.GetPixels32(0);
            Color32[] newpix = new Color32[orig.width * orig.height];
            int i = 0;
            for (int c = 0; c < orig.height; c++)
            {
                for (int r = 0; r < orig.width; r++)
                {
                    newpix[orig.width * orig.height - (orig.height * r + orig.height) + c] = origpix[i];
                    i++;
                }
            }
            Texture2D newtex = new Texture2D(orig.height, orig.width, orig.format, false);
            newtex.SetPixels32(newpix, 0);
            newtex.Apply();
            return newtex;
        }
        private static Texture2D duplicateTexture(this Texture2D source)
        {
            RenderTexture renderTex = RenderTexture.GetTemporary(
                        source.width,
                        source.height,
                        0,
                        RenderTextureFormat.Default,
                        RenderTextureReadWrite.Linear);

            Graphics.Blit(source, renderTex);
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTex;
            Texture2D readableText = new Texture2D(source.width, source.height);
            readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
            readableText.Apply();
            RenderTexture.active = previous;
            RenderTexture.ReleaseTemporary(renderTex);
            return readableText;
        }

    }
}