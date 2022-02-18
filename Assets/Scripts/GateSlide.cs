using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSlide : MonoBehaviour
{
    public enum OccilationFuntion { Sine, Cosine }
    [SerializeField] private float slideRange = 3f;
    private float oscillateOffset;

    public void Start()
    {
        oscillateOffset = gameObject.transform.position.x;
        //to start at zero
        StartCoroutine(Oscillate(OccilationFuntion.Sine, slideRange));
        //to start at scalar value
        //StartCoroutine (Oscillate (OccilationFuntion.Cosine, 1f));
    }

    private IEnumerator Oscillate(OccilationFuntion method, float scalar)
    {
        while (true)
        {
            if (method == OccilationFuntion.Sine)
            {
                gameObject.transform.position = new Vector3(oscillateOffset + Mathf.Sin(Time.time) * scalar, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            else if (method == OccilationFuntion.Cosine)
            {
                gameObject.transform.position = new Vector3(oscillateOffset + Mathf.Cos(Time.time) * scalar, gameObject.transform.position.y, gameObject.transform.position.z);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
