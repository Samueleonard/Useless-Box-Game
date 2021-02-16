using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    public GameManager gManager;

    public int coinBonus;

    public Transform rayT;

    private void Start() 
    {
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
                    CheckTag(rayT.tag);
                }
            }
        }   
    }


    public void CheckTag(string tag)
    {
        if(tag.Contains("Switch"))
        {
            if(!rayT.gameObject.GetComponent<Switch>().switchedOn) //if switched off, switch on
            {
                if(tag == "RightSwitch")
                    FlickForward();
                else
                    FlickBackwards();
                rayT.gameObject.GetComponent<Switch>().switchedOn = true;
                gManager.GetComponent<GameManager>().currentFlicked++;
            }
            else  //switched on, switch off
            {    
                if(tag == "RightSwitch")
                    FlickBackwards();
                else
                    FlickForward();
                rayT.gameObject.GetComponent<Switch>().switchedOn = false;
                gManager.GetComponent<GameManager>().currentFlicked--;
            }    
        }      
    }

    public void FlickForward()
    {
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            25);
        rayT.gameObject.GetComponent<AudioSource>().Play();

        gManager.coins-=10;
    }

    public void FlickBackwards()
    {
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            -25);
        rayT.gameObject.GetComponent<AudioSource>().Play();
        rayT.gameObject.GetComponent<Switch>().switchedOn = true;
        gManager.coins+=10;
    }
}


