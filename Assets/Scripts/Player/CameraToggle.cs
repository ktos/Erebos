﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    public Color ambientOverview = Color.grey;
    public Color ambientFpp = Color.black;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            var camera = FindObjectOfType(typeof(CameraFollow)) as CameraFollow;
            camera.overview = !camera.overview;

            GameObject.FindGameObjectsWithTag("Ceiling").ToList().ForEach(z => z.GetComponent<MeshRenderer>().enabled = !camera.overview);

            RenderSettings.ambientLight = camera.overview ? ambientOverview : ambientFpp;
        }
    }
}