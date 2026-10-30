using UnityEngine;

public class ProximityTooltip : MonoBehaviour
{
    [SerializeField] CanvasGroup group;
    [SerializeField] float showDistance = 1.6f;
    [SerializeField] float fadeSpeed = 4f;

    Transform viewer;

    void Update()
    {
        if (viewer == null)
        {
            Camera main = Camera.main;
            if (main == null)
                return;
            viewer = main.transform;
        }
        float distance = Vector3.Distance(viewer.position, transform.position);
        float target = distance <= showDistance ? 1f : 0f;
        group.alpha = Mathf.MoveTowards(group.alpha, target, Time.deltaTime * fadeSpeed);
    }
}
