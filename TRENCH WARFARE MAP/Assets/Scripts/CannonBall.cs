using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public BattleSystem gameManager1;
    public Rigidbody rb;
    public bool inWindRegion;
    public GameObject windRegion;

    float strenth;
    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        gameManager1 = GameObject.Find("EventSystem").GetComponent<BattleSystem>();

        Invoke("CallSwitch", 10);

        //destroys ball in 10s of spawning in case the ball goes outside the map
        Destroy(this.gameObject, 10f);

    }
    public GameObject Explosion;

    //if the ball collides this method is executed
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundaries" || collision.gameObject.tag == "Cannon")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            CallSwitch();

            //Play explosion particle effect
            Destroy(Instantiate(Explosion, this.transform.position, this.transform.rotation), 2);
            //destroy the ball from the scene
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WindRegion")
        {
            inWindRegion = true;
            windRegion = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WindRegion")
        {
            inWindRegion = false;
        }
    }

    private void FixedUpdate()
    {
        if (inWindRegion)
        {
            rb.AddForce(gameManager1.direction * gameManager1.strenth);
        }
    }

    void CallSwitch()
    {
        if (gameManager1)
        {
        //Calls the PlayerSwitch method within gameManager1 script
            gameManager1.PlayerSwitch();
        }
    }
}
