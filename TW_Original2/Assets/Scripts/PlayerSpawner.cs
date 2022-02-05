using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    Transform[] spawnPoints;


    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            GameObject playerGameobject = PhotonNetwork.Instantiate(player.name, spawnPoints[PhotonNetwork.LocalPlayer.ActorNumber-1].position, Quaternion.identity);
        }
    }















    /*void SpawnPlayer()
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
        Debug.Log("Player setup");
        
    }

    void Start()
    {
        //spawn player only if connected to server
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }*/

}
