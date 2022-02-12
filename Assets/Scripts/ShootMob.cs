using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMob : MonoBehaviour
{
    [SerializeField] private ObjectPooler OP;
    [SerializeField] private Vector3 shootPos;
    [SerializeField] private Quaternion shootRot;
    [SerializeField] private float fireCooldown = 1f;
    [SerializeField] private GameObject enemyFort;
    private float lastFireTime = 0f;
    private GameObject shootPivot;
    // Start is called before the first frame update
    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        shootPivot = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        enemyFort = GameObject.Find("GoalFort");
    }
    private void Update()
    {
        shootPos = shootPivot.transform.position;
        shootRot = shootPivot.transform.rotation;
    }

    private void OnMouseDrag()
    {
        if (lastFireTime + fireCooldown <= Time.time)
        {
            Shoot(shootPos, shootRot, enemyFort);
            lastFireTime = Time.time;
            Debug.Log("shoot");
        }
    }

    private void Shoot(Vector3 pos, Quaternion rot, Vector3 targetPos)
    {
        GameObject playerMob = OP.GetPooledObject(0);
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.SetActive(true);
        playerMob.GetComponent<MobMovement>().SetTargetPos(targetPos);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }
    private void Shoot(Vector3 pos, Quaternion rot, GameObject targetObj)
    {
        GameObject playerMob = OP.GetPooledObject(0);
        playerMob.SetActive(true);
        playerMob.transform.position = pos;
        playerMob.transform.rotation = rot;
        playerMob.GetComponent<MobMovement>().SetTargetObj(targetObj);
        playerMob.GetComponent<MobMovement>().StartMovement();
    }


}
