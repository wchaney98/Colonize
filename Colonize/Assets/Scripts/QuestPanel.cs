using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class QuestPanel : MonoBehaviour
{
    public Text QuestPanelBodyText;

    private void Start()
    {
        OnQuestsUpdated(null);
        QuestManager.Instance.QuestCompletionListener += OnQuestsUpdated;
    }

    private void OnQuestsUpdated(QuestManager.Quest q)
    {
        if (QuestPanelBodyText != null)
        {
            StringBuilder sb = new StringBuilder();
            QuestManager.Instance.CurrentQuests.ForEach(x =>
            {
                sb.AppendLine("<color=grey>" + x.Title + "</color>: " + x.Description + " " + "<color=white>" + x.StatusString + "</color>")
                  .AppendLine();
            });
            QuestPanelBodyText.text = sb.ToString();
        }
    }
}
