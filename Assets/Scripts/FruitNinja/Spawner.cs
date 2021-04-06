using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform _spawnZoneOrigin;
    [SerializeField] Transform _spawnZoneEnd;
    [SerializeField] GameObject[] _spawnable;
    [SerializeField] [Range(0, 10)] float _intervalMin;
    [SerializeField] [Range(0, 5)] float _intervalVariationMax;
    [SerializeField]  float _spawnForce;
    public bool _stop = false;


    void Start()
    {
        Invoke("randomSpawn", 5);
    }

    void randomSpawn()
    {
        GameObject spawned = Instantiate(_spawnable[Random.Range(0, _spawnable.Length)], transform);
        spawned.GetComponent<Launchable>().Launch(_spawnZoneOrigin.localPosition, _spawnZoneEnd.localPosition);

        if (!_stop)
        {
            Invoke("randomSpawn", _intervalMin + Random.Range(0, _intervalVariationMax));
        }

    }

    private void OnDrawGizmos()
    {
        if (Selection.activeObject == gameObject || Selection.activeObject == _spawnZoneOrigin.gameObject || Selection.activeObject == _spawnZoneEnd.gameObject)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(_spawnZoneOrigin.localPosition + (_spawnZoneEnd.localPosition / 2), _spawnZoneEnd.localPosition);
            return;
        }
    }

}
