using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puck : MonoBehaviour
{
    Rigidbody r;
    AudioSource audioSource;

    //// Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Stop()
    {
        r.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SouthGoal")
        {
            Debug.Log("SouthGoal");
            GameObject.Find("ScorePanel").GetComponent<ScoreManager>().AddScore(Player.Which.AI);
       } else if (other.name == "NorthGoal")
        {
           Debug.Log("NorthGoal");
            GameObject.Find("ScorePanel").GetComponent<ScoreManager>().AddScore(Player.Which.PLAYER);
        }
    }
}
