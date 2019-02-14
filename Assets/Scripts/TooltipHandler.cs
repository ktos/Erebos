using System.Collections;
using TMPro;
using UnityEngine;

public class TooltipHandler : MonoBehaviour
{
    private Animator animator;
    private TextMeshProUGUI tooltip;

    private static TooltipHandler _instance;

    public static TooltipHandler Instance => _instance;

    public void Start()
    {
        animator = GetComponent<Animator>();
        tooltip = GetComponent<TextMeshProUGUI>();
        _instance = this;
    }

    public void ShowTooltip(string text, int secondsToClose)
    {
        animator.SetBool("IsTooltipOpened", true);
        tooltip.text = text;

        StartCoroutine(CloseAfter(secondsToClose));
    }

    private IEnumerator CloseAfter(int seconds)
    {
        yield return new WaitForSeconds(seconds);

        animator.SetBool("IsTooltipOpened", false);
    }
}