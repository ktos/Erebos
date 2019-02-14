using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float Irritation = 0;
    public float Insanity = 0;
    public bool IsCheatingEnabled;

    public static bool IsCheater = false;

    private void Start()
    {
        IsCheater = IsCheatingEnabled;
    }
}