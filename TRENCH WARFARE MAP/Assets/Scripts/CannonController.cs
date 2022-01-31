using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CannonController : MonoBehaviourPun
{
    //speed at which cannon rotates
    public float rotationSpeed = 0.05f;
    //blast power
    public float BlastPower = 200;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public cameraSwitch cameraSwitch;
    private bool canShoot;
    private bool active;
    float HorizontalRotation = 0f;
    float VericalRotation = 0f;
    
    public GameObject Explosion;

    private void OnEnable()
    {
        canShoot = true;

            this.gameObject.transform.rotation = Quaternion.Euler(0, -90, -45);

           // this.gameObject.transform.rotation = Quaternion.Euler(0, 90, -45);
           // TODO: Determine rotation based on PhotonView ID / Playerspawn point
    }

    private void Update()
    {
        Debug.Log("active: " + active);

        if (active)
        {
            float HorizontalRotation = Input.GetAxis("Fire1");
            float VericalRotation = Input.GetAxis("Fire2");

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
            fireCannon();

        }
        
    }
  void fireCannon() {
    //when the 'F' key is pressed
    if (Input.GetKeyDown(KeyCode.F) && canShoot == true)
    {
        canShoot = false;

        //Spawn cannon ball at the shotpoint gameobject position
        GameObject CreatedCannonball = PhotonNetwork.Instantiate(Cannonball.name, ShotPoint.position, ShotPoint.rotation);

        //play explosion particle effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);

        //add velocity to the balls rigidbody component to allow it to move
        CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

        // Added explosion for added effect
        Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);

            this.photonView.RPC("EnableCannon", RpcTarget.Others);
            DisableCannon();
    }
  }

    void DisableCannon()
    {
        this.active = false;
        this.gameObject.GetComponent<DrawProjection>().enabled = false;
        
    }

    [PunRPC]
    public void EnableCannon()
    {
        //this.photonView.RPC("DisableCannon", RpcTarget.Others);
        this.active = true;
        this.gameObject.GetComponent<DrawProjection>().enabled = true;
    }

    public void ActivateCannon()
    {
        this.active = true;
        this.gameObject.GetComponent<DrawProjection>().enabled = true;
    }
}
