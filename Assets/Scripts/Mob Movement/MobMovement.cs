using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{

    [SerializeField] private Vector3 targetPos;
    [SerializeField] private GameObject targetObj;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float defMoveSpeed;
    [SerializeField] private bool onMove = false;
    [SerializeField] private Vector3 targetVelocity;
    [SerializeField] private GameObject targetVelocityObj;

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        defMoveSpeed = moveSpeed;
    }
    private void Start()
    {
        if (gameObject.CompareTag("PlayerMob"))//player mob walks in the opposite way of enemy mob
        {
            targetVelocity = -Vector3.forward.normalized * moveSpeed;
        }
        else
        {
            targetVelocity = Vector3.forward.normalized * moveSpeed;
        }
    }
    private void Update()
    {
        if (onMove && targetVelocityObj == null)
        {
            m_Rigidbody.velocity = targetVelocity;
        }
        else if (onMove && targetVelocityObj != null)
        {
            m_Rigidbody.velocity = (targetVelocityObj.gameObject.transform.position - transform.position).normalized * GetMoveSpeed();
        }

    }
    public void StartMovement()
    {
        onMove = true;

        DeactivateDelay(30f);
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
        if (ms == 0)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
            moveSpeed = ms;
    }
    public void SetDefMoveSpeed()
    {
        moveSpeed = defMoveSpeed;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetVelocity(Vector3 vel)
    {
        targetVelocity = vel;
    }
    public void SetVelocityTarget(GameObject velTarget)
    {
        targetVelocityObj = velTarget;
    }
    private IEnumerator DeactivateDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
