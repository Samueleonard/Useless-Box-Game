using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuParent, amountSelectParent, settingsMenu;

#region Main Menu

    private void Start() {
        Debug.Log("Menu Init");
        mainMenuParent.SetActive(true);
        settingsMenu.SetActive(false);
        amountSelectParent.SetActive(false);    
    }

    public void OnPlayButton(){
        Debug.Log("Play pressed");
        mainMenuParent.SetActive(false);
        settingsMenu.SetActive(false);
        amountSelectParent.SetActive(true);
    }

    public void OnSettings(){
        Debug.Log("Settings Pressed");
        mainMenuParent.SetActive(false);
        settingsMenu.SetActive(true);
        amountSelectParent.SetActive(false);
    }

    public void OnQuit(){
        Debug.Log("Quitting");
        Application.Quit();
    }

    #endregion

#region Player Amount Select

    public void OnSinglePlayer(){
        Debug.Log("Loading SinglePlayer Scene");
        SceneManager.LoadScene(1);
    }

    public void OnMultiPlayer(){
        Debug.Log("Loading MultiPlayer Scene");
        SceneManager.LoadScene(1);
    }

    public void OnBack(){
        Debug.Log("Going Back To Main Menu");
        mainMenuParent.SetActive(true);
        amountSelectParent.SetActive(false);   
        settingsMenu.SetActive(false); 
    }

#endregion

public AudioMixer mixer;

public GameObject[] pages;
public int currentPage = 0;

#region SettingsMenu

    public void SetMainVolume(float volume){
        mixer.SetFloat("MainVol", Mathf.Log10(volume) * 20);
    }

    public void NextPage(){
        pages[currentPage].SetActive(false);
        if(currentPage == pages.Length-1){ //we are on last page, go to first page
            Debug.Log("currently on last page");
            pages[0].SetActive(true);
            currentPage = 0;
        }
        else{
            Debug.Log("currently on page " + (currentPage+1) + " moving forward to page " + (currentPage+2));
            pages[currentPage+1].SetActive(true);
            currentPage++;
        }
        Debug.Log("Going to next page");
    }

    public void PreviousPage(){
        pages[currentPage].SetActive(false);
        if(currentPage == 0){ //we are on last page, go to first page
            Debug.Log("currently on first page");
            currentPage = pages.Length-1;
            pages[pages.Length-1].SetActive(true);
        }
        else{
            Debug.Log("currently on page " + (currentPage-1) + " moving back to page " + (currentPage-2));
            pages[currentPage-1].SetActive(true);
            currentPage--;
        }
    }

#endregion

}
