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
        Color[] cols = tex.GetPixels();

        for(int i=0;i<cols.Length;i++)
        {
            if(cols[i].g > 0.5)
            {
                cols[i] = new Color(cols[i].r, cols[i].g-0.1f, cols[i].b,1);
            }
        }
        tex.SetPixels(cols);
        tex.Apply();
    }

    public void ResetColors()
    {
        for (int i = 0; i < textures.Length; i++)
        {
            tex = (Texture2D)textures[i];
            tex.SetPixels(originColors[i]);
            tex.Apply();
        }
        
    }

    public void switchToTexture(int index)
    {
        quadRenderer.material.mainTexture = textures[index];
    }

    void OnApplicationQuit()
    {
        ResetColors();
    }
}
