using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class TurnModeToggle : MonoBehaviour
{
    public void SetSnapTurn(bool snap)
    {
        foreach (SnapTurnProvider provider in FindObjectsByType<SnapTurnProvider>(FindObjectsInactive.Include))
            provider.gameObject.SetActive(snap);
        foreach (ContinuousTurnProvider provider in FindObjectsByType<ContinuousTurnProvider>(FindObjectsInactive.Include))
            provider.gameObject.SetActive(!snap);
    }
}
