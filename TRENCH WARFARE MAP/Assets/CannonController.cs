using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float BlastPower = 5;

    public GameObject Cannonball;
    public Transform ShotPoint;
    

    public GameObject Explosion;

    private void start() {

    }
    private void Update()
    {
        float HorizontalRotation = Input.GetAxis("Fire1");
        float VericalRotation = Input.GetAxis("Fire2");

       transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
       new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject CreatedCannonball = Instantiate(Cannonball, ShotPoint.position, ShotPoint.rotation);
            Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

            // Added explosion for added effect
            Destroy(Instantiate(Explosion, ShotPoint.position, ShotPoint.rotation), 2);
            Destroy(CreatedCannonball, 3.0f);



            // Shake the screen for added effect
            Screenshake.ShakeAmount = 5;
            void onCollisionEnter(Collision col) {
              if (col.gameObject.name == "Base 1") {
                Destroy(col.gameObject);
                Destroy(CreatedCannonball.gameObject, 4.0f);
              }
        }
    }


}
}
