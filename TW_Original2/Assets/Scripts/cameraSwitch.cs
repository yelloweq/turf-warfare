using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public GameObject cam_side;
    public GameObject cam_player;

    public void sideCamera()
    {
        //enables the side camera and disables player cam
        cam_side.SetActive(true);
        cam_player.SetActive(false);
    }

    public void mainCamera()
    {
        //enables the player camera and disables side cam
        cam_side.SetActive(false);
        cam_player.SetActive(true);
    }


}

