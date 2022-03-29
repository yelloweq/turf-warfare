using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindRegion : MonoBehaviour
{

    private float windStrength;
    private Vector3 windDirection;

    public Image windArrow;
    public Text windStrengthText;

    public float setStrength()
    {
        windStrength = Random.Range(1, 8);
        Debug.Log(windStrength);
        return windStrength;
    }

    public Vector3 setDirection()
    {
        windDirection = new Vector3(Random.Range(-15, 15), 0, 0);
        Debug.Log(windDirection);
        return windDirection;
    }

    public Vector3 getDirection()
    {
        return windDirection;
    }

    public float getStrength()
    {
        return windStrength;
    }

    public void setArrow(float direction)
    {
        if(direction > 0)
        {
            windArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
        }else if(direction < 0)
        {
            windArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
    }

    public void setStrengthText(float strength)
    {
        windStrengthText.text = strength.ToString();
    }
}
