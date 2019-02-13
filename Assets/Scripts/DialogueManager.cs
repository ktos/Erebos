using System;
using System.Collections.Generic;
using UnityEngine;

internal class DialogueLine
{
    public string Text { get; set; }
    public IList<DialogueLine> Children { get; set; }
    public string ActionId { get; set; }
}

internal class Dialogue
{
    public Dictionary<string, DialogueLine> Lines;

    public static Dialogue FromFile(string contents)
    {
        Dialogue parsed = new Dialogue();
        parsed.Lines = new Dictionary<string, DialogueLine>();

        DialogueLine current = new DialogueLine();
        current.Children = new List<DialogueLine>();
        string currentId = null;

        var lines = contents.Split('\n');
        foreach (var item in lines)
        {
            if (!item.StartsWith(" "))
            {
                if (currentId != null)
                {
                    parsed.Lines.Add(currentId, current);

                    currentId = string.Empty;
                    current = new DialogueLine();
                    current.Children = new List<DialogueLine>();
                }

                var currentSplitted = item.Trim().Split('|');

                currentId = currentSplitted[0];
                current.Text = currentSplitted[1];

                if (currentSplitted.Length > 2)
                    current.ActionId = currentSplitted[2].TrimEnd();
            }
            else
            {
                var childSplitted = item.Trim().Split('|');
                var childText = childSplitted[0];
                string childAction = null;

                if (childSplitted.Length > 1)
                    childAction = childSplitted[1].TrimEnd();

                current.Children.Add(new DialogueLine { Text = childText, ActionId = childAction });
            }
        }
        if (currentId != null)
        {
            parsed.Lines.Add(currentId, current);
        }

        return parsed;
    }
}

public class DialogueManager : MonoBehaviour
{
    [Header("Miscellaneous")]
    public TextAsset dialogueFile;

    private Dialogue dialogue;

    private void Start()
    {
        if (dialogueFile != null)
        {
            dialogue = Dialogue.FromFile(dialogueFile.text);
        }
    }
}