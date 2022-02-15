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
        SetVelocityFront();
        //StartCoroutine(SetStartMS());
    }
    private void Update()
    {
        if (targetVelocityObj == null)
        {
            m_Rigidbody.velocity = targetVelocity;
        }
        else if (targetVelocityObj != null)
        {
            m_Rigidbody.velocity = (targetVelocityObj.gameObject.transform.position - transform.position).normalized * GetMoveSpeed();
        }

    }
    public void StartMovement()
    {
        onMove = true;
        StartCoroutine(SetStartMS());
        //DeactivateDelay(30f);
    }

    private IEnumerator SetStartMS()
    {
        SetVelocity(targetVelocity * 1.5f);
        yield return new WaitForSeconds(0.7f);
        SetVelocity(targetVelocity /1.5f);

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
        {
            moveSpeed = ms;
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
    public void SetDefMoveSpeed()
    {
        SetMoveSpeed(defMoveSpeed);
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetVelocityFront()
    {
        if (gameObject.CompareTag("PlayerMob"))
        {
            SetVelocity(-Vector3.forward.normalized * moveSpeed);
        }
        else
        {
            SetVelocity(Vector3.forward.normalized * moveSpeed);
        }
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
