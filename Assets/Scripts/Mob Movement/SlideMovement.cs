using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour
{
    [Header("- - - References- - -")]

    [SerializeField] private GameObject playerCannon;
    [SerializeField] private BoxCollider sliderCollider;
    [Header("- - - Variables - - -")]
    private Vector3 mOffset;
    private float mZCoord;

    void Start()
    {
        //initalize references
        playerCannon = transform.Find("PlayerCannon").gameObject;
        sliderCollider = GetComponent<BoxCollider>();
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(playerCannon.transform.position).z;//needed for correct screentoworld conversion
        // Store offset = gameobject world pos - mouse world pos
        mOffset = playerCannon.transform.position - GetMouseAsWorldPoint(mZCoord);
    }

    void OnMouseDrag()
    {
        Vector3 targetPos = new Vector3(GetMouseAsWorldPoint(mZCoord).x + mOffset.x, playerCannon.transform.position.y, playerCannon.transform.position.z);//only move on x axis

        if (sliderCollider.bounds.Contains(targetPos))//dont move cannon outside ofthe slider
        {
            playerCannon.transform.position = targetPos;
        }
    }

    public Vector3 GetMouseAsWorldPoint(float mouseZCoord)
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z cordinate of game object on screen
        mousePoint.z = mouseZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
