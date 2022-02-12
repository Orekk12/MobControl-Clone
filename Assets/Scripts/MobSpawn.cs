using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MonoBehaviour
{
    [SerializeField] protected ObjectPooler OP;
    [SerializeField] protected GameObject spawnPivot;
    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Quaternion spawnRot;
    [SerializeField] protected float spawnCooldown = 1f;
    protected float lastSpawnTime = 0f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        OP = ObjectPooler.SharedInstance;
    }
    private void Update()
    {
        //if (spawnPivot != null)
        //{
        //    spawnPos = spawnPivot.transform.position;
        //    spawnRot = spawnPivot.transform.rotation;
        //}
        
    }

    protected void Spawn(Vector3 pos, Quaternion rot, Vector3 targetPos, int mobIndex)
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().SetTargetPos(targetPos);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    protected void Spawn(Vector3 pos, Quaternion rot, GameObject targetObj, int mobIndex)
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        playerMob.SetActive(true);
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.GetComponent<MobMovement>().SetTargetObj(targetObj);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    protected void Spawn(Vector3 pos, Quaternion rot, int mobIndex)
    {
        GameObject playerMob = OP.GetPooledObject(mobIndex);
        playerMob.SetActive(true);
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    protected void SetSpawnPivot(GameObject obj)
    {
        spawnPivot = obj;
    }

}
