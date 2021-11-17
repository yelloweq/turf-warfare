using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySurvivalScripts;

public class PlayerSetup : MonoBehaviour
{
    public GameObject cameraHolder;

    public void IsLocalPlayer()
    {
        cameraHolder.GetComponent<Camera>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;

    }
}
