using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortManager : MonoBehaviour
{
    [SerializeField] private GameObject fortText;
    [SerializeField] private MobHealth hpScript;
    private float fortHitCD = 1f;
    private float fortLastHitTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        fortText = transform.GetChild(2).gameObject;
        hpScript = GetComponent<MobHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        fortText.GetComponent<TextMesh>().text = hpScript.GetHP().ToString();

    }

    public bool CheckHitCD()
    {
        if (fortLastHitTime + fortHitCD <= Time.time)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FortHitEffect(GameObject effect, Vector3 pos, Quaternion rot)
    {
        if (CheckHitCD())
        {
            GameObject hitP = Instantiate(effect, pos, rot);
            Destroy(hitP, 4f);
            fortLastHitTime = Time.time;
        }
    }
}