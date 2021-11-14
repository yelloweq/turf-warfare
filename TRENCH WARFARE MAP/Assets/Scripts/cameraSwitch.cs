using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public GameObject cam_side;
    public GameObject cam_player;

    public void sideCamera()
    {
        cam_side.SetActive(true);
        cam_player.SetActive(false);
    }

    public void mainCamera()
    {
        cam_side.SetActive(false);
        cam_player.SetActive(true);
    }


}

