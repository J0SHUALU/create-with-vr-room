using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRBaseInteractable))]
public class HoverHighlight : MonoBehaviour
{
    [SerializeField] Color highlightColor = new Color(1f, 0.83f, 0f);
    [SerializeField] float intensity = 0.6f;

    XRBaseInteractable interactable;
    Renderer[] renderers;
    MaterialPropertyBlock block;
    static readonly int EmissionId = Shader.PropertyToID("_EmissionColor");

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        renderers = GetComponentsInChildren<Renderer>();
        block = new MaterialPropertyBlock();
        foreach (Renderer target in renderers)
            foreach (Material material in target.materials)
                material.EnableKeyword("_EMISSION");
    }

    void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
        interactable.selectEntered.RemoveListener(OnSelectEntered);
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (!interactable.isSelected)
            SetGlow(true);
    }

    void OnHoverExited(HoverExitEventArgs args)
    {
        if (!interactable.isHovered)
            SetGlow(false);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        SetGlow(false);
    }

    void SetGlow(bool on)
    {
        Color color = on ? highlightColor * intensity : Color.black;
        foreach (Renderer target in renderers)
        {
            target.GetPropertyBlock(block);
            block.SetColor(EmissionId, color);
            target.SetPropertyBlock(block);
        }
    }
}
