using UnityEngine;

public class KeycardSpawner : MonoBehaviour
{
    public GameObject keycard;
    public Transform spawnPoint;

    private void Awake()
    {
        keycard.SetActive(false);
    }

    public void SpawnKeycard()
    {
        if (keycard.activeSelf)
            return;

        keycard.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        keycard.SetActive(true);
    }
}
