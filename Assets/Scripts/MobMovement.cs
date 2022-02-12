using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private float moveSpeed;

    private Rigidbody m_Rigidbody;

    void Start()
    {
        targetPos = GameObject.Find("GoalFort").transform.position;//debug
        m_Rigidbody = GetComponent<Rigidbody>();

        if (targetPos != Vector3.zero)
        {
            Vector3 targetDir = targetPos - transform.position;
            m_Rigidbody.AddForce(targetDir * moveSpeed);
        }
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
