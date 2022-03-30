using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    //Starting health when bases spawn
    public double health = 100;

    public GameObject Explosion;
    public GameObject gameCompleteScreen;
    public HealthbarScript HealthbarScript;
    private CharacterCurrency characterCurrency;

    private void Start()
    {
        characterCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterCurrency>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        //If the ball collides with the base
        if (collision.gameObject.tag == "ball")

        {
            //if the healthbar is assigned in inspector
            if (HealthbarScript && characterCurrency)
            {
                //Calls the damageTaken method within HealthbarScript script
                HealthbarScript.damageTaken(34);
                health -= 34;

                characterCurrency.updateCurrency(300);
            }
        }

    }

    private void Update()
    {
        //if the health is 0 or below
        if (health <= 0)
        {
            //Starts exploding particle effect
            Destroy(Instantiate(Explosion, this.transform.position, this.transform.rotation), 2);
            //Destroys the base prefab
            Destroy(this.gameObject);
            gameCompleteScreen.SetActive(true);
        }
    }

}