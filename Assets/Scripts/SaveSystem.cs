using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class SaveSystem
{
    public static void SaveData(GameManager gm, UnlockManager um){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        //ProgressData data = EncryptDecrypt(new ProgressData(gm,um);
        
        //formatter.Serialize(stream, data);
        stream.Close();
    }

    public static ProgressData LoadData(){
        string path = Application.persistentDataPath + "/player.sav";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            //ProgressData data =   EncryptDecrypt(formatter.Deserialize(stream));
           
            stream.Close();

            //return data;
            return null;
        }
        else{
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    public static int key = 129;

    public static string EncryptDecrypt(string textToEncrypt)
    {            
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
        char c;
        for (int i = 0; i < textToEncrypt.Length; i++)
        {
            c = inSb[i];
            c = (char)(c ^ key);
            outSb.Append(c);
        }
        return outSb.ToString();
    }   
    

}

