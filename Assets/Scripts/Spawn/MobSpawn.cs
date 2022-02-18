using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    [Header("- - - References- - -")]

    [SerializeField] protected ObjectPooler OP;
    [SerializeField] protected GameObject spawnPivot;

    [Header("- - - Variables - - -")]

    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Quaternion spawnRot;
    [SerializeField] protected float randSpawnRange = 2f;
  
    
    // Start is called before the first frame update
    private void Awake()
    {
        OP = ObjectPooler.SharedInstance;
    }

    protected GameObject Spawn(Vector3 pos, Quaternion rot, int mobIndex)//spawn and walk forward
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        if (playerMob.CompareTag("EnemyMob") || playerMob.CompareTag("PlayerMob"))
        {
            playerMob.GetComponent<MobHealth>().Initalize();
            playerMob.GetComponent<MobMovement>().SetVelocityTarget(null);
            //need to initazlie values again because of objectpooler
        }
        if (Physics.CheckSphere(pos, 0.5f))//check if spawn pos intersects with wall
        {
            pos = GiveRandomPos(randSpawnRange);
        }
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().SetVelocityFront();

        return playerMob;
    }

    private float prevRand = 0f;
    protected Vector3 GiveRandomPos(float range)
    {
        float rand = Random.Range(-range, range);
        if (rand == prevRand)//dont spawn on top of each other
            rand += 1f;
        prevRand = rand;
        return new Vector3(spawnPos.x + rand, spawnPos.y, spawnPos.z);
    }
    protected GameObject RandomSpawn(int mobIndex, float range)//spawn in a range
    {
        Vector3 randomPos = GiveRandomPos(range);
        //rand *= 0.1f;
        return Spawn(randomPos, spawnRot, mobIndex);
    }

    protected void SetSpawnPivot(GameObject obj)
    {
        spawnPivot = obj;
    }
}
