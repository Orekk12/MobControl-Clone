using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private int HP = 1;
    [SerializeField] private int maxHP = 1;
    private Vector3 defaultScale;
    private void Awake()
    {
        defaultScale = transform.localScale;
        HP = maxHP;
        if (HP > 1 && (gameObject.CompareTag("EnemyMob") || gameObject.CompareTag("PlayerMob")))
        {
            transform.localScale = defaultScale * (Mathf.Sqrt(HP));
        }
    }
    void LateUpdate()
    {
        if (HP <= 0 && (gameObject.CompareTag("EnemyMob") || gameObject.CompareTag("PlayerMob")))
        {
            //kill mobs
            gameObject.SetActive(false);
            //Debug.Log("kill hp:" + HP);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        //if mobs hit each other
        if (gameObject != null && other.gameObject != null)
        {
            if (gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerMob") || gameObject.CompareTag("PlayerMob") && other.gameObject.CompareTag("EnemyMob"))
            {
                //other.gameObject.GetComponent<MobHealth>().ReduceHP(maxHP);
                StartCoroutine(other.gameObject.GetComponent<MobHealth>().DelayedReduceHP(1));
                //Debug.Log("hit" + other.gameObject.name);
            }
            else if (gameObject.CompareTag("PlayerMob") && other.gameObject.CompareTag("EnemyFort") || gameObject.CompareTag("EnemyFort") && other.gameObject.CompareTag("PlayerMob"))
            {
                other.gameObject.GetComponent<MobHealth>().ReduceHP(1);
            }
            else if (gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerArea"))//if enemies enter player line
            {
                //game over
            }
        }
    }

    public void ReduceHP(int dmg)
    {
        HP -= dmg;
    }
    public IEnumerator DelayedReduceHP(int dmg)
    {
        gameObject.GetComponent<MobMovement>().SetMoveSpeed(0f);
        yield return new WaitForSeconds(0.7f);
        gameObject.GetComponent<MobMovement>().SetDefMoveSpeed();
        HP -= dmg;
        if (HP < 0)
            HP = 0;
        if (HP > 0)
        {
            transform.localScale = defaultScale * (Mathf.Sqrt(HP));
        }
    }
    public void SetToMAXHP()
    {
        HP = maxHP;
    }
}
