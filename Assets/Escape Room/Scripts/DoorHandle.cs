using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class DoorHandle : XRBaseInteractable
{
    [Header("Door Data")]
    public Transform draggedTransform;
    public Vector3 localDragDirection = new Vector3(-1, 0, 0);
    public float dragDistance = 0.8f;
    public int doorWeight = 20;
    public AudioSource slideSource;

    private Vector3 m_StartPosition;
    private Vector3 m_EndPosition;
    private Vector3 m_WorldDragDirection;

    private void Start()
    {
        m_WorldDragDirection = transform.TransformDirection(localDragDirection).normalized;
        m_StartPosition = draggedTransform.position;
        m_EndPosition = m_StartPosition + m_WorldDragDirection * dragDistance;
    }

    public override bool IsSelectableBy(IXRSelectInteractor interactor)
    {
        return enabled && base.IsSelectableBy(interactor);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase != XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            return;

        float speed = 0f;

        if (isSelected)
        {
            var interactorTransform = firstInteractorSelecting.GetAttachTransform(this);
            Vector3 selfToInteractor = interactorTransform.position - transform.position;
            float forceInDirectionOfDrag = Vector3.Dot(selfToInteractor, m_WorldDragDirection);
            bool dragToEnd = forceInDirectionOfDrag > 0.0f;
            float absoluteForce = Mathf.Abs(forceInDirectionOfDrag);
            speed = absoluteForce / Time.deltaTime / doorWeight;

            Vector3 target = dragToEnd ? m_EndPosition : m_StartPosition;
            Vector3 before = draggedTransform.position;
            draggedTransform.position = Vector3.MoveTowards(draggedTransform.position, target, speed * Time.deltaTime);
            speed = (draggedTransform.position - before).magnitude / Mathf.Max(Time.deltaTime, 0.0001f);

            if (firstInteractorSelecting is XRBaseInputInteractor input && speed > 0.01f)
                input.SendHapticImpulse(Mathf.Clamp01(speed), Time.deltaTime);
        }

        if (slideSource != null)
        {
            if (speed > 0.02f && !slideSource.isPlaying)
                slideSource.Play();
            else if (speed <= 0.02f && slideSource.isPlaying)
                slideSource.Pause();
        }
    }
}
