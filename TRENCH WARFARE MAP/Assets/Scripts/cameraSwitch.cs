using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public GameObject cam_side;
    public GameObject cam_main;
    public GameObject cam_back;

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

