using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float normalSpeed = 60f;

    public bool reverseOnNewPin;
    public bool changesSpeed;
    public float increasedSpeed;
    public float secondsBetween;
    public int pinsToWin;
    private bool speedIsChanged;

    private void Start()
    {
        if(changesSpeed)
            InvokeRepeating("changeSpeedChange", secondsBetween, secondsBetween);
    }

    void Update()
    {
        RotateCircle();
    }
    void RotateCircle()
    {
        if (!speedIsChanged)
        {
            transform.Rotate(0f, 0f, normalSpeed * Time.deltaTime);
        }
        else if (speedIsChanged)
        {
            transform.Rotate(0f, 0f, increasedSpeed * Time.deltaTime);
        }
    }

    void changeSpeedChange()
    {
        if (speedIsChanged)
            speedIsChanged = false;
        else
            speedIsChanged = true;
    }
}
