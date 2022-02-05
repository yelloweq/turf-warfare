using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using EasySurvivalScripts;

public class PlayerSetup : MonoBehaviour
{
    public GameObject cameraHolder;
    public GameObject CanonObject;


    private void Start()
    {
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            cameraHolder.GetComponent<Camera>().enabled = true;
            GetComponent<PlayerMovement>().enabled = true;
            CanonObject.GetComponent<CannonController>().enabled = true;
            CanonObject.GetComponent<DrawProjection>().enabled = true;
        }
        else
        {
            cameraHolder.GetComponent<Camera>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            CanonObject.GetComponent<CannonController>().enabled = false;
            CanonObject.GetComponent<DrawProjection>().enabled = false;
        }

        CanonObject.transform.parent = null;
    }







    /*public void IsLocalPlayer()
    {
        //turn on local player camera and movement
        //prevents from one player controlling both characters
        cameraHolder.GetComponent<Camera>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;

    }*/
}
