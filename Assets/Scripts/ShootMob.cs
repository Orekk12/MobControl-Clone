using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMob : MonoBehaviour
{
    [SerializeField] private ObjectPooler OP;
    // Start is called before the first frame update
    void Start()
    {
        OP = GameObject.Find("Spawner").GetComponent<ObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
