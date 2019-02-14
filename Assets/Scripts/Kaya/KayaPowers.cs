using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaPowers : MonoBehaviour
{
    [Header("Kaya Magic")]
    public float Energy = 0;

    public float ChargeRate = 0.1f;

    public GameObject InfluenceSphere;

    public float InfluenceSize = 0;

    public float InfluenceSizeGrowthRate = 0.1f;

    [Header("Player")]
    public GameObject Player;

    private void UpdateInfluenceSize()
    {
        InfluenceSphere.transform.localScale = new Vector3(InfluenceSize, InfluenceSize, InfluenceSize);
    }

    public void Charge()
    {
        Energy += ChargeRate;
        InfluenceSize += InfluenceSizeGrowthRate;

        UpdateInfluenceSize();
    }

    public void Start()
    {
        InvokeRepeating("Charge", 0, 1.0f);
    }
}