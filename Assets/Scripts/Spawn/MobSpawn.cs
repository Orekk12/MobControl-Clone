using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    [SerializeField] protected ObjectPooler OP;
    [SerializeField] protected GameObject spawnPivot;
    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Quaternion spawnRot;
  
    
    // Start is called before the first frame update
    private void Awake()
    {
        OP = ObjectPooler.SharedInstance;
    }

    protected void Spawn(Vector3 pos, Quaternion rot, GameObject targetObj, int mobIndex)//spawn with target objct
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        if (playerMob.CompareTag("EnemyMob") || playerMob.CompareTag("PlayerMob"))
        {
            playerMob.GetComponent<MobHealth>().SetToMAXHP();
        }
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().SetTargetObj(targetObj);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    protected GameObject Spawn(Vector3 pos, Quaternion rot, int mobIndex)//spawn and walk forward
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        if (playerMob.CompareTag("EnemyMob") || playerMob.CompareTag("PlayerMob"))
        {
            playerMob.GetComponent<MobHealth>().SetToMAXHP();
        }
        if (Physics.CheckSphere(pos, 0.5f))
        {
            pos = GiveRandomPos(2f);
        }
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().StartMovement();

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
    protected GameObject RandomSpawn(int mobIndex, float range)
    {
        Vector3 randomPos = GiveRandomPos(range);
        //rand *= 0.1f;
        return Spawn(randomPos, spawnRot, mobIndex);
    }

    /*protected IEnumerator SpawnBig5(int mobIndex)
    {
      
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.01f);
            RandomSpawn(mobIndex, 4);
        }
    }*/

    protected void SetSpawnPivot(GameObject obj)
    {
        spawnPivot = obj;
    }
}
