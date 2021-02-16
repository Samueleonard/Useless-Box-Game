using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    [HideInInspector]
    public GameManager gManager;

    public int delay;

    private void Update()
    {
        FindTarget();
    }
    void FindTarget() //finds the closest target, then uses dijkstras to find the shortest path to there 
                      //using the movement nodes 
                      //TODO: movement nodes
    { 
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        /*
        optimized by comparing distance squared instead of straight distance. 
        avoids long square root operation that happens  by doing "Vector3.Distance".
        https://forum.unity.com/threads/trying-to-find-the-minimum-vector3-distance-from-an-array.481265/
         */
        /*
        foreach (GameObject potentialTarget in GetComponent<ShortestPath>().switches)
        {
            if(potentialTarget.gameObject.GetComponent<Switch>().switchedOn) //if switched off dont bother doing anything off
            {
                Debug.Log(potentialTarget.gameObject.name + "is on");
                Vector2 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
                //FIXME robot moves to offset, and high in the air
                float dSqrToTarget = (potentialTarget.gameObject.transform.position - currentPosition).sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;     
                    StartCoroutine(Move(potentialTarget.gameObject.transform.position, 5f));
                }
            }
        }
        */
    }

    IEnumerator Move(Vector2 targetPosition, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            yield return new WaitForSeconds(1f);
            transform.position = Vector2.Lerp(transform.position, targetPosition, time / duration);
            time = time + 1 +  Time.deltaTime; //move same rate for all frame rates
        }
        //Camera.main.GetComponent<SwitchClick>().FlickOff();
        Debug.Log("Switched Off");
    }
    
    //find target
        //loop through all switches
        //find switch that is off
        //wait x seconds, flip off
    //move to target
    //flip target off
}
