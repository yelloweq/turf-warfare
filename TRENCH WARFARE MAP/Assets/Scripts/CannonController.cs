using Photon.Pun;
using System.Collections;
using UnityEngine;

public class CannonController : MonoBehaviourPun
{
    [SerializeField]
    private float rotationSpeed = 0.05f;
    public float BlastPower = 200;
    [SerializeField]
    private GameObject Cannonball;
    public Transform ShotPoint;
    [SerializeField]
    private cannonEnterTrigger cannonEnterTrigger;
    private bool horizontalSet;
    private bool verticalSet;
    private bool active = true;
    private int projectiles = 1;
    private DrawProjection Projection;
    private PhotonView PV;
    private TurnTracking gameManager;
    private const int projectionLength = 20;
    [SerializeField]
    private GameObject Explosion;
    public const int DAMAGE = 100;
    private void Start()
    {
        Projection = GetComponent<DrawProjection>();
        PV = PhotonView.Get(this);
        gameManager = GameObject.Find("GameManager").GetComponent<TurnTracking>();

        PV.RPC("DisableCannon", RpcTarget.All, "DISABLED CANNON -> GAME START");

        if (photonView.IsMine)
        {
            this.gameObject.name = "FriendlyCannon";
            StartCoroutine(ResetCannon());
        }
        else
        {
            this.gameObject.name = "EnemyCannon";
        }

    }

    public int GetDamage()
    {
        return DAMAGE;
    }

    private void OnEnable()
    {
        horizontalSet = false;
        verticalSet = false;
    }

    public IEnumerator ResetCannon()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("ResetCannon calling");

        PV.RPC("EnableCannon", RpcTarget.All, "ENABLED CANNON -> CANNON RESET");

        Debug.Log("RESET CANNON RPC REQUEST SENT");
    }

    public void setHorizontal(float HorizontalRotation)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, HorizontalRotation * rotationSpeed, 0));

        if (Input.GetMouseButtonDown(0))
        {
            horizontalSet = true;
            cannonEnterTrigger.SetCam("sideCamera");
        }

    }

    public void setVertical(float VerticalRotation)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
            new Vector3(0, 0, VerticalRotation * rotationSpeed));

        if (Input.GetMouseButtonDown(1))
        {
            verticalSet = true;
            fireCannon();
        }
    }
    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (!(gameManager.CheckTurn()))
        {
            Projection.SetPoints(0);
            return;
        }

        if (!active)
        {
            return;
        }

        if (projectiles != 1)
        {
            return;
        }

        float HorizontalRotation = Input.GetAxis("Fire1");
        float VerticalRotation = Input.GetAxis("Fire2");


        if (cannonEnterTrigger.entered == true && horizontalSet == false)
        {
            setHorizontal(HorizontalRotation);
        }

        if (cannonEnterTrigger.entered == true && horizontalSet == true && verticalSet == false)
        {
            setVertical(VerticalRotation);
        }
        Projection.SetPoints(projectionLength);
        // float HorizontalRotation = Input.GetAxis("Fire1");
        // float VericalRotation = Input.GetAxis("Fire2");

        // transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles +
        // new Vector3(0, HorizontalRotation * rotationSpeed, VericalRotation * rotationSpeed));
        fireCannon();

    }

    void fireCannon()
    {

        if (Input.GetKeyDown(KeyCode.F) && projectiles == 1)
        {
            horizontalSet = false;
            verticalSet = false;
            cannonEnterTrigger.entered = false;
            cannonEnterTrigger.SetCam("mainCamera");
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

    [PunRPC]
    private void DisableCannon(string str)
    {
        Debug.Log(str + " =========id========== " + PV.ViewID);
        active = false;
        Projection.SetPoints(0);
        projectiles = 0;

        if (PhotonNetwork.IsMasterClient)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(180, 270, 175);
        }
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }

    }

    [PunRPC]
    private void EnableCannon(string str)
    {
        Debug.Log(str + " =========id========== " + PV.ViewID);
        active = true;
        Projection.SetPoints(projectionLength);
        projectiles = 1;

        if (PhotonNetwork.IsMasterClient)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(180, 270, 175);
        } //0 620 195
        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }
}
