using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDuplicate : MobSpawn
{
    [SerializeField] private List<GameObject> dupeGatesPassed;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DupeGate") && (gameObject.CompareTag("PlayerMob") || gameObject.CompareTag("EnemyMob")))
        {
            Duplicate(other.gameObject);
        }
    }

    void Duplicate(GameObject dupeGate)
    {
        if (!dupeGatesPassed.Contains(dupeGate))
        {
            spawnPos = gameObject.transform.position;
            spawnRot = gameObject.transform.rotation;

            dupeGatesPassed.Add(dupeGate);//mobs cannot pass the same gate more than once

            int dupeAmt = dupeGate.GetComponent<DupeGate>().GetDupeAmt();
            StartCoroutine(SpawnDelayed(0.1f, dupeGate, dupeAmt - 1));
        }
    }

    private IEnumerator SpawnDelayed(float delay, GameObject dupeGate, int spawnAmt)//a small delay is used to replicate the effect on the original game
    {
        for (int i = 0; i < spawnAmt; i++)
        {
            yield return new WaitForSeconds(delay);
            //GameObject dupedObj = RandomSpawn(gameObject.GetComponent<MobHealth>().GetMobIndex(), 4f);
            GameObject dupedObj = Spawn(spawnPos, spawnRot, gameObject.GetComponent<MobHealth>().GetMobIndex());
            dupedObj.GetComponent<MobDuplicate>().AddDupeGate(dupeGate);
        }
        
    }
    public void AddDupeGate(GameObject dupeGate)
    {
        dupeGatesPassed.Add(dupeGate);
    }
    public void ResetDupeGate()
    {
        dupeGatesPassed.Clear();
    }
}
