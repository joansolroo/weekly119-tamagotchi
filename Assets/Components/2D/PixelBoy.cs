using UnityEngine;
using System.Collections;
using Unity.Collections;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/PixelBoy")]
public class PixelBoy : MonoBehaviour
{
    public int height = 720;
    [ReadOnly] public int width;
    public float ratio = 16f / 9;

    

    public Camera cam;

    protected void Start()
    {
    	cam = GetComponent<Camera>();
    	
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }
    void Update() {

        //ratio = ((float)cam.pixelHeight / (float)cam.pixelWidth);
        width = Mathf.RoundToInt(height * ratio);
        cam.orthographicSize = height / 2;



    }
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture buffer = RenderTexture.GetTemporary(width, height, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}