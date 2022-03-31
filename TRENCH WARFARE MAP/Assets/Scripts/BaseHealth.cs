using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class BaseHealth : MonoBehaviourPun
{
//     //Starting health when bases spawn
    private int health = 1000;
    public const int maxHealth = 1000;
    [SerializeField]
    private GameObject Explosion;
    
    private PhotonView PV;
    private TurnTracking gameManager;

    [SerializeField]
    private HealthbarScript healthbarScript;

    private CannonController cannonController;

    void Start()
    {
        PV = PhotonView.Get(this); 
        if (PV.IsMine)
        {
            healthbarScript = GameObject.Find("P1Healthbar").GetComponent<HealthbarScript>();
        } 
        else 
        {
            healthbarScript = GameObject.Find("P2Healthbar").GetComponent<HealthbarScript>();
        }

        if (PV.IsMine)
        {
            this.gameObject.name = "FriendlyBase";
        }
        else 
        {
            this.gameObject.name = "EnemyBase";
        }
       
        gameManager = GameObject.Find("GameManager").GetComponent<TurnTracking>();
    }

    public int GetHealth()
    {
        return health;
    }
    public IEnumerator OnCollisionEnter(Collision collision)
    {
        //If the ball collides with the base
        if (collision.gameObject.tag == "ball")

        {
            //if the healthbar is assigned in inspector
            if (healthbarScript)
            {

                TakeDamage();
                yield return new WaitForSeconds(2);
            }
        }
        
    }

    private void TakeDamage()
    {
        cannonController = GameObject.Find("EnemyCannon").GetComponent<CannonController>();
        int damageTaken = cannonController.GetDamage();
        Debug.Log(damageTaken + " DAMAGE DEALT TO ENEMY BASE");
        // health -= damageTaken;
        PV.RPC("RPC_TakeDamage", RpcTarget.AllViaServer, damageTaken);
        
    }

    public void increaseHealth(int hp)
    {
        Debug.Log("BASE HEALED FOR " + hp + " hp");
        // health -= damageTaken;
        PV.RPC("RPC_IncreaseHealth", RpcTarget.AllViaServer, hp);
    }

    public void restoreHealth()
    {
        Debug.Log("BASE HEALTH RESTORED");
        // health -= damageTaken;
        PV.RPC("RPC_RestoreHealth", RpcTarget.AllViaServer);
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
            if (PV.IsMine)
            {
                gameManager.EndGame();
            }
        }
    }



    [PunRPC]
    void RPC_TakeDamage(int dmg)
    {
        Debug.Log("[RPC] BASE TAKEN DAMAGE");
        health -= dmg;
        healthbarScript.damageTaken(dmg);
    }

     [PunRPC]
    void RPC_IncreaseHealth(int amount)
    {
        if (healthbarScript)
        {
            Debug.Log("[RPC] BASE HEALED");
            health += amount;
            healthbarScript.increaseHealth(amount);
        }
        
    }

     [PunRPC]
    void RPC_RestoreHealth()
    {
        Debug.Log("[RPC] BASE MAX HEALTH RESTORED");
        health = 1000;
        healthbarScript.restoreHealth();
    }
}
