using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 15f);
    }
    public GameObject Explosion;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(Explosion, this.transform.position, this.transform.rotation), 2);
        Destroy(this.gameObject);
    }
}
