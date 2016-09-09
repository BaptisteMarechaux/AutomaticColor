using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorManager : MonoBehaviour {

    public Texture[] textures;

    public Texture2D tex;

    public Color[][] originColors;

    public Renderer quadRenderer;
    // Use this for initialization

    public float angleH = 120;
    public Slider hueSlider;

    private bool textureLoaded = false;

    void Start ()
    {
        originColors = new Color[textures.Length][];
	    for(int i=0;i<textures.Length;i++)
        {
            tex = (Texture2D)textures[i];
            originColors[i] = tex.GetPixels();
        }
        hueSlider.value = angleH;
	}
	
    public void updateHue()
    {
        angleH = hueSlider.value;
        Colorize();
    }

	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Colorize()
    {
        if (textureLoaded)
        {
            ResetColors();
            tex = (Texture2D)quadRenderer.material.mainTexture;
            Color[] cols = tex.GetPixels();
            //Debug.Log(angleH / 360.0f);
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = hsl2rgb(angleH, 1, cols[i].g);
                /*if(cols[i].g > 0.5)
                {
                    cols[i] = new Color(cols[i].r, cols[i].g-0.1f, cols[i].b,1);
                }*/
            }
            tex.SetPixels(cols);
            tex.Apply();
        }
    }

    private Color hsl2rgb(float h, int s, float l)
    {
        Color col = Color.black;
        float q, p;

        h /= 360;
        q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        p = 2 * l - q;

        col.r = hue2rgb(p, q, h + 1.0f/3);
        col.g = hue2rgb(p, q, h);
        col.b = hue2rgb(p, q, h - 1.0f/3);

        return col;
    }

    float hue2rgb(float p, float q, float t)
    {
        if (t < 0) t++;
        if (t > 1) t--;
        if (t < 1.0f/6) return p + (q - p) * 6 * t;
        if (t < 1.0f/2) return q;
        if (t < 2.0f/3) return p + (q - p) * (2.0f/3 - t) * 6;
        return p;
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
        textureLoaded = true;
        ResetColors();
    }

    void OnApplicationQuit()
    {
        ResetColors();
    }

    public void FindSky()
    {
        tex = (Texture2D)quadRenderer.material.mainTexture;

        Color[] cols = tex.GetPixels(); //Colors

        bool[] canColor = new bool[cols.Length];

        int height = quadRenderer.material.mainTexture.height;
        int width = quadRenderer.material.mainTexture.width;

        for(int i=0;i<height-1;i++)
        {
            for(int j=0;j<width;j++)
            {
                if(cols[i+j].r/cols[i+width+j].r < 0.75f  /*&& cols[i + j].r < cols[i + width + j].r*/)
                {
                    canColor[i] = true;
                }
                else
                {
                    canColor[i] = false;
                }
            }
        }

        for(int i=0;i<cols.Length;i++)
        {
            if (canColor[i])
                cols[i] = hsl2rgb(144, 1, cols[i].g);
        }

        tex.SetPixels(cols);
        tex.Apply();

        quadRenderer.material.mainTexture = tex;

    }
}
