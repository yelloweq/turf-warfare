using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 20f;
    public float zoomSpeed = 10f;
    public float zoomMax = 5f;
    public float zoomMin = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dx = panSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float dy = panSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Camera.main.transform.Translate(dx, dy, 0);

        float dz = zoomSpeed * Time.deltaTime * Input.GetAxis("Zoom");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + dz, zoomMin, zoomMax);
    }
}
