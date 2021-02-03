using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchClick : MonoBehaviour
{
    public GameManager gManager;

    public int coinBonus;

    public Transform rayT;

    private void Start() {
        coinBonus = gManager.coinBonus;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100f)){
            if(hit.transform != null){ //if we hit something
                if(Input.GetKeyDown(KeyCode.Mouse0)){

                    rayT = hit.transform;
                    CheckTag();
                }
            }
        }
    }

    public void CheckTag()
    {
        try
        {
            //if(rayT.tag == "")
            if(rayT.gameObject.GetComponent<Switch>().switchedOn)
            {
                //Debug.Log("NOW OFF");
                FlickOff(); 
            }
            else
            {
                //Debug.Log("NOW ON");
                FlickOn(); 
            }
        }
        catch(System.Exception)
        {
            Debug.LogError("error");
        }
    }

    public void FlickOff()
    {
        gManager.GetComponent<GameManager>().currentFlicked--;
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            50);
        rayT.gameObject.GetComponent<AudioSource>().Play();
        rayT.gameObject.GetComponent<Switch>().switchedOn = false;
        gManager.coins-=10;
    }

    public void FlickOn(){
        gManager.GetComponent<GameManager>().currentFlicked++;
        rayT.gameObject.transform.eulerAngles = new Vector3(rayT.gameObject.transform.eulerAngles.x,
                                                            rayT.gameObject.transform.eulerAngles.y,
                                                            -50);
        rayT.gameObject.GetComponent<AudioSource>().Play();
        rayT.gameObject.GetComponent<Switch>().switchedOn = true;
        gManager.coins+=10;
    }
}
