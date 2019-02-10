using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KayaMovement : MonoBehaviour
{
    public float speed = 0.1f;
    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 5)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed);
        }
    }
}