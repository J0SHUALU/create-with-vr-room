using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveTracker : MonoBehaviour
{
    [Serializable]
    public class Objective
    {
        public string id;
        public string label;
        [NonSerialized] public bool done;
    }

    [SerializeField] List<Objective> objectives = new List<Objective>();
    [SerializeField] TMP_Text listText;
    [SerializeField] TMP_Text progressText;
    [SerializeField] AudioSource completeSound;
    [SerializeField] UnityEvent onAllComplete = new UnityEvent();
    [SerializeField] string doneColor = "#FFD400";
    [SerializeField] string pendingColor = "#A8A8A0";

    void Start()
    {
        Refresh();
    }

    public void Complete(string id)
    {
        Objective objective = objectives.Find(o => o.id == id);
        if (objective == null || objective.done)
            return;
        objective.done = true;
        if (completeSound != null)
            completeSound.Play();
        Refresh();
        if (objectives.TrueForAll(o => o.done))
            onAllComplete.Invoke();
    }

    void Refresh()
    {
        int done = 0;
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        for (int i = 0; i < objectives.Count; i++)
        {
            Objective objective = objectives[i];
            if (objective.done)
                done++;
            string color = objective.done ? doneColor : pendingColor;
            string mark = objective.done ? "■" : "□";
            builder.Append("<color=").Append(color).Append(">").Append(mark).Append("  ")
                .Append((i + 1).ToString("00")).Append("  ").Append(objective.label).Append("</color>\n");
        }
        if (listText != null)
            listText.text = builder.ToString();
        if (progressText != null)
            progressText.text = done + "/" + objectives.Count;
    }
}
