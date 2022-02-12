using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMob : MobSpawn
{
    //[SerializeField] private GameObject enemyFort;
    [SerializeField] protected float spawnCooldown = 1f;
    protected float lastSpawnTime = 0f;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        //enemyFort = GameObject.Find("GoalFort");
        SetSpawnPivot(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
    }
    private void Update()
    {
        if (spawnPivot != null)
        {
            spawnPos = spawnPivot.transform.position;
            spawnRot = spawnPivot.transform.rotation;
        }
        
    }

    private void OnMouseDrag()
    {
        if (lastSpawnTime + spawnCooldown <= Time.time)
        {
            Spawn(spawnPos, spawnRot, 0);
            lastSpawnTime = Time.time;
        }
    }
}
