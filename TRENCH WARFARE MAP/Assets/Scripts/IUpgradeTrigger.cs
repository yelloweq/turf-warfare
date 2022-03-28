using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 A simple interface that needs the following methods to be implemented for any
 upgrades. To implement this interface you need to add a "," after
 "MonoBehaviour" then type "IUpgradeTrigger" in the class declatation line.
*/
public interface IUpgradeTrigger
{

    /* method that will involve getting keyboard inputs from users when
    the collider touches the trigger
    */
    void OnTriggerStay(Collider other);

    //when the collider has stopped touching the trigger
    void OnTriggerExit(Collider other);



}