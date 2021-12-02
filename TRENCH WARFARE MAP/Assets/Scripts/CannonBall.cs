using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public BattleSystem BattleSystemScript;

    private void Start()
    {
        BattleSystemScript = GameObject.Find("EventSystem").GetComponent<BattleSystem>();

        //destroys ball in 15s of spawning in case the ball goes outside the map
        Destroy(this.gameObject, 15f);
    }
    public GameObject Explosion;

    //if the ball collides this method is executed
    void OnCollisionEnter(Collision collision)
    {
        //Play explosion particle effect
        Destroy(Instantiate(Explosion, this.transform.position, this.transform.rotation), 2);
        //destroy the ball from the scene
        Destroy(this.gameObject);

        //if the BattleSystemScript is assigned in inspector
        if (BattleSystemScript)
        {
            //Calls the PlayerSwitch method within BattleSystemScript script
            BattleSystemScript.PlayerSwitch();
        }
    }
}
