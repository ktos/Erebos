using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //var sceneSwitcher = FindObjectOfType<SceneSwitcher>();

        //if (sceneSwitcher != null)
        //    sceneSwitcher.FadedLoadScene("Scene01");

        if (other.CompareTag("Player"))
            TooltipHandler.Instance?.ShowTooltip("hello", 5);
    }
}