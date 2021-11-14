using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public double health = 100;
    public GameObject Explosion;
    public HealthbarScript HealthbarScript;
    public BattleSystem BattleSystemScript;

    public void OnCollisionEnter(Collision collision)
    {
      Debug.Log(health);
      //  if (collision.gameObject.tag == "ball")

    //  {
         BattleSystemScript.PlayerSwitch();

            if (HealthbarScript)
            {
                HealthbarScript.damageTaken(34);
                health -= 34;
                //if (BattleSystemScript) {
                 Debug.Log(health);

              //}
            }

      // }


    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(Instantiate(Explosion, this.transform.position, this.transform.rotation), 2);
            Destroy(this.gameObject);
        }
    }
}
