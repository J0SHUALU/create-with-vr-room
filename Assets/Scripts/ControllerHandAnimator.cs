using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerHandAnimator : MonoBehaviour
{
    [SerializeField] InputActionProperty gripAction;
    [SerializeField] InputActionProperty triggerAction;
    [SerializeField] InputActionProperty thumbTouchAction;
    [SerializeField] Transform[] indexJoints;
    [SerializeField] Transform[] gripJoints;
    [SerializeField] Transform[] thumbJoints;
    [SerializeField] Vector3 fingerCurlAxis = Vector3.right;
    [SerializeField] Vector3 thumbCurlAxis = Vector3.forward;
    [SerializeField] float indexCurl = 65f;
    [SerializeField] float gripCurl = 75f;
    [SerializeField] float thumbCurl = 30f;
    [SerializeField] float responsiveness = 20f;

    Quaternion[] indexRest;
    Quaternion[] gripRest;
    Quaternion[] thumbRest;
    float grip;
    float trigger;
    float thumb;

    public float Grip => grip;
    public float Trigger => trigger;

    void Awake()
    {
        indexRest = CacheRest(indexJoints);
        gripRest = CacheRest(gripJoints);
        thumbRest = CacheRest(thumbJoints);
    }

    void OnEnable()
    {
        EnableAction(gripAction);
        EnableAction(triggerAction);
        EnableAction(thumbTouchAction);
    }

    void Update()
    {
        float step = 1f - Mathf.Exp(-responsiveness * Time.deltaTime);
        grip = Mathf.Lerp(grip, ReadValue(gripAction), step);
        trigger = Mathf.Lerp(trigger, ReadValue(triggerAction), step);
        thumb = Mathf.Lerp(thumb, Mathf.Max(ReadValue(thumbTouchAction), grip), step);

        Pose(indexJoints, indexRest, fingerCurlAxis, trigger * indexCurl);
        Pose(gripJoints, gripRest, fingerCurlAxis, grip * gripCurl);
        Pose(thumbJoints, thumbRest, thumbCurlAxis, thumb * thumbCurl);
    }

    static Quaternion[] CacheRest(Transform[] joints)
    {
        Quaternion[] rest = new Quaternion[joints.Length];
        for (int i = 0; i < joints.Length; i++)
            rest[i] = joints[i].localRotation;
        return rest;
    }

    static void Pose(Transform[] joints, Quaternion[] rest, Vector3 axis, float angle)
    {
        for (int i = 0; i < joints.Length; i++)
            joints[i].localRotation = rest[i] * Quaternion.AngleAxis(angle, axis);
    }

    static void EnableAction(InputActionProperty property)
    {
        if (property.action != null)
            property.action.Enable();
    }

    static float ReadValue(InputActionProperty property)
    {
        InputAction action = property.action;
        if (action == null || !action.enabled)
            return 0f;
        if (action.activeControl != null && action.activeControl.valueType == typeof(bool))
            return action.IsPressed() ? 1f : 0f;
        return action.ReadValue<float>();
    }
}
