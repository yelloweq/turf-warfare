using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    //speed at which cannon rotates
    public float rotationSpeed = 0.05f;
    //blast power
    public float BlastPower = 200;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public cameraSwitch cameraSwitch;
    private bool canShoot;

    //float HorizontalRotation = 0f;
    //float VericalRotation = 0f;

    public GameObject Explosion;

    private void OnEnable()
    {
        canShoot = true;

        if (this.gameObject.name == "FriendlyCannon")
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, -90, -45);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 90, -45);
        }
    }

    private void Update()
    {
        float HorizontalRotation = Input.GetAxis("Fire1");
        float VericalRotation = Input.GetAxis("Fire2");

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        //new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
        fireCannon();
    }
    void fireCannon() {
    //when the 'F' key is pressed
    if (Input.GetKeyDown(KeyCode.F) && canShoot == true)
    {
        canShoot = false;

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
