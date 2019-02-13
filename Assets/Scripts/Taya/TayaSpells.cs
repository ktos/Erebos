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

    [Header("Player")]
    public GameObject Player;

    [Header("Prefabs")]
    public Transform PlatformPrefab;

    public void Charge()
    {
        Energy += ChargeRate;
    }

    public void CreatePlatform()
    {
        if (Energy < CreatePlatformCost)
            return;

        Energy -= CreatePlatformCost;

        Instantiate(PlatformPrefab, Player.transform.position + Vector3.forward, Quaternion.identity);
    }

    public void Start()
    {
        InvokeRepeating("Charge", 0, 1.0f);
    }

    public void Update()
    {
    }
}