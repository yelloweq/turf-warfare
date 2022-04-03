using UnityEngine;


public class PlayerSetup : MonoBehaviour
{
    public GameObject cameraHolder;

    private cameraSwitch CameraManager;

    public void Start()
    {
        CameraManager = GameObject.Find("cameraManager").GetComponent<cameraSwitch>();
        CameraManager.SetMainCamera(cameraHolder);
    }
    public void IsLocalPlayer()
    {
        //turn on local player camera and movement
        //prevents from one player controlling both characters
        cameraHolder.GetComponent<Camera>().enabled = true;
        this.gameObject.GetComponent<CharacterMovement>().enabled = true;

    }


}
