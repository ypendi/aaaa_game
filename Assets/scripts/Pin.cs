using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pin : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rotator rotatorScript;

    public float speed = 30f;
    private bool isPinned;

    private void Start()
    {
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
            if (rotatorScript.reverseOnNewPin)
            {
                rotatorScript.normalSpeed *= -1;
                rotatorScript.increasedSpeed *= -1;
            }
        }
        if(other.tag == "Pin")
        {
            FindObjectOfType<GameManager>().LevelLost();
        }
    }

}
