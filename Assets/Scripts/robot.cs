using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public GameManager gManager;
    public GameObject target;

    private void Update()
    {
        FindTarget();
    }
    void FindTarget() //finds the closest target, then uses dijkstras to find the shortest path to there, using the movement nodes
    { 
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        /*
        optimized by comparing distance squared instead of straight distance. 
        avoids the expensive square root operation that happens 
        under the hood by doing "Vector3.Distance".
        as seen in https://forum.unity.com/threads/trying-to-find-the-minimum-vector3-distance-from-an-array.481265/
         */
        foreach (GameObject potentialTarget in GetComponent<ShortestPath>().switches)
        {
            Vector3 directionToTarget = potentialTarget.gameObject.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                target = potentialTarget.gameObject;
            }
        }

    }

    IEnumerator Flip()
    {
        yield return new WaitForSeconds(5);
        transform.position = target.transform.position;
        target.GetComponent<Switch>().switchedOn = false;
        gManager.GetComponent<GameManager>().currentFlicked--;
        //rotate 
    }

    //find target
        //loop through all switches
        //find switch that is off
        //wait x seconds, flip off
    //move to target
    //flip target off
}
