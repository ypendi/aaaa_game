using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pin : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rotator rotatorScript;

    public AudioSource pinHit;
    public AudioSource pinFail;

    public float speed = 30f;
    private bool isPinned;


    private void Start()
    {
        pinHit = GameObject.Find("aaaa_hit").GetComponent<AudioSource>();
        pinFail = GameObject.Find("aaaa_fail").GetComponent<AudioSource>();
        rotatorScript = FindObjectOfType<Rotator>();
    }

    void FixedUpdate()
    {
        if(!isPinned)
            rb.MovePosition(rb.position + Vector2.up * speed * Time.deltaTime);// not using a function cause it fucks it up
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Rotator")
        {
            isPinned = true;
            transform.SetParent(other.transform);
            Score.score--;
            pinHit.Play();
            if (rotatorScript.reverseOnNewPin)
            {
                rotatorScript.normalSpeed *= -1;
                rotatorScript.increasedSpeed *= -1;
            }
            if (rotatorScript.increasesSpeedOnPin)
            {
                rotatorScript.normalSpeed *= rotatorScript.speedIncreaseFactor;
                rotatorScript.increasedSpeed *= rotatorScript.speedIncreaseFactor;
            }
        }
        if(other.tag == "Pin")
        {
            pinFail.Play();
            FindObjectOfType<GameManager>().LevelLost();
        }
    }

}
