using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MobSpawn
{
    [Header("- - - Variables - - -")]
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

    private IEnumerator SpawnPeriodically(int amt, float period)//loops until level ends
    {
        for (int i = 0; i < amt; i++)
        {
            if (canSpawn)
            {
                yield return new WaitForSeconds(period);
                StartCoroutine(SpawnWave(1, waveSpawnDensity, waveEnemyAmt));
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
    }

   
}
