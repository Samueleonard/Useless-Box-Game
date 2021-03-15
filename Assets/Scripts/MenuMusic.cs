using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic mmMusic;

    private void Awake() { //prevent double music from playing if more audio sources begin
        if(mmMusic != null && mmMusic != this){
            Destroy(this.gameObject);
            return;
        }

        mmMusic = this; 
        DontDestroyOnLoad(this); //play the music through all scenes/menus
    }
}
