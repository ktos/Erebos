using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TayaSpells : MonoBehaviour
{
    [Header("Taya Magic")]
    public float Energy = 0;

    public float ChargeRate = 0.1f;

    [Header("Spells Cost")]
    public float CreatePlatformCost = 100.0f;

    public float CreateDecoyCost = 500.0f;

    [Header("Player")]
    public GameObject Player;

    public GameObject Sparks;

    [Header("Prefabs")]
    public Transform PlatformPrefab;

    public Transform DecoyPrefab;

    public void Charge()
    {
        Energy += ChargeRate;
    }

    public bool CreatePlatform()
    {
        if (Energy < CreatePlatformCost)
            return false;

        TurnOnSparks();
        Energy -= CreatePlatformCost;

        Instantiate(PlatformPrefab, Player.transform.position + (Player.transform.forward * 2), Player.transform.rotation);

        return true;
    }

    public bool CreateDecoy()
    {
        if (Energy < CreatePlatformCost)
            return false;

        TurnOnSparks();
        Energy -= CreatePlatformCost;

        Instantiate(DecoyPrefab, Player.transform.position + (Player.transform.forward * 2), Player.transform.rotation);

        return true;
    }

    public void Start()
    {
        InvokeRepeating("Charge", 0, 1.0f);
    }

    public IEnumerator TurnOffSparks()
    {
        yield return new WaitForSeconds(1);
        Sparks.SetActive(false);
    }

    public void TurnOnSparks()
    {
        Sparks.SetActive(true);
        StartCoroutine(TurnOffSparks());
    }

    public void Update()
    {
        if (PlayerStateManager.IsCheater && Input.GetKeyDown(KeyCode.F1))
        {
            Energy += CreatePlatformCost;
            CreatePlatform();
        }

        if (PlayerStateManager.IsCheater && Input.GetKeyDown(KeyCode.F2))
        {
            Energy += CreateDecoyCost;
            CreateDecoy();
        }
    }
}