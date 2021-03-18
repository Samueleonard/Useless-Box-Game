using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    public GameManager gManager;

    public int coinBonus;

    public Transform rayT;

    private GameObject robot;

    private void Start() 
    {
        robot = GameObject.Find("ComputerRobot");
        coinBonus = gManager.coinBonus;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.transform != null) //if we hit something
            { 
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    rayT = hit.transform;
                    CheckTag(rayT.tag, false);
                }
            }
        }   
    }


    public void CheckTag(string tag, bool robotFlick) //robotflick - if robot flicks switch,
                                                      //do not take away coins, only remove coins
                                                      //on flick if user flicks it, prevent coin
                                                      //farming by repeatedly flicking switches
    {
        if(tag.Contains("Switch"))
        {
            if(!rayT.gameObject.GetComponent<Switch>().switchedOn) //if switched off, switch on
            {
                if(tag == "RightSwitch")
                    FlickForward(robotFlick);
                else
                    FlickBackwards(robotFlick);
                rayT.gameObject.GetComponent<Switch>().switchedOn = true;
                gManager.GetComponent<GameManager>().currentFlicked++;
            }
            else  //switched on, switch off
            {    
                if(tag == "RightSwitch")
                    FlickBackwards(robotFlick);
                else
                    FlickForward(robotFlick);
                rayT.gameObject.GetComponent<Switch>().switchedOn = false;
                gManager.GetComponent<GameManager>().currentFlicked--;
            }    
        }   
        if(robot.GetComponent<Robot>().target != null)
        {
            if(robot.GetComponent<Robot>().target.name == rayT.gameObject.name) //if we click the target switch, make the target null
            {
                robot.GetComponent<Robot>().target = null;
            }
        }
    }

    public void FlickForward(bool robotFlicked)
    {
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            25);
        rayT.gameObject.GetComponent<AudioSource>().Play();

        if (!robotFlicked)
            gManager.coins-=10;
    }

    public void FlickBackwards(bool robotFlicked)
    {
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            -25);
        rayT.gameObject.GetComponent<AudioSource>().Play();
        
        if(!robotFlicked)  
            gManager.coins+=10;
    }
}


