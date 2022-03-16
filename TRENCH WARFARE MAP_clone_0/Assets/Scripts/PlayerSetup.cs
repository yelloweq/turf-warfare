using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasySurvivalScripts;

public class PlayerSetup : MonoBehaviour
{
    public GameObject cameraHolder;

    public void IsLocalPlayer()
    {
        //turn on local player camera and movement
        //prevents from one player controlling both characters
        cameraHolder.GetComponent<Camera>().enabled = true;
        GetComponent<CharacterMovement>().enabled = true;

    }
}
