using UnityEngine;

[RequireComponent(typeof(HingeJoint))]
public class DoorCreak : MonoBehaviour
{
    [SerializeField] AudioSource creakSource;
    [SerializeField] AudioSource latchSource;
    [SerializeField] float creakSpeedThreshold = 15f;
    [SerializeField] float closedAngle = 2f;

    HingeJoint hinge;
    bool wasOpen;

    public float Angle => hinge != null ? hinge.angle : 0f;
    public bool IsOpen => Mathf.Abs(Angle) > closedAngle;

    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
    }

    void Update()
    {
        float speed = Mathf.Abs(hinge.velocity);
        if (creakSource != null)
        {
            float target = Mathf.InverseLerp(creakSpeedThreshold, creakSpeedThreshold * 6f, speed);
            creakSource.volume = Mathf.MoveTowards(creakSource.volume, target, Time.deltaTime * 4f);
            if (target > 0f && !creakSource.isPlaying)
                creakSource.Play();
            else if (creakSource.volume <= 0.01f && creakSource.isPlaying)
                creakSource.Stop();
        }

        bool open = IsOpen;
        if (wasOpen && !open && latchSource != null)
            latchSource.Play();
        wasOpen = open;
    }
}
