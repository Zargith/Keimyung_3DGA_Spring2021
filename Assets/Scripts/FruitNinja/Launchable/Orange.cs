using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : Launchable
{
    [SerializeField] float _spawnForce;

    override public void Launch(Vector3 beg, Vector3 end)
    {
        transform.position = new Vector3(Random.Range(beg.x, end.x), beg.y, Random.Range(beg.z, end.z));
        _main.AddForce(Vector3.up * _spawnForce, ForceMode.Impulse);
        _main.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
    }
}
