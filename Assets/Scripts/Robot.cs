using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameObject[] switches;
    public GameObject target = null;
    Vector3 targetPos = new Vector3(0,0,0);
    
    float distToTarget;
    float closestDist = Mathf.Infinity;

    public GameManager gameManager;

    public float delay; //the robot delay - can be changed from elsewhere

    private void Start() {
        delay = gameManager.delay;
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
        //targetPos = bestTarget.transform.position;
        return bestTarget;
    }

    IEnumerator ChangeTarget() //TODO: terrible name - should probably rename it
    {
        /*
        wait DELAY seconds - 
        move to switch
        visibly flip switch - 
        change flip state - 
        reduce coins - 
        reduce score -
        */
        if(target.GetComponent<Switch>().switchedOn) //double check if switch is still on, could be switched off by user
        {
            yield return new WaitForSeconds(delay/2);
            Move();
            yield return new WaitForSeconds(delay/2);
            //Flick();
            target = null;
            yield return null; //breakout of fucntion
        }
    }

    void Flick()
    {
        gameManager.GetComponent<GameManager>().coins-=10;
        gameManager.GetComponent<GameManager>().currentFlicked--;
        Camera.main.GetComponent<SwitchClick>().CheckTag(target.tag); //re use flick function that the player uses
        target.GetComponent<Switch>().switchedOn = false;
    }

    void Move()
    {
        Debug.Log("moving to target");
        //GetComponent<PathManager>().NavigateTo(targetPos);
        /*
        move smoothly to target (via movement nodes?)
        animate arm move
        */
        Debug.Log(target.transform.position);
        //this.transform.position = targetPos;
        Debug.Log("moved to target");
    }
}

