using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsFort : MonoBehaviour
{
    [SerializeField] private bool foundFort = false;
    private void OnTriggerEnter(Collider other)//if player mobs are close enoguh to enemy fort
    {
        if (other.CompareTag("EnemyFort"))
        {
            GameObject parent = transform.parent.gameObject;
            MobMovement moveScript = parent.GetComponent<MobMovement>();
            moveScript.SetVelocityTarget(other.gameObject);
            foundFort = true;
        }
    }
}
