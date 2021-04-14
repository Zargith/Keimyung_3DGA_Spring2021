using UnityEngine;

public class Kiwi : Launchable
{
    [SerializeField] float _spawnForce;
    Rigidbody rb;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    override public void Launch(Vector3 beg, Vector3 end)
    {
        transform.localPosition = new Vector3(Random.Range(beg.x, end.x), beg.y, Random.Range(beg.z, end.z));
        rb.AddForce(Vector3.up * _spawnForce, ForceMode.Impulse);
        rb.AddTorque(new Vector3(Random.value, Random.value, Random.value) * Random.Range(1, 10), ForceMode.Impulse);
    }

}
