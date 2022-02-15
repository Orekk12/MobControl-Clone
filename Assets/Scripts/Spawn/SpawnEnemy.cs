using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MobSpawn
{
    [SerializeField] protected float waveSpawnDensity = 0.01f;
    [SerializeField] private float waveSpawnPeriod = 5f;
    [SerializeField] private int waveEnemyAmt = 5;
    private bool canSpawn = true;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        SetSpawnPivot(transform.GetChild(0).gameObject);

        if (spawnPivot != null)
        {
            spawnPos = spawnPivot.transform.position;
            spawnRot = spawnPivot.transform.rotation;
        }

        StartCoroutine(SpawnPeriodically(50, waveSpawnPeriod));
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.G) && canSpawn)
        //{
        //    StartCoroutine(SpawnWave(1, waveSpawnDensity, 20));
        //    canSpawn = false;
        //}
    }

    private IEnumerator SpawnPeriodically(int amt, float period)
    {
        for (int i = 0; i < amt; i++)
        {
            if (canSpawn)
            {
                yield return new WaitForSeconds(period);
                StartCoroutine(SpawnWave(1, waveSpawnDensity, waveEnemyAmt));
                //StartCoroutine(SpawnWave(3, waveSpawnDensity, 3));
            }
        }
    }
    private IEnumerator SpawnWave(int mobIndex, float delay, int amt)
    {
        for (int i = 0; i < amt; i++)
        {
            yield return new WaitForSeconds(delay);
            SpawnMob(mobIndex);
        }
    }

    private void SpawnMob(int mobIndex)
    {
        GameObject obj = RandomSpawn(mobIndex, 4);
        obj.GetComponent<MobMovement>().StartMovement();
    }

    
    //private void SpawnWave(int mobIndex)
    //{
    //    if (waveLastSpawnTime + waveSpawnDensity <= Time.time)
    //    {
    //        SpawnMob(mobIndex);
    //    }
    //}
}
