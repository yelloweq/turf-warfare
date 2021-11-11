using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public double health = 100;
    public GameObject Explosion;
    public HealthbarScript HealthbarScript;

    public void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "ball")
      {
            if (HealthbarScript)
            {
                HealthbarScript.damageTaken(34);
                health -= 34;
            }
  
       }
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
