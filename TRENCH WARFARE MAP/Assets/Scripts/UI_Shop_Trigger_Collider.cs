using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Shop_Trigger_Collider : MonoBehaviour
{

    private bool isMouseOverUI;

    public GameObject menuUI;

    private void Update()
    {
        isMouseOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    void OnTriggerEnter(Collider Obj)
    {
        if(Obj.gameObject.tag == "Player")
        {
            menuUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
 
    void OnTriggerExit(Collider Obj)
    {
        if (Obj.gameObject.tag == "Player")
        {
            menuUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void baseUpgrade()
    {

    }

    void buyHealth()
    {

    }

}