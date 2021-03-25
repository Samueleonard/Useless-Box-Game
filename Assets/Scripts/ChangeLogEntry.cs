using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
a single entry to the changelog list in the main menu
can be instanstiated to create new entries, with the field entries
entered through a ui entry box, and then saved in the changelog list.
*/
public class ChangeLogEntry : MonoBehaviour
{
    public string version;
    public string changes;
    public GameObject changePanel;
    public TMP_Text versionText, changeText, buttonVersionText;

    private void Start() {
        this.gameObject.name = "ChangeLog - v" + version;  
        buttonVersionText.text = "v" + version;
    }

    public void Show() {
        versionText.text = "Changes - " + version;
        if(!changePanel.activeSelf) //only activate if disabled, prevents toggling when clicking on other buttons
            changePanel.SetActive(true);
        changeText.text = changes;
    }
}
