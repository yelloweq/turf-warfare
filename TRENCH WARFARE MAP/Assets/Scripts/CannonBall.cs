using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CannonBall : MonoBehaviourPun
{
    private WindSystem windSystem;
    private Rigidbody rb;
    private bool inWindRegion;
    public GameObject windRegion;
    private Currency currency;

    float strenth;
    Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        windSystem = GameObject.Find("EventSystem").GetComponent<WindSystem>();
        currency = GameObject.Find("CurrencyManager").GetComponent<Currency>();

        //destroys ball in 10s of spawning in case the ball goes outside the map
        Destroy(this, 10f);

    }
    public GameObject Explosion;

    //if the ball collides this method is executed
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundaries" || collision.gameObject.tag == "Cannon" || collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
        else
        {
            if (collision.gameObject.tag == "Base" && photonView.IsMine)
            {
                currency.updateCurrency(300);
            }  
            //Play explosion particle effect
            Destroy(PhotonNetwork.Instantiate(Explosion.name, this.transform.position, this.transform.rotation), 2);
            //destroy the ball from the scene
            Destroy(this.gameObject);
        }
        windSystem.GenerateWind();
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
            rb.AddForce(windSystem.direction * windSystem.strength);
        }
    }
}
