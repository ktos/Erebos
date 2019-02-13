﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [Header("Dialogue Text Objects")]
    public GameObject TextGroup;

    public GameObject MainText;

    public GameObject Answer1;
    public GameObject Answer2;
    public GameObject Answer3;

    [Header("Miscellaneous")]
    public TextAsset DialogueFile;

    private Dialogue dialogue;

    private void Start()
    {
        if (DialogueFile != null)
        {
            dialogue = Dialogue.FromFile(DialogueFile.text);
        }
        current = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (current == null)
                ShowLine("d1");
            else
                HideCurrent();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Answer(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Answer(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Answer(3);
        }
    }

    private DialogueLine current;

    public void Answer(int number)
    {
        if (current != null && current.Children != null && current.Children.Count > number - 1)
        {
            var action = current.Children[number - 1].ActionId;
            if (action != null)
            {
                if (action.StartsWith("d"))
                {
                    ShowLine(action);
                }
            }
            else
            {
                HideCurrent();
            }
        }
    }

    public void ShowLine(string dialogueId)
    {
        TextGroup.SetActive(true);

        if (!dialogue.Lines.ContainsKey(dialogueId))
            throw new ArgumentException("Wrong dialogue ID: " + dialogueId);

        current = dialogue.Lines[dialogueId];

        Answer1.GetComponent<TextMeshProUGUI>().text = "";
        Answer2.GetComponent<TextMeshProUGUI>().text = "";
        Answer3.GetComponent<TextMeshProUGUI>().text = "";

        MainText.GetComponent<TextMeshProUGUI>().text = current.Text;
        if (current.Children != null && current.Children.Count > 0)
        {
            Answer1.GetComponent<TextMeshProUGUI>().text = current.Children[0].Text;
        }

        if (current.Children != null && current.Children.Count > 1)
        {
            Answer2.GetComponent<TextMeshProUGUI>().text = current.Children[1].Text;
        }

        if (current.Children != null && current.Children.Count > 2)
        {
            Answer3.GetComponent<TextMeshProUGUI>().text = current.Children[2].Text;
        }

        if (current.Children == null || current.Children.Count == 0)
        {
            StartCoroutine(CloseAfter(5));
        }
    }

    public void HideCurrent()
    {
        current = null;
        TextGroup.SetActive(false);
    }

    private IEnumerator CloseAfter(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        HideCurrent();
    }
}