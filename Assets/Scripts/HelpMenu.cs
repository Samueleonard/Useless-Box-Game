using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
    /*
    This really shouldnt be needed but the toggle wasnt working 
    and i couldnt figure out why so instead
    we can just hide the panels when the user presses a button
    */
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
