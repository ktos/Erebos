using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActionManager : MonoBehaviour
{
    public GameObject Taya;

    public Dictionary<string, Action> Actions;

    // Start is called before the first frame update
    private void Start()
    {
        Actions = new Dictionary<string, Action>();
    }
}