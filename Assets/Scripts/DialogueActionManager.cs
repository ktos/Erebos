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
    public static DialogueActionManager Instance { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Instance = this;

        Actions = new Dictionary<string, Func<bool>>();

        tayaSpells = Taya.GetComponent<TayaSpells>();

        Actions.Add("taya_create_platform", () => tayaSpells.CreatePlatform());
        Actions.Add("taya_create_decoy", () => tayaSpells.CreateDecoy());
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