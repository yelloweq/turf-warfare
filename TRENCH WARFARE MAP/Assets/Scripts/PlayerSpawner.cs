using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerModel;
    [SerializeField]
    GameObject[] spawnPoints;

    void SpawnPlayer()
    {
        //spawn player based on host
        int player = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }
        GameObject _localPlayer = PhotonNetwork.Instantiate(playerModel.name, spawnPoints[player].transform.position, Quaternion.identity, 0);

        //enable camera and movement for player
        _localPlayer.GetComponent<PlayerSetup>().IsLocalPlayer();  
    }

    void Start()
    {
        //spawn player only if connected to server
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }
   
}