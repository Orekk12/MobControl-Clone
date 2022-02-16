using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobHealth : MonoBehaviour
{
    [SerializeField] private int HP = 1;
    [SerializeField] private int maxHP = 1;
    [SerializeField] private bool isDead = false;
    [SerializeField] private int mobIndex = 1;
    [SerializeField] private GameObject hitParticle;
    private Vector3 defaultScale;
    private void Awake()
    {
        //ffffffdefaultScale = transform.localScale;
        /*HP = maxHP;
        if (HP > 1 && (gameObject.CompareTag("EnemyMob") || gameObject.CompareTag("PlayerMob")))
        {
            Initalize();
        }*/
    }
    void Update()
    {
        if (isDead && (gameObject.CompareTag("EnemyMob") || gameObject.CompareTag("PlayerMob")))
        {
            //kill mobs
            StartCoroutine(MobKill()); 
        }
    }

    public void Initalize()
    {
        SetToMAXHP();
        isDead = false;
        defaultScale = transform.localScale;
        transform.localScale = defaultScale * (Mathf.Sqrt(HP));
        
    }
    private void OnCollisionEnter(Collision other)
    {
        //if mobs hit each other
        if (gameObject != null && other.gameObject != null)
        {
            if (/*gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerMob") ||*/ gameObject.CompareTag("PlayerMob") && other.gameObject.CompareTag("EnemyMob"))
            {
                int dmg = other.gameObject.GetComponent<MobHealth>().GetHP();
                if (!isDead)
                {
                    other.gameObject.GetComponent<MobHealth>().GetHit(HP);//hit other
                }
                GetHit(dmg);//get hit
            }
            else if (gameObject.CompareTag("PlayerMob") && other.gameObject.CompareTag("EnemyFort") )
            {
                //StartCoroutine(other.gameObject.GetComponent<MobHealth>().DelayedReduceHP(1));
                HitFort(other.gameObject);

            }
            else if (gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerArea"))//if enemies enter player line
            {
                //game over
                Debug.Log("area hit");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("EnemyMob") && other.gameObject.CompareTag("PlayerArea"))//if enemies enter player line
        {
            //game over
            Debug.Log("area hit");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
    }

    public void GetHit(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
        }
        else
        {
            transform.localScale = defaultScale * (Mathf.Sqrt(HP));
        }
    }

    public IEnumerator GetHit(GameObject other, bool hitOther)
    {
        MobHealth hpScript = other.GetComponent<MobHealth>();
        if (hitOther)
        {
            StartCoroutine(hpScript.GetHit(gameObject, false));
        }
        if (!hpScript.GetIsDead())
        {
            //HP -= hpScript.GetHP();
            if (HP - hpScript.GetHP() <= 0)
            {
                isDead = true;
                GetComponent<MobMovement>().SetMoveSpeed(0f);
                yield return new WaitForSeconds(0.7f);
                GetComponent<MobMovement>().SetDefMoveSpeed();
                HP = 0;
            }
            else
            {
                HP -= hpScript.GetHP();
                transform.localScale = defaultScale * (Mathf.Sqrt(HP));
            }
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }
    public int GetHP()
    {
        return HP;
    }
    public IEnumerator MobKill()
    {
        GetComponent<MobMovement>().SetMoveSpeed(0f);
        yield return new WaitForSeconds(0.7f);
        GetComponent<MobMovement>().SetDefMoveSpeed();
        GetComponent<MobDuplicate>().ResetDupeGate();
        transform.localScale = defaultScale;
        gameObject.SetActive(false);
        //Debug.Log("kill" + gameObject.name);
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
    public void HitFort(GameObject targetFort)
    {
        targetFort.GetComponent<MobHealth>().ReduceHP(HP);
        var fortMng = targetFort.GetComponent<FortManager>();
        if (fortMng.CheckHitCD())
        {
            fortMng.FortHitEffect(hitParticle, targetFort.transform.GetChild(1).gameObject.transform.position, targetFort.transform.GetChild(1).gameObject.transform.rotation);
            fortMng.reduceHP();
        }
        GetHit(HP);
    }
    public void SetToMAXHP()
    {
        HP = maxHP;
    }
    public int GetMobIndex()
    {
        return mobIndex;
    }
    public void SetDefScale(Vector3 scale)
    {
        defaultScale = scale;
    }
}
