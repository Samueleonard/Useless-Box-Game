using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System.IO;  
using UnityEngine.UI;
using TMPro;

/*
dynamically creates lists for the menus
(changelogs/savegames)
*/

public class ListCreator : MonoBehaviour
{   
    
    private string directoryPath;   //the directory path  
    
    private List<string> fileNames;  //A List of strings with file names and extensions  

    private string fullFilePath; //A string that stores the full file path 

    private Vector2 scrollPosition = Vector2.zero;   // initial position of the scroll view    
  
    public GameObject savePrefab;
    public RectTransform content;
    public TMP_Text text;

    int saves = 0;
  
    public void LoadSaveList ()   
    {  
        try  
        {  
            //Get the path of all files inside the directory and save them on a List  
            fileNames = new List<string>( Directory.GetFiles(Application.persistentDataPath) );  
            //For each string in the fileNames List   
            for (int i = 0; i < fileNames.Count; i++)  
            {  
                string[] fileName = Path.GetFileName(fileNames[i]).Split('.'); //
                string saveName = fileName[0];
                string fileType = fileName[1];
                if(fileType == "data")
                {
                    saves++;
                    //instantiate button on list
                    GameObject buttonPrefab = Instantiate(savePrefab);
                    buttonPrefab.transform.SetParent(content, false);
                    buttonPrefab.name = saveName;  //remove the .save from the file name
                    buttonPrefab.transform.Find("SaveNumberText").GetComponent<TMP_Text>().text = saveName;
                    int saveInt = int.Parse(saveName.Split('e')[1]);
                    PlayerPrefs.SetInt("SaveAmount", saveInt);
                    buttonPrefab.GetComponent<Button>().onClick.AddListener(delegate { GetComponent<SaveSystem>().LoadGame(saveInt) ; });                    
                }     
            }
            if(saves == 0)
                text.text = "No Save Files found in directory. Have you saved any progress?";
        }  
        //Catch any of the following exceptions and store the error message 
        catch (System.Exception e) 
        {  
            text.text = "ERROR - " + e.Message;  
        }  
    }
}
