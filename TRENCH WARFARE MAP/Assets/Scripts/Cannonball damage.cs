using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonballdamage : MonoBehaviour
{
  void onCollisionEnter(Collision col) {
    if (col.gameObject.name == "Base 1") {
      Destroy(col.gameObject);
    }
  }
}
