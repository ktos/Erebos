using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveChanger : MonoBehaviour
{
    private float progress = 1.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (progress <= 0.0f)
            Destroy(this.gameObject);

        //Debug.Log(progress);
        progress -= 0.01f;
        gameObject.GetComponent<Renderer>().material.SetFloat("_Progress", progress);
    }
}