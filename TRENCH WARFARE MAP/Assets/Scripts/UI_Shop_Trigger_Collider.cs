using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
public class UI_Shop_Trigger_Collider : MonoBehaviour
{
    public GameObject menuUI;
    GameObject mouseLookScript;
    public Text message;

    private PhotonView PV;

    private TurnTracking turnTracking;
    private void Start()
    {
        mouseLookScript = GameObject.FindWithTag("MainCamera");
        turnTracking = GameObject.Find("GameManager").GetComponent<TurnTracking>();
    }
    void OnTriggerEnter(Collider Obj)
    {
        if (!PV)
        {
            PV = PhotonView.Get(GameObject.Find("FriendlyBase"));
        }
        if (!mouseLookScript)
        {
            mouseLookScript = GameObject.FindWithTag("MainCamera");
        }
        if (!turnTracking)
        {
            turnTracking = GameObject.Find("GameManager").GetComponent<TurnTracking>();
        }

        if(Obj.gameObject.tag == "Player" && turnTracking.CheckTurn())
        {
            menuUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            mouseLookScript.GetComponent<MouseLook>().enabled = false;
        }
    }
 
    void OnTriggerExit(Collider Obj)
    {
        if (!PV)
        {
            PV = PhotonView.Get(GameObject.Find("FriendlyBase"));
        }
        if (!mouseLookScript)
        {
            mouseLookScript = GameObject.FindWithTag("MainCamera");
        }
        if (!turnTracking)
        {
            turnTracking = GameObject.Find("GameManager").GetComponent<TurnTracking>();
        }

        if (Obj.gameObject.tag == "Player" && PV.IsMine)
        {
            menuUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mouseLookScript.GetComponent<MouseLook>().enabled = true;
            message.gameObject.SetActive(false);
        }
    }
}