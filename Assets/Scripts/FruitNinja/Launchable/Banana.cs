using UnityEngine;

public class Banana : Launchable
{
    [SerializeField] float _spawnForce;
    Rigidbody rb;

    protected void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        BzKovSoft.ObjectSlicer.Samples.ObjectSlicerSample.OnSliced += yolo;
    }

    protected void OnDisable()
    {
        BzKovSoft.ObjectSlicer.Samples.ObjectSlicerSample.OnSliced -= yolo;
    }


    override public void Launch(Vector3 beg, Vector3 end)
    {
        float x = beg.x;
        Vector3 dir = (end - beg);
        if (Random.value > 0.5f)
        {
            x = end.x;
            dir = (beg - end);
        }
        dir.y = 0;
        dir.z = 0;
        transform.position = new Vector3(x, end.y, Random.Range(beg.z, end.z));
        rb.AddForce(dir.normalized * _spawnForce, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
        print(end);
    }

}