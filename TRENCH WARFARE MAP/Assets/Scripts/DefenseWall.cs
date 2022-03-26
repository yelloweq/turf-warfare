using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWall : MonoBehaviour
{
    public double lives;

    // Start is called before the first frame update
    void Start()
    {
        lives = 1;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            lives =- 1;
            if(lives <= 0)
            {
                DestroyWall();
            }
        }
    }

    void DestroyWall()
    {   
        lives = 3;
        gameObject.SetActive(false);
    }
}
