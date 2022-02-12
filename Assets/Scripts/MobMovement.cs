using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{

    [SerializeField] private Vector3 targetPos;
    [SerializeField] private GameObject targetObj;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool onMove = false;
    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    public void StartMovement()
    {
        if (targetObj != null && !onMove)
        {
            targetPos = targetObj.transform.position;
            Vector3 targetDir = targetPos - transform.position;
            m_Rigidbody.AddForce(targetDir * moveSpeed);
            onMove = true;
        }
        else if (targetPos != Vector3.zero && !onMove)
        {
            Vector3 targetDir = targetPos - transform.position;
            m_Rigidbody.AddForce(targetDir * moveSpeed);
            onMove = true;
        }
        else if(!onMove)
        {
            m_Rigidbody.AddForce(Vector3.forward * moveSpeed * 50);
            onMove = true;
        }
    }
    public void SetTargetObj(GameObject tObj)
    {
        targetObj = tObj;
    }
    public void SetTargetPos(Vector3 tPos)
    {
        targetPos = tPos;
    }
    public void SetMoveSpeed(float ms)
    {
        moveSpeed = ms;
    }
}
