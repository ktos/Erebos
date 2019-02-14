using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI tooltip;

    [Header("Tooltip Text Asset")]
    public TextAsset TooltipFile;

    private Dictionary<string, string> tooltips;

    public static TooltipHandler Instance { get; private set; }

    public void Start()
    {
        animator = GetComponent<Animator>();
        tooltip = GetComponent<TextMeshProUGUI>();
        Instance = this;

        tooltips = TooltipLoader.FromFile(TooltipFile.text);
    }

    public void ShowTooltip(string text, int secondsToClose = 5)
    {
        animator.SetBool("IsTooltipOpened", true);
        if (tooltips.ContainsKey(text))
            tooltip.text = tooltips[text];
        else
            tooltip.text = text;

        StartCoroutine(CloseAfter(secondsToClose));
    }

    private IEnumerator CloseAfter(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("IsTooltipOpened", false);
    }
}

public static class TooltipLoader
{
    public static Dictionary<string, string> FromFile(string contents)
    {
        var result = new Dictionary<string, string>();

        foreach (var item in contents.Split('\n'))
        {
            var tip = item.Split('|');
            result.Add(tip[0], tip[1].TrimEnd());
        }

        return result;
    }
}