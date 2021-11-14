using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //speed at which cannon rotates
    public float rotationSpeed = 1;
    //blast power
    public float BlastPower = 200;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public cameraSwitch cameraSwitch;

    public GameObject Explosion;

    private void Update()
    {
        float HorizontalRotation = Input.GetAxis("Fire1");
        float VericalRotation = Input.GetAxis("Fire2");

       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
       new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
       fireCannon();



}
  void fireCannon() {
    //when the 'F' key is pressed
    if (Input.GetKeyDown(KeyCode.F))
    {
        //Spawn cannon ball at the shotpoint gameobject position
        GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
        //play explosion particle effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
        //add velocity to the balls rigidbody component to allow it to move
        CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

        // Added explosion for added effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
    }
  }

}
