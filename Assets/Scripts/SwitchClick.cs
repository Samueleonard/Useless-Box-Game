using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    public GameManager gManager;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100f));{
            string tag = hit.transform.gameObject.tag;
            if(hit.transform != null){
                if(Input.GetKeyDown(KeyCode.Mouse0)){
                    if(tag == "LeftSwitch" ||tag == "BackSwitch"){
                        //if switch off, rotate +60, else rotate -60
                        if(hit.transform.gameObject.GetComponent<Switch>().switchedOn){
                            gManager.GetComponent<GameManager>().currentFlicked--;
                            hit.transform.gameObject.transform.Rotate(0,0,60);
                        }
                        else{
                            gManager.GetComponent<GameManager>().currentFlicked++;

                            hit.transform.gameObject.transform.Rotate(0, 0, -60);
                        }
                        hit.transform.gameObject.GetComponent<Switch>().switchedOn = !hit.transform.gameObject.GetComponent<Switch>().switchedOn; //if on, turn off, vice versa
                    }
                    if(tag == "RightSwitch" || tag == "FrontSwitch"){
                        //if switch off, rotate -60, else rotate +60
                        if(hit.transform.gameObject.GetComponent<Switch>().switchedOn){
                            hit.transform.gameObject.transform.Rotate(0, 0, -60);
                            gManager.GetComponent<GameManager>().currentFlicked--;
                        }
                        else{
                            gManager.GetComponent<GameManager>().currentFlicked++;
                            hit.transform.gameObject.transform.Rotate(0, 0, 60);
                        }
                        hit.transform.gameObject.GetComponent<Switch>().switchedOn = !hit.transform.gameObject.GetComponent<Switch>().switchedOn; //if on, turn off, vice versa
                    }
                }
            }
        }
    }
}
