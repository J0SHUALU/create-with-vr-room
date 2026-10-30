using System;
using TMPro;
using UnityEngine;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text dateText;
    [SerializeField] bool twentyFourHour = true;

    int lastSecond = -1;

    void Update()
    {
        DateTime now = DateTime.Now;
        if (now.Second == lastSecond)
            return;
        lastSecond = now.Second;

        if (timeText != null)
            timeText.text = now.ToString(twentyFourHour ? "HH:mm" : "h:mm");
        if (secondsText != null)
            secondsText.text = now.ToString("ss");
        if (dateText != null)
            dateText.text = now.ToString("ddd dd MMM").ToUpperInvariant();
    }
}
