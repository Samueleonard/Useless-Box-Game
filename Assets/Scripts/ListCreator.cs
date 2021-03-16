using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System.IO;  
using UnityEngine.UI;
using TMPro;

public class ListCreator : MonoBehaviour
{
    ///Variables for listing the directory contents    
    //A string that holds the directory path  
    private string directoryPath;   
    //A List of strings that holds the file names with their respective extensions  
    private List<string> fileNames;  
    //A string that stores the full file path  
    private string fullFilePath;  
  
    //The initial position of the scroll view  
    private Vector2 scrollPosition = Vector2.zero;  
    ////  
  
    public GameObject savePrefab;
    public RectTransform content;
    public TMP_Text text;

    int saves = 0;
  
    // Use this for initialization  
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
                    //instantiate 
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
            text.text = "ERROR - " + e.Message + " \nREPORT TO ME";  
        }  
    }
}
