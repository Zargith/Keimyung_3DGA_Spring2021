using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    Rigidbody r;

    private Vector3 direction = Vector3.zero;

    //// Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        //direction = new Vector3(-0.01F, 0, 0);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position += direction;
    //    if (transform.position.z > 0.6F || transform.position.z < -0.6F ||
    //        transform.position.x > 1.1F || transform.position.x < -1.1F)
    //    {
    //        Debug.Log("Problem: " + transform.position);
    //        Stop();
    //        GameObject.Find("Puck").GetComponent<Transform>().position = new Vector3(-0.470999986F, 0.337000012F, -0.0280000009F);
    //    }
    //}

    public void Stop()
    {
        direction = Vector3.zero;
        r.velocity = Vector3.zero;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "AirHockey_Paddle")
    //    {
    //        Debug.Log("Paddle");
    //        Vector3 velocity = collision.gameObject.GetComponent<Rigidbody>().velocity;

    //        //direction += velocity * 0.5F * Time.deltaTime;
    //        direction = -Vector3.Reflect(direction + velocity * 0.2F * Time.deltaTime, collision.contacts[0].normal);
    //    } else if (collision.gameObject.tag == "AirHockey_Goal")
    //    {
    //        Debug.Log(collision.gameObject.name);
    //    } else if (collision.gameObject.tag == "AirHockey_Side" || collision.gameObject.tag == "AirHockey_Corner")
    //    {
    //        Debug.Log(collision.gameObject.tag);

    //        direction = Vector3.Reflect(direction, collision.contacts[0].normal) * 0.9F;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "SouthGoal")
    //    {
    //        Debug.Log("SouthGoal");
    //        GameObject.Find("ScorePanel").GetComponent<ScoreManager>().AddScore(Player.Which.AI);
    //    } else if (other.name == "NorthGoal")
    //    {
    //        Debug.Log("NorthGoal");
    //        GameObject.Find("ScorePanel").GetComponent<ScoreManager>().AddScore(Player.Which.PLAYER);
    //    }
    //}
}
