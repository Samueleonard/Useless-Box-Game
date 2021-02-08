using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuParent, amountSelectParent, settingsMenuParent, levelSelectParent,
           loadLevelParent;
    
    

#region Main Menu

    private void Start() {
        Debug.Log("Menu Init");
        mainMenuParent.SetActive(true);
        settingsMenuParent.SetActive(false);
        amountSelectParent.SetActive(false);  
        levelSelectParent.SetActive(false);  
        loadLevelParent.SetActive(false);
    }

    public void OnPlayButton(){
        Debug.Log("Play pressed");
        mainMenuParent.SetActive(false);
        amountSelectParent.SetActive(true);
    }

    public void OnLoadLevel(){
        Debug.Log("Load Level Pressed");
        mainMenuParent.SetActive(false);
        loadLevelParent.SetActive(true);
    }
    
    public void OnSettings(){
        Debug.Log("Settings Pressed");
        mainMenuParent.SetActive(false);
        settingsMenuParent.SetActive(true);
        pages[0].SetActive(true);
    }

    public void OnQuit(){
        Debug.Log("Quitting");
        Application.Quit();
    }

#endregion

#region Player Amount Select

    public void OnSinglePlayer(){
        Debug.Log("Loading SinglePlayer Scene");
        mainMenuParent.SetActive(false);
        amountSelectParent.SetActive(false);
        levelSelectParent.SetActive(true);
    }

    public void OnMultiPlayer(){
        Debug.Log("Loading Multiplayer Scene");
        SceneManager.LoadScene(1);
    }

    public void OnBack(){
        Debug.Log("Going Back To Main Menu");
        mainMenuParent.SetActive(true);
        amountSelectParent.SetActive(false);   
        settingsMenuParent.SetActive(false); 
        levelSelectParent.SetActive(false);
        loadLevelParent.SetActive(false);
    }

#endregion

#region Level Select

    public void LoadLevel(int number){
        SceneManager.LoadScene("Level " + number.ToString());
    }

    public void ResetPrefs(){
        PlayerPrefs.DeleteAll();
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
