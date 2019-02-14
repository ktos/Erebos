using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //var sceneSwitcher = FindObjectOfType<SceneSwitcher>();

        //if (sceneSwitcher != null)
        //    sceneSwitcher.FadedLoadScene("Scene01");

        TooltipHandler.Instance?.ShowTooltip("Hello <b>Megan</b>!", 10);
    }
}