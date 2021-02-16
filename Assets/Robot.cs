using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject[] switches;
    public GameObject target = null;
    
    float distToTarget;
    float closestDist = Mathf.Infinity;

    public GameObject[] movementNodes; //movement nodes for dijkstra

    public GameManager gameManager;

    public int delay; //

    private void Start() {
        delay = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Debug.Log("no target. finding.");
            target = GetClosestEnemy();
        }
        else
        {
            Debug.Log("target found. flicking.");
            StartCoroutine(ChangeTarget());
        }

    }

    private GameObject GetClosestEnemy()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < switches.Length; i++)
        {
            if(switches[i].GetComponent<Switch>().switchedOn)
            {
                Vector3 directionToTarget = switches[i].transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = switches[i];
                    Debug.Log("new target - " + bestTarget.name);
                }
            }
        }
     
        return bestTarget;
    }

    IEnumerator ChangeTarget()
    {
        /*
        wait DELAY seconds
        move to switch
        visibly flip switch
        change flip state
        reduce coins
        reduce score
        */
        yield return new WaitForSeconds(1);
        Flick();
        target = null;
        yield return null;
    }

    void Flick()
    {
        if(target.GetComponent<Switch>().switchedOn) //double check if switch is still on, could be switched off by user
        {
            gameManager.GetComponent<GameManager>().coins-=10;
            gameManager.GetComponent<GameManager>().currentFlicked--;
            Camera.main.GetComponent<SwitchClick>().CheckTag(target.tag); //re use flick function that the player uses
            target.GetComponent<Switch>().switchedOn = false;
        }
    }
}

