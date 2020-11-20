using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public GameSettings gameSettings;

    public Dropdown resolutionDropdown;
    public Resolution[] resolutions;

    private void OnEnable() {
        gameSettings= new GameSettings();  

        resolutionDropdown.onValueChanged.AddListener(delegate {OnResChange();});
        
        resolutions = Screen.resolutions; 
        foreach(Resolution resolution in resolutions){
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }
    }

    public void OnResChange(){
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
    }

    public void SaveSettings(){

    }

    public void LoadSettings(){

    }
}
