using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class cameraSwitch : MonoBehaviourPun
{
    private GameObject cam_side;
    private GameObject cam_main;
    private GameObject cam_back;


    public void Start()
    {
        if (PhotonNetwork.IsMasterClient && photonView.IsMine){
            cam_back = GameObject.Find("cannonViewBack");
            cam_side = GameObject.Find("cannonViewSide");

        }
        else 
        {
            cam_back = GameObject.Find("cannon2ViewBack");
            cam_side = GameObject.Find("cannon2ViewSide");
        }

        cam_back.SetActive(false);
        cam_side.SetActive(false);
    }
    public void SetMainCamera(GameObject cam)
    {
        this.cam_main = cam;
    }
    public void changeCam(string camName)
    {
        if (camName == "sideCamera")
        {
            //enables the side camera and disables player cam
            cam_side.SetActive(true);
            cam_main.SetActive(false);
            cam_back.SetActive(false);

        } else if(camName == "backCamera")
        {
            //enables the player camera and disables side cam
            cam_side.SetActive(false);
            cam_main.SetActive(false);
            cam_back.SetActive(true);

        } else if(camName == "mainCamera")
        {
            //enables the player camera and disables side cam
            cam_side.SetActive(false);
            cam_main.SetActive(true);
            cam_back.SetActive(false);
        }
    }

}

