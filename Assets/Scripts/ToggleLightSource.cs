using UnityEngine;

public class ToggleLightSource : MonoBehaviour
{
    [SerializeField] Light targetLight;
    [SerializeField] AudioSource clickSound;
    [SerializeField] Renderer glowRenderer;
    [SerializeField] Color glowColor = new Color(1f, 0.9f, 0.6f);

    MaterialPropertyBlock block;
    static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        block = new MaterialPropertyBlock();
        Apply();
    }

    public void Toggle()
    {
        targetLight.enabled = !targetLight.enabled;
        if (clickSound != null)
            clickSound.Play();
        Apply();
    }

    void Apply()
    {
        if (glowRenderer == null)
            return;
        glowRenderer.GetPropertyBlock(block);
        block.SetColor(EmissionId, targetLight.enabled ? glowColor * 2f : Color.black);
        glowRenderer.SetPropertyBlock(block);
    }
}
