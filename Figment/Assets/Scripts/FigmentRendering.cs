using UnityEngine;

public class FigmentRendering : MonoBehaviour 
{
    public Material screenRenderMaterial;
    
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, screenRenderMaterial);
    }
}
