using UnityEngine;

namespace Crogen.CustomHierarchy.Editor
{
    public class EditorStyleGradient
    {
        public Texture2D GetTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height);
            return texture;
        }
    }
}