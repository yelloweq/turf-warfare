using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop_Trigger_Collider : MonoBehaviour
{

public GameObject menuUI;
 
     void OnTriggerEnter(Collider Obj)
     {
         if(Obj.gameObject.tag == "Player")
         {
             menuUI.SetActive(true);
             
         }
     }
 
     void OnTriggerExit(Collider Obj)
     {
         if (Obj.gameObject.tag == "Player")
         {
             menuUI.SetActive(false);
         }
 }}