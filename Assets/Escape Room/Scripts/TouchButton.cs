using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TouchButton : XRBaseInteractable
{
    [Header("Button Data")]
    public int buttonNumber;
    public NumberPad linkedNumberPad;
    public Material normalMaterial;
    public Material touchedMaterial;

    private Renderer m_RendererToChange;

    protected override void Awake()
    {
        base.Awake();
        m_RendererToChange = GetComponent<Renderer>();
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        if (interactorsHovering.Count > 1)
            return;

        m_RendererToChange.material = touchedMaterial;
        linkedNumberPad.ButtonPressed(buttonNumber);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (interactorsHovering.Count == 0)
            m_RendererToChange.material = normalMaterial;
    }
}
