﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class CannonController : MonoBehaviourPun
{
    //speed at which cannon rotates
    public float rotationSpeed = 0.05f;
    //blast power
    public float BlastPower = 200;

    public GameObject Cannonball;
    public Transform ShotPoint;
    public cameraSwitch cameraSwitch;
    private bool canShoot;
    private bool active;

    private int projectiles = 1;

    public GameObject Explosion;

    public DrawProjection Projection;

    private const byte CANNON_FIRED_EVENT = 0;

    private PhotonView PV;

    private GameManager gameManager;

    private const int projectionLength = 50;
    private void Start()
    {
        Projection = GetComponent<DrawProjection>();
        PV = PhotonView.Get(this);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        

        PV.RPC("DisableCannon", RpcTarget.AllViaServer, "CANNON DISABLED");

        // if (!PhotonNetwork.IsMasterClient)
        // {
        //     PV.RPC("EnableCannon", RpcTarget.AllViaServer, "Master Cannon enabled!");
        // }
    }

    private void OnEnable()
    {

        this.gameObject.transform.rotation = Quaternion.Euler(0, -90, -45);

    }

    private bool CheckTurn(){
        return gameManager.GetCurrentTurn() == gameManager.GetMyTurn();
    }

    public void ResetCannon()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0, -90, -45);
        projectiles = 1;
    }

    private void Update()
    {
        if (!photonView.IsMine){
            return;
        }

        if (CheckTurn()){
            active = true;
            Projection.SetPoints(projectionLength);

        }

        if (active){
            float HorizontalRotation = Input.GetAxis("Fire1");
            float VericalRotation = Input.GetAxis("Fire2");

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
            fireCannon(); 
        }
        

        
    }

    void fireCannon()
    {
        //when the 'F' key is pressed
        if (Input.GetKeyDown(KeyCode.F) && System.Convert.ToBoolean(projectiles))
        {
            projectiles = 0;
            //Spawn cannon ball at the shotpoint gameobject position
            GameObject CreatedCannonball = PhotonNetwork.Instantiate(Cannonball.name, ShotPoint.position, ShotPoint.rotation);

            //play explosion particle effect
            Destroy(PhotonNetwork.Instantiate(Explosion.name, ShotPoint.position, ShotPoint.rotation), 2);

            //add velocity to the balls rigidbody component to allow it to move
            CreatedCannonball.GetComponent<Rigidbody>().velocity = ShotPoint.transform.up * BlastPower;

            // Added explosion for added effect
            Destroy(PhotonNetwork.Instantiate(Explosion.name, ShotPoint.position, ShotPoint.rotation), 2);


            PV.RPC("DisableCannon", RpcTarget.All, "DISABLED CANNON -> SHOT");

        }
    }

    private void DisableAfterTurn(){
        PV.RPC("DisableCannon", RpcTarget.All, "DISABLED CANNON -> END OF TURN");
    }
    [PunRPC]
    private void DisableCannon(string str)
    {
        Debug.Log(str + " =========id========== " + PV.ViewID);
        active = false;
        Projection.SetPoints(0);
        projectiles = 0;
    }

    [PunRPC]
    private void EnableCannon(string str)
    {
        Debug.Log(str + " =========id========== " + PV.ViewID);
        active = true;
        Projection.SetPoints(projectionLength);
        projectiles = 0;
    }
}
