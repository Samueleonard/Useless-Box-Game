using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SaveSystem : MonoBehaviour
{
    int saveAmount;
    public static int key = 150; //the key to be used for encryption

    private void Start() {
        saveAmount = GetSaveAmount();
        /* encryption test - successful
        ProgressData test = new ProgressData();
        test.coins = 0; //set test data
        test.level = 0;
        test.saveDate = "abc";
        Debug.Log(EncryptDecrypt(test.coins.ToString() + "," + test.level + "," + test.saveDate)); //encrypt test date
        Debug.Log(encrypted);
        Debug.Log(EncryptDecrypt(encrypted)); //run the same encryption algorithm and get the same result back
        */
    }

    public void SaveGame()
    {
        //ProgressData saveData = new ProgressData();
        //saveData.coins = gm.GetComponent<GameManager>().coins;
        //saveData.level = GetComponent<UnlockManager>().levelPassed;
        //saveData.saveDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        saveAmount++;
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveAmount + ".data", FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        //converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public void LoadGame(int saveNumber) //load encrypted file, decrypt it and convert to progress data
    {
        /*
        Debug.Log("loading save " + saveNumber);
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveNumber + ".data", FileMode.Open);

        BinaryFormatter converter = new BinaryFormatter();
        ProgressData saveData = converter.Deserialize(dataStream) as ProgressData;

        dataStream.Close();

        ProgressData save = new ProgressData();

        save.coins = saveData.coins;
        save.level = saveData.level;
        save.saveDate = saveData.saveDate;
        */
        GetComponent<ListCreator>().text.text = "Save Loaded Successfully!"; 
    }

    public string EncryptDecrypt(string data)
    { 
        StringBuilder inS = new StringBuilder(data);
        StringBuilder outS = new StringBuilder(data.Length);
        for (int i = 0; i < data.Length; i++)
        {
            outS.Append((char)(inS[i] ^ key)); //simple XOR encrypt using predefined key
        }
        return outS.ToString();
    }   

    public int GetSaveAmount()
    {
        int saveAmount = 0;
        //file names have for ex save1.data
        try  
        {    
            string[] files = Directory.GetFiles(Application.persistentDataPath);
            //For each string in the save directory directory   
            for (int i = 0; i < files.Length; i++)  
            {   
                //split the string into name and format and then if the format is .data
                if(Path.GetFileName(files[i].Split('.')[1]) == "data")
                {
                    saveAmount++;  
                }
            }
        }  
        //Catch any  exceptions and log the error message
        catch (System.Exception e)  
        {
            //Debug.Log("ERROR - " + e.Message);  
        }   
        return saveAmount;
    }
}

