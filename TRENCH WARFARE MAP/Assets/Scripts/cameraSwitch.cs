using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class cameraSwitch : MonoBehaviourPun
{
    private GameObject cam_side;
    private GameObject cam_main;
    private GameObject cam_back;
    private GameObject Enemy_cam_side;
    private GameObject Enemy_cam_back;

    public void Start()
    {
        if (PhotonNetwork.IsMasterClient){
            cam_back = GameObject.Find("cannonViewBack");
            cam_side = GameObject.Find("cannonViewSide");
            Enemy_cam_back = GameObject.Find("cannon2ViewBack");
            Enemy_cam_side = GameObject.Find("cannon2ViewSide");

        }
        else 
        {
            cam_back = GameObject.Find("cannon2ViewBack");
            cam_side = GameObject.Find("cannon2ViewSide");
            Enemy_cam_back = GameObject.Find("cannonViewBack");
            Enemy_cam_side = GameObject.Find("cannonViewSide");
        }

        cam_back.SetActive(false);
        cam_side.SetActive(false);
        Enemy_cam_back.SetActive(false);
        Enemy_cam_side.SetActive(false);
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

