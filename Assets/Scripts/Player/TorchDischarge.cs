using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchDischarge : MonoBehaviour
{
    public float Battery = 100.0f;
    public float DischargeRate = 0.0001f;
    public bool TorchEnabled = true;
    private LightFlicker flicker;
    private Light torch;

    private void Start()
    {
        InvokeRepeating("Discharge", 0, 1.0f);
        flicker = GetComponent<LightFlicker>();
        torch = GetComponent<Light>();
    }

    private void Discharge()
    {
        if (!TorchEnabled)
            return;

        Battery -= DischargeRate;

        if (Battery < 50)
        {
            flicker.StopFlickering = false;
        }

        if (Battery < 40)
        {
            flicker.MaxReduction = 20;
        }

        if (Battery < 30)
        {
            flicker.MaxReduction = 50;
        }

        if (Battery < 20)
        {
            flicker.MaxReduction = 100;
        }

        if (Battery < 10)
        {
            flicker.MaxReduction = 200;
        }

        if (Battery < 0)
        {
            torch.enabled = false;
        }
    }

    public void TurnOn()
    {
        if (Battery <= 0)
            return;

        TorchEnabled = true;
        torch.enabled = true;
        flicker.enabled = true;
    }

    public void TurnOff()
    {
        TorchEnabled = false;
        torch.enabled = false;
        flicker.enabled = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (TorchEnabled)
                TurnOff();
            else
                TurnOn();
        }
    }
}