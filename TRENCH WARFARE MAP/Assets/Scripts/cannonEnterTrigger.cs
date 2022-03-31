using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class cannonEnterTrigger : MonoBehaviourPun, IUpgradeTrigger
{
    public cameraSwitch cameraSwitch;
    public Text message;
    public BoxCollider cannonBody;

    public bool entered;
    private bool canEnter;

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
        canEnter = false;
    }
    /* Updates every frame, and checks whether player can enter the cannon
       and checks where the correct key is pressed and if it should switch
       camera angles*/
    private void Update()
    {
        if(canEnter == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                entered = true;
                cannonBody.isTrigger = true;
                message.text = "Walk away to exit";
                cameraSwitch.changeCam("backCamera");
            }
        }
    }
    /* If the player is near the cannon, but hasn't entered, display the
    message notifying the use about which button to press*/
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && entered == false && photonView.IsMine)
        {
            canEnter = true;
            message.text = "Press 'E' to enter cannon";
        }
    }
    /* If the player leaves the cannon area, switch back to the main Camera */
    public void OnTriggerExit(Collider other)
    {
        canEnter = false;
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
