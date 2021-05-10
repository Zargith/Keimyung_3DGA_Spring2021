using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.name == "Puck")
        {
            Debug.Log("Puck");
            Vector3 velocity = transform.gameObject.GetComponent<Rigidbody>().velocity;

            //direction += velocity * 0.5F * Time.deltaTime;
            direction = Vector3.Reflect(direction + velocity * 0.3F * Time.deltaTime, collision.contacts[0].normal);
        }
        else if (collision.gameObject.tag == "Side")
        {
            Debug.Log("Side");
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            //direction *= 0.8F;
        }*/
    }
}
