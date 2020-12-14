using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class robot : MonoBehaviour
{
    public GameObject switches;
    private gameObject target;
    public GameManager gManager;

    void FindTarget(){
        foreach (GameObject swt in switches)
        {
            if(!(swt.GetComponent<Switch>().switchedOn)){
                target = swt;
                StartCoroutine(Flip());
            }
        }
    }

    void MoveToTarget(){

    }

    IEnumerator Flip(){
        yield WaitForSeconds(5);
        target.GetComponent<Switch>().switchedOn = false;
        gManager.GetComponent<GameManager>.currentFlicked--;
        //rotate 
    }

    //find target
        //loop through all switches
        //find switch that is off
        //wait x seconds, flip off
    //move to target
    //flip target off
}
