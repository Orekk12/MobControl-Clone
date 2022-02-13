using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private int HP = 1;
    [SerializeField] private int maxHP = 1;
    [SerializeField] private int mobIndex = 1;
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
            MobKill();
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
            else if (gameObject.CompareTag("PlayerMob") && other.gameObject.CompareTag("EnemyFort") )
            {
                //StartCoroutine(other.gameObject.GetComponent<MobHealth>().DelayedReduceHP(1));
                StartCoroutine(HitFort(other.gameObject));

            }
            else if (gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerArea"))//if enemies enter player line
            {
                //game over
            }
        }
    }

    public void MobKill()
    {
        GetComponent<MobDuplicate>().ResetDupeGate();
        gameObject.SetActive(false);
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
        if (HP > 0 && !gameObject.CompareTag("EnemyFort"))
        {
            transform.localScale = defaultScale * (Mathf.Sqrt(HP));
        }
    }
    public IEnumerator HitFort(GameObject targetFort)
    {
        gameObject.GetComponent<MobMovement>().SetMoveSpeed(0f);
        yield return new WaitForSeconds(0.7f);
        targetFort.GetComponent<MobHealth>().ReduceHP(HP);
        HP = 0;
    }
    public void SetToMAXHP()
    {
        HP = maxHP;
    }
    public int GetMobIndex()
    {
        return mobIndex;
    }
}
