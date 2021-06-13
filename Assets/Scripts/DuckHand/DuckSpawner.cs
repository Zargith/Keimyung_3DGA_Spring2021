using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;

    [SerializeField] private float InitialCd = 4;
    [SerializeField] private float CdChange = -0.1f;
    [SerializeField] private float MinCd = 2;

    [SerializeField] private float MinApexHeight = 5;
    [SerializeField] private float MaxApexHeight = 7;

    [SerializeField] private float SpawnZoneCenterX;
    [SerializeField] private float SpawnZoneCenterZ;
    [SerializeField] private float SpawnZoneSizeX = 20;
    [SerializeField] private float SpawnZoneSizeZ = 5;


    private Coroutine m_spawningCoroutine;
    private float m_currentCd;

    public void StartSpawning()
    {
        if (m_spawningCoroutine != null)
        {
            Debug.LogError("Called DuckSpawner.StartSpawning() but already spawning :(");
            return;
        }

        m_spawningCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopSpawning()
    {
        StopCoroutine(m_spawningCoroutine);
        m_spawningCoroutine = null;
    }

    public void ClearAllDucks()
    {
        foreach (Transform duck in transform)
        {
            Destroy(duck.gameObject);
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        m_currentCd = InitialCd;

        while (true)
        {
            SpawnDuck();
            yield return new WaitForSeconds(m_currentCd);
        }
    }

    private void Update()
    {
        m_currentCd = Mathf.Max(m_currentCd + CdChange * Time.deltaTime, MinCd);
    }

    private void SpawnDuck()
    {
        var minX = SpawnZoneCenterX - 0.5f * SpawnZoneSizeX;
        var maxX = SpawnZoneCenterX + 0.5f * SpawnZoneSizeX;
        var minZ = SpawnZoneCenterZ - 0.5f * SpawnZoneSizeZ;
        var maxZ = SpawnZoneCenterZ + 0.5f * SpawnZoneSizeZ;

        var P0 = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));

        var duck = Instantiate(EnemyPrefab, P0, Quaternion.identity, transform);

        var rb = duck.GetComponent<Rigidbody>();

        rb.angularVelocity = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), Random.Range(-3f, 3f));

        var V0 = new Vector3();

        { // Velocity computation
            var apex = Random.Range(MinApexHeight, MaxApexHeight);

            var Gy = Physics.gravity.y;

            // Time at apex
            var Tmax = Mathf.Sqrt(-2 * (apex - P0.y) / Gy);

            // Time of return to initial heigth ( = hit the ground, as we always spawn at y=0)
            var Tfall = 2 * Tmax;


            V0.x = Random.Range(((minX - P0.x) / Tfall), ((maxX - P0.x) / Tfall));
            V0.z = Random.Range(((minZ - P0.z) / Tfall), ((maxZ - P0.z) / Tfall));
            V0.y = -Gy * Tmax;
        }

        rb.velocity = V0;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(
            new Vector3(SpawnZoneCenterX, MaxApexHeight / 2, SpawnZoneCenterZ),
            new Vector3(SpawnZoneSizeX, MaxApexHeight, SpawnZoneSizeZ)
        );

    }
}
