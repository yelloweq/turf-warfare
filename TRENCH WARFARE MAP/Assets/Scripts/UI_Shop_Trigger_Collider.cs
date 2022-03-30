using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop_Trigger_Collider : MonoBehaviour
{
    public GameObject menuUI;
    GameObject mouseLookScript;
    public Text message;
    private void Start()
    {
        mouseLookScript = GameObject.FindWithTag("MainCamera");
    }
    void OnTriggerEnter(Collider Obj)
    {
        if(Obj.gameObject.tag == "Player")
        {
            menuUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            mouseLookScript.GetComponent<MouseLook>().enabled = false;
        }
    }
 
    void OnTriggerExit(Collider Obj)
    {
        if (Obj.gameObject.tag == "Player")
        {
            menuUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mouseLookScript.GetComponent<MouseLook>().enabled = true;
            message.gameObject.SetActive(false);
        }
    }
}