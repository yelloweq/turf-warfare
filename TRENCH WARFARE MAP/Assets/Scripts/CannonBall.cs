using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CannonBall : MonoBehaviour
{
    public GameManager1 gameManager1;

    private void Start()
    {
    //    gameManager1 = GameObject.Find("GameManager").GetComponent<GameManager1>();

    //    Invoke("CallSwitch", 10);
      
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
            // CallSwitch();

            //Play explosion particle effect
            Destroy(PhotonNetwork.Instantiate(Explosion.name, this.transform.position, this.transform.rotation), 2);
            //destroy the ball from the scene
            Destroy(this.gameObject);
        }
    }

    // void CallSwitch()
    // {
    //     if (gameManager1)
    //     {
    //         //Calls the PlayerSwitch method within gameManager1 script
    //         gameManager1.PlayerSwitch();
    //     }
    // }
}
