using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject windRegion;

    [SerializeField]
    private GameObject windUI;

    private TurnTracking turnTracking;
    public float strength;
    public Vector3 direction;

    void Update()
    {
        if (turnTracking.CheckTurn())
        {
            windUI.SetActive(true);
        }
        else 
        {
            windUI.SetActive(false);
        }
    }
    public void GenerateWind()
    {
        strength = windRegion.GetComponent<WindRegion>().setStrength();
        direction = windRegion.GetComponent<WindRegion>().setDirection();

        windRegion.GetComponent<WindRegion>().setArrow(direction.x);
        windRegion.GetComponent<WindRegion>().setStrengthText(strength);
    }

    // Update is called once per frame
    void Start()
    {
        windUI = GameObject.Find("WindUI");
        turnTracking = GameObject.Find("GameManager").GetComponent<TurnTracking>();
        strength = windRegion.GetComponent<WindRegion>().setStrength();
        direction = windRegion.GetComponent<WindRegion>().setDirection();
        windRegion.GetComponent<WindRegion>().setArrow(direction.x);
        windRegion.GetComponent<WindRegion>().setStrengthText(strength);

    }
}
