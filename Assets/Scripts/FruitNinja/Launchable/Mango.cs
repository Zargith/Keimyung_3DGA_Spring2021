using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : Launchable
{
    [SerializeField] float _spawnForce;
    GameObject player;

    protected void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }


    override public void Launch(Vector3 beg, Vector3 end)
    {
        transform.position = new Vector3(Random.Range(beg.x, end.x), end.y / 2, Random.Range(beg.z, end.z));
        Vector3 dir = (player.transform.position - transform.position) + Vector3.up * 10;
        _main.AddForce(dir.normalized * _spawnForce, ForceMode.Impulse);
        _main.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
    }
}
