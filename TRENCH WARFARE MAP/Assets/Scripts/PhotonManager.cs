using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }   
    }

    void SpawnPlayer()
    {
        Debug.Log(SpawnPoints.Length + " :: " + SpawnPoints); 
        int player = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }
          
        GameObject Player = PhotonNetwork.Instantiate("FPS Controller Ethan", SpawnPoints[player].transform.position, Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
