using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySurvivalScripts;

public class PlayerSetup : MonoBehaviour
{
    public GameObject cameraHolder;

    public void IsLocalPlayer()
    {
        cameraHolder.SetActive(true);

        Debug.Log("camera activated");
        GetComponent<PlayerMovement>().enabled = true;
        Debug.Log("movement activated");
    }
}
