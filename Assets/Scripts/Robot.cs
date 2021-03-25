using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
handles the moveable robot
including tracking targets, moving and finding new targets
variable are accessed from economy also, to apply boosts
*/
public class Robot : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> switches;
    public GameObject target = null;
    Vector3 targetPos = new Vector3(0,0,0);
    
    float distToTarget;
    float closestDist = Mathf.Infinity;

    GameManager gameManager;
   
    [HideInInspector]
    public float delay; //the robot delay between finding a target and moving to it - can be changed from elsewhere

    private void Start() {
        delay = 8-(SceneManager.GetActiveScene().buildIndex); //lessen delay as levels progress

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        foreach (Transform child in GameObject.Find("Switches").transform)
            if(child.gameObject.name.Contains("Switch"))
                switches.Add(child.gameObject); //improve modularity by dynamically finding children, rather than predefining
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            //Debug.Log("no target. finding.");
            target = GetClosestSwitch();
        }
        else
        {
            //Debug.Log("target found. flicking.");
            StartCoroutine(ChangeTarget());
        }

    }

    //return the closest switch
    private GameObject GetClosestSwitch()
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        for (int i = 0; i < switches.Count; i++)
        {
            if(switches[i].GetComponent<Switch>().switchedOn)
            {
                Vector3 directionToTarget = switches[i].transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if(dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = switches[i];
                    //Debug.Log("new target - " + bestTarget.name);
                }
            }
        }
        //targetPos = bestTarget.transform.position;
        return bestTarget;
    }

    bool canFlick;

    //move, flick target and find a new target
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
            yield return new WaitForSeconds(delay);
            canFlick = true;
            if(canFlick)
            {
                Move();
                Flick();
                target = null;
            }
            canFlick = false;
            yield return null; //breakout of function
        }
    }

    void Flick()
    {
        gameManager.GetComponent<GameManager>().coins-=10;
        gameManager.GetComponent<GameManager>().currentFlicked--;
        Camera.main.GetComponent<SwitchClick>().CheckTag(target.tag, true); //re use flick function that the player uses
        target.GetComponent<Switch>().switchedOn = false;
    }

    //move to the node
    void Move()
    {
        //Debug.Log("moving to target");
        GetComponent<PathManager>().NavigateTo(targetPos);
        transform.position = GetClosestNodePos().position;
        //Debug.Log("moved to target");
    }

    //return the transform component (position, rotation, scale) of the closest node
    //so that we can move to it
    Transform GetClosestNodePos()
    {
        //add all nodes to the list
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Waypoint");
        Transform tMin = null;
        float minDist = Mathf.Infinity; //initially set closest to infinity
        Vector3 currentPos = transform.position;
        //loop through each node and find the closest
        foreach (GameObject t in nodes)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.transform;
                minDist = dist;
            }
        }
        return tMin; //return closest transform
    }

}

