using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // The position that that camera will be following.
    public Transform[] targetsHorizontal; 

    public int currentTargetHIndex = 0; //which target we are currently on horizontally
    // The speed with which the camera will be following.           
    public float smoothing = 2f;        

    public bool vertical; //are we currently in vertical view

    // Start is called before the first frame update
    void Start()
    {                  
        Application.targetFrameRate = PlayerPrefs.GetInt("FPSLimit");   
    }

    void FixedUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            print("left key, old - " + currentTargetHIndex);
            if (currentTargetHIndex == 0)
            {
                currentTargetHIndex = targetsHorizontal.Length-1;
            }
            else{
                currentTargetHIndex--;
            }
            print("left key, new - " + currentTargetHIndex);
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            print("left key, old - " + currentTargetHIndex);
            if (currentTargetHIndex == targetsHorizontal.Length - 1)
            {
                currentTargetHIndex = 0;
            }
            else {
                currentTargetHIndex++;
            }
            print("right key, new - " + currentTargetHIndex);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            //if we are horizontal, do nothing
            if(vertical){
                vertical = false;
                Vector3 targetCamPos = targetsHorizontal[0].position;
                transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);
            }                
        }

        if(!vertical){
            Vector3 targetCamPos = targetsHorizontal[currentTargetHIndex].position;
            transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetsHorizontal[currentTargetHIndex].rotation, 200 * Time.deltaTime);
        }
    }
}
