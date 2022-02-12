using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MobSpawn
{
    [SerializeField] protected float waveSpawnCooldown = 0.01f;
    protected float waveLastSpawnTime = 0f;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        SetSpawnPivot(transform.GetChild(0).gameObject);

        if (spawnPivot != null)
        {
            spawnPos = spawnPivot.transform.position;
            spawnRot = spawnPivot.transform.rotation;
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            SpawnWave(1);
            //StartCoroutine(Wave1());
        }
    }

    private void SpawnWave(int mobIndex)
    {
        if (waveLastSpawnTime + waveSpawnCooldown <= Time.time)
        {
            RandomSpawn(mobIndex, 4);
            waveLastSpawnTime = Time.time;
        }
    }

}
