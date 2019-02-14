using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActionManager : MonoBehaviour
{
    [Header("Taya Object")]
    public GameObject Taya;

    private TayaSpells tayaSpells;

    public Dictionary<string, Func<bool>> Actions;

    private static DialogueActionManager _instance;
    public static DialogueActionManager Instance => _instance;

    // Start is called before the first frame update
    private void Start()
    {
        _instance = this;

        Actions = new Dictionary<string, Func<bool>>();

        tayaSpells = Taya.GetComponent<TayaSpells>();

        Actions.Add("taya_create_platform", () => tayaSpells.CreatePlatform());
    }

    public bool RunAction(string actionId)
    {
        if (Actions.ContainsKey(actionId))
        {
            return Actions[actionId].Invoke();
        }
        else
        {
            throw new ArgumentException("Action " + actionId + " does not exist!");
        }
    }
}