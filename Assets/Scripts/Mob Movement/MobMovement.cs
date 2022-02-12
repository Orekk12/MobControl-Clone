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

    private Rigidbody m_Rigidbody;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        defMoveSpeed = moveSpeed;
    }
    private void Update()
    {
        if (onMove)
        {
            if (gameObject.CompareTag("PlayerMob"))//player mob walks in the opposite way of enemy mob
            {
                //m_Rigidbody.AddForce(-Vector3.forward.normalized * moveSpeed * 50);
                m_Rigidbody.velocity = -Vector3.forward.normalized * moveSpeed;
            }
            else
            {
                //m_Rigidbody.AddForce(Vector3.forward.normalized * moveSpeed * 50);
                m_Rigidbody.velocity = Vector3.forward.normalized * moveSpeed;
            }
            
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
    private IEnumerator DeactivateDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
