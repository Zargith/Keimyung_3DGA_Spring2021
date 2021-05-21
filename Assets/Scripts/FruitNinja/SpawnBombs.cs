using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBombs : MonoBehaviour
{
    [SerializeField] Transform _spawnContainer;
    [SerializeField] Transform _spawnZoneOrigin;
    [SerializeField] Transform _spawnZoneEnd;
    [SerializeField] GameObject _bomb;
    [SerializeField] [Range(10, 30)] float _intervalMin;
    public bool _stop = false;

    void OnEnable()
    {
        Time.timeScale = 0.75f;
        Invoke("randomSpawn", 5 + _intervalMin);
    }

    void randomSpawn()
    {
        GameObject spawned = Instantiate(_bomb, _spawnContainer);
        spawned.GetComponent<Launchable>().Launch(_spawnZoneOrigin.position, _spawnZoneEnd.position);

        if (!_stop)
        {
            Invoke("randomSpawn", _intervalMin);
        }

    }

}
