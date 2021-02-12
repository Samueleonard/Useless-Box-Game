using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SaveSystem : MonoBehaviour
{
    int saveAmount;
    public static int key = 150; //the key to be used for encryption

    private void Start() {
        saveAmount = PlayerPrefs.GetInt("SaveAmount", 0); //HOW MANY SAVES ARE CURRENTLY SAVED   
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

    public GameManager gm;

    public void SaveGame()
    {
        ProgressData saveData = new ProgressData();
        saveData.coins = gm.GetComponent<GameManager>().coins;
        //saveData.level = GetComponent<UnlockManager>().levelPassed;
        //Debug.Log(saveData.level);
        saveData.saveDate = System.DateTime.Now.ToString("dd/MM/yyyy");
        saveAmount++;
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveAmount + ".data", FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveData);

        dataStream.Close();
    }

    public ProgressData LoadGame(int saveNumber) //load encrypted file, decrypt it and convert to progress data
    {
        Debug.Log("loading save " + saveNumber);
        FileStream dataStream = new FileStream(Application.persistentDataPath + "/save" + saveNumber + ".data", FileMode.Open);

        BinaryFormatter converter = new BinaryFormatter();
        ProgressData saveData = converter.Deserialize(dataStream) as ProgressData;

        dataStream.Close();

        ProgressData save = new ProgressData();
        save.coins = saveData.coins;
        save.level = saveData.level;
        save.saveDate = saveData.saveDate;
        
        return save;
    }

    public static string EncryptDecrypt(string data)
    {   
        StringBuilder inSb = new StringBuilder(data);
        StringBuilder outSb = new StringBuilder(data.Length);
        for (int i = 0; i < data.Length; i++)
        {
            outSb.Append((char)(inSb[i] ^ key));
        }
        return outSb.ToString();
    }   
}

