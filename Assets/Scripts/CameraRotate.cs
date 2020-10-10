using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float dragSpeed = 1f;
    public GameObject pivotPoint;
    private void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1)){
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - pivotPoint.transform.position);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);
            transform.LookAt(pivotPoint.transform);
            transform.Translate(move, Space.World);
        }
    }
}
