using UnityEngine;
using System.Collections;

public class ColorManager : MonoBehaviour {

    public Texture[] textures;

    public Texture2D tex;

    public Color[][] originColors;

    public Renderer quadRenderer;
	// Use this for initialization
	void Start () {
        originColors = new Color[textures.Length][];
	    for(int i=0;i<textures.Length;i++)
        {
            tex = (Texture2D)textures[i];
            originColors[i] = tex.GetPixels();
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void Colorize()
    {
        tex = (Texture2D)quadRenderer.material.mainTexture;
    }

    public void ResetColors()
    {
        for (int i = 0; i < textures.Length; i++)
        {
            tex = (Texture2D)textures[i];
            tex.SetPixels(originColors[i]);
        }
    }
}
