using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public BattleSystem BattleSystemScript;

    private void Start()
    {
       BattleSystemScript = GameObject.Find("EventSystem").GetComponent<BattleSystem>();

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

    void CallSwitch()
    {
        if (BattleSystemScript)
        {
            //Calls the PlayerSwitch method within BattleSystemScript script
            BattleSystemScript.PlayerSwitch();
        }
    }
}
