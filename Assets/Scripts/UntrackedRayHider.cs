using UnityEngine;

public class UntrackedRayHider : MonoBehaviour
{
    public Transform head;
    public float minDistance = 0.08f;

    private LineRenderer[] m_Lines;
    private bool m_Hidden;

    private void Awake()
    {
        m_Lines = GetComponentsInChildren<LineRenderer>(true);
        if (head == null && Camera.main != null)
            head = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (head == null)
            return;

        bool hide = Vector3.Distance(transform.position, head.position) < minDistance;
        if (hide == m_Hidden)
            return;

        m_Hidden = hide;
        foreach (var line in m_Lines)
        {
            if (line != null)
                line.forceRenderingOff = hide;
        }
    }
}
