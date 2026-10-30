using UnityEngine;
using UnityEngine.Events;

public class ProjectorSwitch : MonoBehaviour
{
    [SerializeField] ParticleSystem beam;
    [SerializeField] Light beamLight;
    [SerializeField] AudioSource humSource;
    [SerializeField] UnityEvent onTurnedOn = new UnityEvent();

    bool isOn;

    void Start()
    {
        Apply();
    }

    public void Toggle()
    {
        isOn = !isOn;
        Apply();
        if (isOn)
            onTurnedOn.Invoke();
    }

    void Apply()
    {
        if (beam != null)
        {
            if (isOn)
                beam.Play();
            else
                beam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (beamLight != null)
            beamLight.enabled = isOn;
        if (humSource != null)
        {
            if (isOn)
                humSource.Play();
            else
                humSource.Stop();
        }
    }
}
