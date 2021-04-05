using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject pinPrefab;
    bool bugFix;

    private void Start()
    {
        if (Input.GetMouseButtonDown(0))
            bugFix = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !bugFix)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            if(touchPos.y < 4)
                Instantiate(pinPrefab, transform.position, transform.rotation);
        }
        bugFix = false;
    }

    //public void SpawnPin()
    //{
    //    Instantiate(pinPrefab, transform.position, transform.rotation);
    //}
}
