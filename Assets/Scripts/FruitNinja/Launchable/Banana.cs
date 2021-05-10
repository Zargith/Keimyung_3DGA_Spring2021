using UnityEngine;

public class Banana : Launchable
{
    [SerializeField] float _spawnForce;

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
        _main.AddForce(dir.normalized * _spawnForce, ForceMode.Impulse);
        _main.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
    }

}