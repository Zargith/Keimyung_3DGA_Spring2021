using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] Transform _spawnContainer;
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
        Time.timeScale = 0.75f;
        Invoke("randomSpawn", 5);
    }

    void randomSpawn()
    {
        _started = true;
        GameObject spawned = Instantiate(_spawnable[Random.Range(0, _roundNumber)], _spawnContainer);
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


#if UNITY_EDITOR // This doesn't build properly, so we ignore it when building
    private void OnDrawGizmos()
    {
        if (Selection.activeObject == gameObject || Selection.activeObject == _spawnZoneOrigin.gameObject || Selection.activeObject == _spawnZoneEnd.gameObject)
        {
            Gizmos.color = new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube((_spawnZoneOrigin.position + _spawnZoneEnd.position) / 2, (_spawnZoneOrigin.position - _spawnZoneEnd.position));
            return;
        }
    }
#endif

}
