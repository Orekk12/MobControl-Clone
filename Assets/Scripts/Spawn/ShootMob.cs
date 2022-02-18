using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMob : MobSpawn
{
    [Header("- - - References- - -")]

    [SerializeField] private GameObject releaseText;

    [Header("- - - Variables - - -")]
    [SerializeField] private int bigMobFillAmt = 10;
    [SerializeField] private int currAmt = 0;
    [SerializeField] private bool readyToShootBigMob = false;
    [SerializeField] protected float spawnCooldown = 1f;

    protected float lastSpawnTime = 0f;

    public enum OccilationFuntion { Sine, Cosine }
    [SerializeField] private float slideRange = 3f;
    private float oscillateOffset;

    void Start()
    {
        OP = ObjectPooler.SharedInstance;
        releaseText = transform.GetChild(0).GetChild(1).gameObject;
        SetSpawnPivot(transform.GetChild(0).gameObject.transform.GetChild(0).gameObject);
    }
    private void Update()
    {
        if (spawnPivot != null)
        {
            spawnPos = spawnPivot.transform.position;
            spawnRot = spawnPivot.transform.rotation;
        }
        if (currAmt >= bigMobFillAmt)//shoot big mob
        {
            readyToShootBigMob = true;
            StartCoroutine(Oscillate(OccilationFuntion.Sine, slideRange));
        }
        
    }

    private void OnMouseDrag()
    {
        if (lastSpawnTime + spawnCooldown <= Time.time)//shooting rate
        {
            GameObject obj = Spawn(spawnPos, spawnRot, 0);
            obj.GetComponent<MobMovement>().StartMovement();
            currAmt++;
            lastSpawnTime = Time.time;
        }
    }

    private void OnMouseUp()
    {
        if (readyToShootBigMob)//release big mob
        {
            GameObject obj = Spawn(spawnPos, spawnRot, 2);
            obj.GetComponent<MobMovement>().StartMovement();
            readyToShootBigMob = false;
            releaseText.SetActive(false);
            currAmt = 0;


        }
    }

    private IEnumerator Oscillate(OccilationFuntion method, float scalar)//release text
    {
        releaseText.SetActive(true);
        while (readyToShootBigMob)
        {
            if (method == OccilationFuntion.Sine)
            {
                var scale = oscillateOffset + Mathf.Abs(Mathf.Sin(Time.time * 1.5f)) * scalar;
                scale += 0.6f;
                releaseText.transform.localScale = new Vector3(scale, scale, scale);
            }
            else if (method == OccilationFuntion.Cosine)
            {
                releaseText.transform.localScale = new Vector3(oscillateOffset + Mathf.Abs(Mathf.Cos(Time.time * 1.5f)) * scalar, releaseText.transform.localScale.y, releaseText.transform.localScale.z);
            }
           
            //gameObject.transform.position += oscilatePOs;
            yield return new WaitForEndOfFrame();
        }
    }

}
