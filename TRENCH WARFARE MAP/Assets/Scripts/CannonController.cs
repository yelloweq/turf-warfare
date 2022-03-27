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
    public cannonEnterTrigger cannonEnterTrigger;
    private bool canShoot;
    public bool horizontalSet;
    public bool verticalSet;

    //float HorizontalRotation = 0f;
    //float VerticalRotation = 0f;

    public GameObject Explosion;
    public GameObject cannonTop;

    private void OnEnable()
    {
        canShoot = true;
        horizontalSet = false;
        verticalSet = false;

        GameObject cannonTopPrefab = transform.GetChild(1).gameObject;
        cannonTop = cannonTopPrefab.transform.GetChild(0).gameObject;

        if (this.gameObject.name == "FriendlyCannon")
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    private void Update()
    {
        float HorizontalRotation = Input.GetAxis("Fire1");
        float VerticalRotation = Input.GetAxis("Fire2");

        
        if(cannonEnterTrigger.entered == true && horizontalSet == false)
        {
            setHorizontal(HorizontalRotation);
        }

        if(cannonEnterTrigger.entered == true && horizontalSet == true && verticalSet == false)
        {
            setVertical(VerticalRotation);
        }

    }

    public void setHorizontal(float HorizontalRotation)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, HorizontalRotation * rotationSpeed, 0));

        if (Input.GetMouseButtonDown(0))
        {
            horizontalSet = true;
            cannonEnterTrigger.SetCam("sideCamera");
        }
        
    }

    public void setVertical(float VerticalRotation)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, 0, VerticalRotation * rotationSpeed));

        if (Input.GetMouseButtonDown(1))
        {
            verticalSet = true;
            fireCannon();
        }
    }

    void fireCannon() {
 
        if (canShoot == true)
        {
          
            canShoot = false;
            horizontalSet = false;
            verticalSet = false;
            cannonEnterTrigger.entered = false;
            cannonEnterTrigger.SetCam("mainCamera");

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
