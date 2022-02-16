using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDuplicate : MobSpawn
{
    [SerializeField] private List<GameObject> dupeGatesPassed;
    // Start is called before the first frame update
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

            dupeGatesPassed.Add(dupeGate);

            int dupeAmt = dupeGate.GetComponent<DupeGate>().GetDupeAmt();
            StartCoroutine(SpawnDelayed(0.1f, dupeGate, dupeAmt - 1));
        }
    }

    private IEnumerator SpawnDelayed(float delay, GameObject dupeGate, int spawnAmt)
    {
        for (int i = 0; i < spawnAmt; i++)
        {
            yield return new WaitForSeconds(delay);
            GameObject dupedObj = RandomSpawn(gameObject.GetComponent<MobHealth>().GetMobIndex(), 4f);
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
