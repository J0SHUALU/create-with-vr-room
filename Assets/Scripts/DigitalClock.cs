using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DigitalClock : MonoBehaviour
{
    [SerializeField] TMP_Text timeText;
    [SerializeField] TMP_Text secondsText;
    [SerializeField] TMP_Text dateText;
    [SerializeField] Image secondsBar;
    [SerializeField] bool twentyFourHour = true;

    int lastSecond = -1;

    void Update()
    {
        DateTime now = DateTime.Now;

        if (secondsBar != null)
            secondsBar.fillAmount = (now.Second + now.Millisecond / 1000f) / 60f;

        if (now.Second == lastSecond)
            return;
        lastSecond = now.Second;

        if (timeText != null)
            timeText.text = now.ToString(twentyFourHour ? "HH:mm" : "h:mm");
        if (secondsText != null)
            secondsText.text = ":" + now.ToString("ss");
        if (dateText != null)
            dateText.text = now.ToString("ddd dd MMM").ToUpperInvariant();
    }
}
