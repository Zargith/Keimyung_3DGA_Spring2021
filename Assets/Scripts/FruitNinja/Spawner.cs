using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform _spawnZoneOrigin;
    [SerializeField] Transform _spawnZoneEnd;
    [SerializeField] GameObject[] _spawnable;
    [SerializeField] [Range(0, 5)] float _intervalMin;
    public bool _stop = false;
    public int _roundTime;

    int _roundNumber = 1;
    private bool _started = false;
    private float elapsedTime = 0;


    void Start()
    {
        Invoke("randomSpawn", 5);
    }

    void randomSpawn()
    {
        _started = true;
        GameObject spawned = Instantiate(_spawnable[Random.Range(0, _roundNumber)], transform);
        spawned.GetComponent<Launchable>().Launch(_spawnZoneOrigin.position, _spawnZoneEnd.position);

        if (!_stop)
        {
            Invoke("randomSpawn", _intervalMin);
        }

    }

    private void Update()
    {
        if (_started && !_stop)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime - _roundTime > 0)
            {
                elapsedTime = 0;
                ++_roundNumber;
                _intervalMin -= 0.05f;
                if (_roundNumber > _spawnable.Length)
                {
                    _stop = true;
                }
            }
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
