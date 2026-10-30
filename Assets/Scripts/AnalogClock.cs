using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    [SerializeField] Transform hourHand;
    [SerializeField] Transform minuteHand;
    [SerializeField] Transform secondHand;
    [SerializeField] bool sweepSeconds = true;
    [SerializeField] float direction = -1f;
    [SerializeField] AudioSource tickSource;

    int lastSecond = -1;

    void Update()
    {
        DateTime now = DateTime.Now;
        float seconds = now.Second + (sweepSeconds ? now.Millisecond / 1000f : 0f);
        float minutes = now.Minute + seconds / 60f;
        float hours = now.Hour % 12 + minutes / 60f;

        hourHand.localRotation = Quaternion.Euler(0f, 0f, direction * hours * 30f);
        minuteHand.localRotation = Quaternion.Euler(0f, 0f, direction * minutes * 6f);
        secondHand.localRotation = Quaternion.Euler(0f, 0f, direction * seconds * 6f);

        if (tickSource != null && now.Second != lastSecond)
        {
            lastSecond = now.Second;
            tickSource.Play();
        }
    }
}
