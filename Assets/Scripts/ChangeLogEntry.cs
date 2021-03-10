using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
