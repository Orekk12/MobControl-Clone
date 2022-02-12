using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsFort : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)//if player mobs are close enoguh to enemy fort
    {
        if (other.CompareTag("EnemyFort"))
        {
            GameObject parent = transform.parent.gameObject;
            parent.GetComponent<Rigidbody>().velocity = (other.gameObject.transform.position - transform.position).normalized * parent.GetComponent<MobMovement>().GetMoveSpeed();
        }
    }
}
