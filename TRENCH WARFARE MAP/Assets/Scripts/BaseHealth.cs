using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BaseHealth : MonoBehaviour
{
    //Starting health when bases spawn
    public double health = 100;

    public GameObject Explosion;

    public HealthbarScript HealthbarScript;

    private PhotonView PV;
    

    private void Start()
    {
        PV = PhotonView.Get(this);
    }
    public void OnCollisionEnter(Collision collision)
    {
        //If the ball collides with the base
        if (collision.gameObject.tag == "ball")
        {
            Debug.Log("Collision with ball");
            //if the healthbar is assigned in inspector
            if (HealthbarScript)
            {
                //Calls the damageTaken method within HealthbarScript script
                PV.RPC("TakeDamage", RpcTarget.AllViaServer, 34);
            }
        }

    }

    private void Update()
    {
        //if the health is 0 or below
        if (health <= 0)
        {
            //Starts exploding particle effect
            Destroy(PhotonNetwork.Instantiate(Explosion.name, this.transform.position, this.transform.rotation), 2);
            //Destroys the base prefab
            Destroy(this.gameObject);
        }
    }

    [PunRPC]
    void TakeDamage(int damage)
    {
        Debug.Log("[RPC] BASE TAKEN DAMAGE");
        health -= damage;
        HealthbarScript.damageTaken(34);
    }

}