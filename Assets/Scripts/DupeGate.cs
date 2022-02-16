using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DupeGate : MonoBehaviour
{
    [SerializeField] private int dupeAmt;

    public int GetDupeAmt()
    {
        return dupeAmt;
    }
}
