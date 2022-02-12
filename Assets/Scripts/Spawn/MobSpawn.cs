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
    protected void Spawn(Vector3 pos, Quaternion rot, int mobIndex)//spawn and walk forward
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        if (playerMob.CompareTag("EnemyMob") || playerMob.CompareTag("PlayerMob"))
        {
            playerMob.GetComponent<MobHealth>().SetToMAXHP();
        }
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    protected void SetSpawnPivot(GameObject obj)
    {
        spawnPivot = obj;
    }
}
