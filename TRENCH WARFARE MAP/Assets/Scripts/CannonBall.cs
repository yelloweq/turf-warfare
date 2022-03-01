using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CannonBall : MonoBehaviour
{
    public BattleSystem BattleSystemScript;
    public GameManager1 OfflineSystemScript;

    private void Start()
    {
       SetCorrectTurnSwitcher();
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
        Debug.Log("addw");
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            
            if (BattleSystemScript)
            {
                //Calls the PlayerSwitch method within script MainScene scene
                BattleSystemScript.PlayerSwitch();
            }
        }
        else
        {
            if (OfflineSystemScript)
            {
                Debug.Log("Made it here!!");
                //Calls the PlayerSwitch method within the script for the offline scene
                OfflineSystemScript.PlayerSwitch();
            }
        }
    }
    void SetCorrectTurnSwitcher()
    {
        
        if(SceneManager.GetActiveScene().name == "MainScene") //When in the mainscene we get the BattleSystem script which is used to switch canons in that scene
        {
            BattleSystemScript = GameObject.Find("EventSystem").GetComponent<BattleSystem>();
            OfflineSystemScript = null;
        }
        else //When in the offline scene we use the gameManager1 script which is used to switch players and canons in that scene
        {
            OfflineSystemScript = GameObject.Find("GameManager").GetComponent<GameManager1>();
            BattleSystemScript = null;
        }
    }
    
}
