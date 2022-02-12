using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MobSpawn
{
    [SerializeField] protected float waveSpawnCooldown = 2f;
    protected float waveLastSpawnTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        spawnCooldown = 0f;
        SetSpawnPivot(transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawnPivot != null)
        {
            spawnPos = spawnPivot.transform.position;
            spawnRot = spawnPivot.transform.rotation;
        }
        if (Input.GetKey(KeyCode.G))
        {
            SpawnWave(1, 0);
        }
    }

    private void SpawnWave(int noSmallMob, int noBigMob)
    {
        if (waveLastSpawnTime + waveSpawnCooldown <= Time.time)
        {
            SpawnMobs(noSmallMob, 1);
            SpawnMobs(noBigMob, 2);
            waveLastSpawnTime = Time.time;
        }
    }

    private void SpawnMobs(int noMobs, int mobIndex)
    {
        for (int i = 0; i < noMobs; i++)
        {
            Spawn(spawnPos, spawnRot, mobIndex);
        }
    }
}
