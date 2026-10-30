using System.Collections;
using TMPro;
using UnityEngine;

public class NumberPad : MonoBehaviour
{
    public string sequence = "2580";
    public KeycardSpawner cardSpawner;
    public TextMeshProUGUI inputDisplayText;
    public AudioSource feedbackSource;
    public AudioClip pressClip;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public Color normalColor = Color.white;
    public Color correctColor = new Color(0.24f, 0.86f, 0.59f);
    public Color wrongColor = new Color(1f, 0.33f, 0.33f);

    private string m_CurrentEnteredCode = "";
    private bool m_Locked;

    private void Start()
    {
        ResetDisplay();
    }

    public void ButtonPressed(int valuePressed)
    {
        if (m_Locked)
            return;

        m_CurrentEnteredCode += valuePressed;
        inputDisplayText.text = m_CurrentEnteredCode;
        Play(pressClip);

        if (m_CurrentEnteredCode.Length == sequence.Length)
        {
            if (m_CurrentEnteredCode == sequence)
            {
                m_Locked = true;
                inputDisplayText.color = correctColor;
                inputDisplayText.text = "OPEN";
                Play(correctClip);
                cardSpawner.SpawnKeycard();
            }
            else
            {
                StartCoroutine(WrongCode());
            }
        }
    }

    private IEnumerator WrongCode()
    {
        m_Locked = true;
        inputDisplayText.color = wrongColor;
        inputDisplayText.text = "ERROR";
        Play(wrongClip);
        yield return new WaitForSeconds(1f);
        ResetDisplay();
        m_Locked = false;
    }

    private void ResetDisplay()
    {
        m_CurrentEnteredCode = "";
        inputDisplayText.color = normalColor;
        inputDisplayText.text = new string('-', sequence.Length);
    }

    private void Play(AudioClip clip)
    {
        if (feedbackSource != null && clip != null)
            feedbackSource.PlayOneShot(clip);
    }
}
