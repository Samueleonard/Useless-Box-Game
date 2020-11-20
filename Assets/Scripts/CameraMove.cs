using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // The position that that camera will be following.
    public Transform[] targetsHorizontal; 
    public Transform[] targetsVertical;

    public int currentTargetHIndex = 0; //which target we are currently on horizontally
    public int currentTargetVIndex = 0; //which target we are currently on vertically
    // The speed with which the camera will be following.           
    public float smoothing = 2f;        

    // Start is called before the first frame update
    void Start()
    {                  
        Application.targetFrameRate = 60;   
    }

    void FixedUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(currentTargetHIndex < 1)
            {
                currentTargetHIndex = targetsHorizontal.Length-1;
            }
            else{
                currentTargetHIndex--;
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(currentTargetHIndex > targetsHorizontal.Length)
            {
                currentTargetHIndex = 0;
            }
            else{
                currentTargetHIndex++;
            }
        }

        Vector3 targetCamPos = targetsHorizontal[currentTargetHIndex].position;
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);
    }
}
