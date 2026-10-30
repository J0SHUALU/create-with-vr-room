using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] bool yawOnly = true;

    Transform target;

    void LateUpdate()
    {
        if (target == null)
        {
            Camera main = Camera.main;
            if (main == null)
                return;
            target = main.transform;
        }
        Vector3 direction = transform.position - target.position;
        if (yawOnly)
            direction.y = 0f;
        if (direction.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }
}
