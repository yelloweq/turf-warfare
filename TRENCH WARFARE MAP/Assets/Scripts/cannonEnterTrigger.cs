using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class cannonEnterTrigger : MonoBehaviourPun
{
    public cameraSwitch cameraSwitch;
    public Text message;
    public BoxCollider cannonBody;

    public bool entered;

    string originalText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject camManager = GameObject.Find("cameraManager");
        cameraSwitch = camManager.GetComponent<cameraSwitch>();
        message = GameObject.Find("cannonTrigger").GetComponent<Text>();
        cannonBody = transform.parent.gameObject.GetComponent<BoxCollider>();
        entered = false;
        originalText = message.text;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && entered == false && photonView.IsMine)
        {
            message.text = "Press 'E' to enter cannon";
            if (Input.GetKeyDown(KeyCode.E))
            {
                entered = true;
                cannonBody.isTrigger = true;
                message.text = "Walk away to exit";
                cameraSwitch.changeCam("backCamera");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
        message.text = originalText;
        message.text = "";
        cameraSwitch.changeCam("mainCamera");
        cannonBody.isTrigger = false;
    }

    public void SetCam(string camName)
    {
        cameraSwitch.changeCam(camName);
    }
}
