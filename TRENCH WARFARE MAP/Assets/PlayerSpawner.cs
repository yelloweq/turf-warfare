using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject player;
    [Space]
    public Transform spawnPoint;

    public void SpawnPlayer()
    {
        GameObject _localPlayer = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity, 0);

        _localPlayer.GetComponent<PlayerSetup>().IsLocalPlayer();
        
    }
   
}
