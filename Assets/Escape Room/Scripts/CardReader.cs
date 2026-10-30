using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CardReader : XRSocketInteractor
{
    [Header("Reader Data")]
    public float allowedUprightErrorRange = 0.2f;
    public float minimumSwipeDistance = 0.15f;
    public GameObject visualLockToHide;
    public DoorHandle handleToEnable;
    public Renderer redLight;
    public Renderer greenLight;
    public Material lightOffMaterial;
    public AudioSource readerSource;
    public AudioClip acceptedClip;
    public AudioClip rejectedClip;

    private Transform m_KeycardTransform;
    private Vector3 m_HoverEntry;
    private bool m_SwipeIsValid;
    private bool m_Unlocked;
    private Material m_RedOn;
    private Material m_GreenOn;

    protected override void Awake()
    {
        base.Awake();
        m_RedOn = redLight.sharedMaterial;
        m_GreenOn = greenLight.sharedMaterial;
        redLight.sharedMaterial = lightOffMaterial;
        greenLight.sharedMaterial = lightOffMaterial;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return false;
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        m_KeycardTransform = args.interactableObject.transform;
        m_HoverEntry = m_KeycardTransform.position;
        m_SwipeIsValid = true;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        if (m_KeycardTransform == null)
            return;

        Vector3 entryToExit = m_KeycardTransform.position - m_HoverEntry;

        if (!m_Unlocked)
        {
            if (m_SwipeIsValid && entryToExit.y < -minimumSwipeDistance)
                Unlock();
            else
                Reject();
        }

        m_KeycardTransform = null;
    }

    private void Update()
    {
        if (m_KeycardTransform != null)
        {
            float dot = Vector3.Dot(m_KeycardTransform.forward, Vector3.up);

            if (Mathf.Abs(dot) < 1 - allowedUprightErrorRange)
                m_SwipeIsValid = false;
        }
    }

    private void Unlock()
    {
        m_Unlocked = true;
        visualLockToHide.SetActive(false);
        handleToEnable.enabled = true;
        redLight.sharedMaterial = lightOffMaterial;
        greenLight.sharedMaterial = m_GreenOn;
        if (readerSource != null && acceptedClip != null)
            readerSource.PlayOneShot(acceptedClip);
    }

    private void Reject()
    {
        redLight.sharedMaterial = m_RedOn;
        CancelInvoke(nameof(RedOff));
        Invoke(nameof(RedOff), 0.8f);
        if (readerSource != null && rejectedClip != null)
            readerSource.PlayOneShot(rejectedClip);
    }

    private void RedOff()
    {
        if (!m_Unlocked)
            redLight.sharedMaterial = lightOffMaterial;
    }
}
