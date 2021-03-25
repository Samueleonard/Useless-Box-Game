using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // The position that that camera will be following.
    public Transform[] targetsHorizontal; 
    public GameObject targetV;

    Vector3 inT; //when we go vertical, remember where we came from

    public int currentTargetHIndex = 0; //which target we are currently on horizontally
    // The speed with which the camera will be following.           
    public float smoothing = 2f;        

    public bool vertical; //are we currently in vertical view

    void FixedUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentTargetHIndex == 0)
                currentTargetHIndex = targetsHorizontal.Length-1;
            else
                currentTargetHIndex--;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentTargetHIndex == targetsHorizontal.Length - 1)
                currentTargetHIndex = 0;
            else
                currentTargetHIndex++;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.Log("going up");
            inT = targetsHorizontal[currentTargetHIndex].transform.position;
            vertical = true;
            Vector3 targetCamPos = targetV.transform.position;
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);              
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("going down");
            vertical = false;
            Vector3 targetCamPos = inT;
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);              
        }

        if(!vertical)
        {
            Vector3 targetCamPos = targetsHorizontal[currentTargetHIndex].position;
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);
        }
    }
}
