using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerCannon;
    [SerializeField] private BoxCollider sliderCollider;
    private Vector3 mOffset;
    private float mZCoord;
    // Start is called before the first frame update
    void Start()
    {
        playerCannon = transform.Find("PlayerCannon").gameObject;
        sliderCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(playerCannon.transform.position).z;
        // Store offset = gameobject world pos - mouse world pos
        mOffset = playerCannon.transform.position - GetMouseAsWorldPoint(mZCoord);
    }

    void OnMouseDrag()
    {
        Vector3 targetPos = new Vector3(GetMouseAsWorldPoint(mZCoord).x + mOffset.x, playerCannon.transform.position.y, playerCannon.transform.position.z);

        if (sliderCollider.bounds.Contains(targetPos))
        {
            playerCannon.transform.position = targetPos;
        }
    }

    private Vector3 GetMouseAsWorldPoint(float mouseZCoord)
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z cordinate of game object on screen
        mousePoint.z = mouseZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
