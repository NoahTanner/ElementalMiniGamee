using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementScript : MonoBehaviour
{
    public float speed = 5f; // Speed at which the character moves to the right
    bool moving = true;

    // Update is called once per frame
    void Update()
    {
        if (moving == true){
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Checkpoint"))
        {
            moving = false;
        }
        
    }

}

