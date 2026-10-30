using UnityEngine;

public class DoorObjective : MonoBehaviour
{
    [SerializeField] DoorCreak door;
    [SerializeField] ObjectiveTracker tracker;
    [SerializeField] string objectiveId = "door";
    [SerializeField] float openAngle = 45f;

    bool reported;

    void Update()
    {
        if (reported || door == null || tracker == null)
            return;
        if (Mathf.Abs(door.Angle) >= openAngle)
        {
            reported = true;
            tracker.Complete(objectiveId);
        }
    }
}
