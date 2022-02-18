using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FortManager : MonoBehaviour
{
    [Header("- - - References- - -")]

    [SerializeField] private GameObject fortText;
    [SerializeField] private MobHealth hpScript;

    [Header("- - - Variables - - -")]

    private float fortHitCD = 1f;
    private float fortLastHitTime = 0f;

    void Start()
    {
        //initalize references
        fortText = transform.GetChild(2).gameObject;
        hpScript = GetComponent<MobHealth>();
        fortText.GetComponent<TextMesh>().text = hpScript.GetHP().ToString(); 
    }


    public void reduceHP()
    {
        fortText.GetComponent<TextMesh>().text = hpScript.GetHP().ToString();
        if (hpScript.GetHP() <= 0)
        {
            gameObject.SetActive(false);
            //gameover
            hpScript.LevelOver();
        }
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
