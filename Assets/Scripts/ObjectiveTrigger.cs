using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [SerializeField] ObjectiveTracker tracker;
    [SerializeField] string objectiveId;

    public void Report()
    {
        if (tracker != null)
            tracker.Complete(objectiveId);
    }
}
