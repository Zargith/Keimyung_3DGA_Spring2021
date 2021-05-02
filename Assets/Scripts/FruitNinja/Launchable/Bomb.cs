using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Launchable
{
    [SerializeField] float _spawnForce;
    Rigidbody rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        BzKovSoft.ObjectSlicer.Samples.ObjectSlicerSample.OnSliced += yolo;
    }

    private void OnDisable()
    {
        BzKovSoft.ObjectSlicer.Samples.ObjectSlicerSample.OnSliced -= yolo;
    }


    new void yolo(int id)
    {
        if (gameObject.GetInstanceID().Equals(id))
        {
            man.addToScore(-10000);
        }
    }

    override public void Launch(Vector3 beg, Vector3 end)
    {
        transform.position = new Vector3(Random.Range(beg.x, end.x), beg.y, Random.Range(beg.z, end.z));
        rb.AddForce(Vector3.up * _spawnForce, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
    }
}
