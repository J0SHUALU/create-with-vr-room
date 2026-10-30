using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class BalloonInflator : XRGrabInteractable
{
    [Header("Balloon Data")]
    public Transform attachPoint;
    public Balloon balloonPrefab;

    private Balloon m_BalloonInstance;
    private XRBaseInputInteractor m_Controller;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        m_BalloonInstance = Instantiate(balloonPrefab, attachPoint);
        m_Controller = args.interactorObject as XRBaseInputInteractor;
        if (m_Controller != null)
            m_Controller.SendHapticImpulse(1f, 0.5f);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        if (m_BalloonInstance != null)
            Destroy(m_BalloonInstance.gameObject);
        m_Controller = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && m_Controller != null && m_BalloonInstance != null)
        {
            m_BalloonInstance.transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 4.0f, m_Controller.activateInput.ReadValue());
        }
    }
}
