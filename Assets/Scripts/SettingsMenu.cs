using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    public AudioSource mmMusic;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public TMP_Dropdown textureDropdown;
    public TMP_Dropdown aaDropdown;
    public TMP_Dropdown fpsLimitDropdown;
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    float currentMainVolume, currentMusicVolume, currentEffectsVolume;
    Resolution[] resolutions;
    public Toggle vsyncToggle;
    public Toggle fullscreenToggle;


    // Start is called before the first frame update
    void Start()
    {
        resolutionDropdown.ClearOptions();
        fpsLimitDropdown.ClearOptions();

        List<string> options = new List<string>();
        List<string> fpsOptions = new List<string> {"20", "30", "100"};
        List<string> qualityOptions = new List<string> {"Very Low", "Low", "Medium", "High", "Very High"};

        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + 
                    resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(options);
        fpsLimitDropdown.AddOptions(fpsOptions);
        resolutionDropdown.RefreshShownValue();
        fpsLimitDropdown.RefreshShownValue();
        

        LoadSettings();
    
    }

    public void SetFPS(){
        //Application.targetFrameRate = fpsLimitDropdown.value;
    }

    public void ToggleVsync(){
        if(vsyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void SetVolume(float volume)
    {
        /*
        volume levels are hard to get right. at the upper end, 
        you want to make big changes to make it quieter/louder 
        but at the lower ends, you want fine control over volume, 
        so by using a logarithmic approach to volume control, 
        you can get fine control over the low end whilst making 
        the high end adjustable still
        */
	    audioMixer.SetFloat("MainVol", Mathf.Log10(volume) + 1);
        currentMainVolume = volume;
    }

    
    public void SetMusicVolume(float volume)
    {
        currentMusicVolume = Mathf.Log10(volume) + 1;
        mmMusic.volume = currentMusicVolume;
    }

    public void SetEffectsVolume(float volume)
    {
        //effectVolume.volume = volume;
        currentEffectsVolume = Mathf.Log10(volume) + 1;
    }

    public void SetFullscreen(bool isFullscreen)
    {
	    Screen.fullScreen = isFullscreen;
    }

    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, 
                resolution.height, Screen.fullScreen);
    }


    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }

    public void SetAntiAliasing(int aaIndex)
    {
        QualitySettings.antiAliasing = aaIndex;
        qualityDropdown.value = 6;
    }
    
    public void SetQuality(int qualityIndex)
    {
        if (qualityIndex != 6) // if the user is not using 
                                //any of the presets
            QualitySettings.SetQualityLevel(qualityIndex);
        switch (qualityIndex)
        {
            case 0: // quality level - very low
                textureDropdown.value = 3;
                aaDropdown.value = 0;
                break;
            case 1: // quality level - low
                textureDropdown.value = 2;
                aaDropdown.value = 0;
                break;
            case 2: // quality level - medium
                textureDropdown.value = 1;
                aaDropdown.value = 0;
                break;
            case 3: // quality level - high
                textureDropdown.value = 0;
                aaDropdown.value = 0;
                break;
            case 4: // quality level - very high
                textureDropdown.value = 0;
                aaDropdown.value = 1;
                break;
            case 5: // quality level - ultra
                textureDropdown.value = 0;
                aaDropdown.value = 2;
                break;
        }
            
        qualityDropdown.value = qualityIndex;
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference", 
                qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference", 
                resolutionDropdown.value);
        PlayerPrefs.SetInt("TextureQualityPreference", 
                textureDropdown.value);
        PlayerPrefs.SetInt("AntiAliasingPreference", 
                aaDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference", 
                Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("mainVolumePreference", 
                currentMainVolume); 
        PlayerPrefs.SetFloat("musicVolumePreference", 
                currentMusicVolume); 
        PlayerPrefs.SetFloat("effectsVolumePreference", 
                currentEffectsVolume); 
        PlayerPrefs.SetFloat("FPSLimit", 
                fpsLimitDropdown.value);
        PlayerPrefs.SetString("VSyncToggle",
                vsyncToggle.isOn.ToString());
    }
    
    //look for playerpref, it isnt there do default setting
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value = 
                        PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;

        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value = 
                        PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = 0;

        if (PlayerPrefs.HasKey("TextureQualityPreference"))
            textureDropdown.value = 
                        PlayerPrefs.GetInt("TextureQualityPreference");
        else
            textureDropdown.value = 0;

        if (PlayerPrefs.HasKey("AntiAliasingPreference"))
            aaDropdown.value = 
                        PlayerPrefs.GetInt("AntiAliasingPreference");
        else
            aaDropdown.value = 1;

        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen = 
                        Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;

        if (PlayerPrefs.HasKey("mainVolumePreference"))
            mainVolumeSlider.value = 
                        PlayerPrefs.GetFloat("mainVolumePreference");
        else
            mainVolumeSlider.value = 1;

        if (PlayerPrefs.HasKey("musicVolumePreference"))
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolumePreference");
        else
            musicVolumeSlider.value = 0.2f;

        if (PlayerPrefs.HasKey("effectsVolumePreference"))
            effectsVolumeSlider.value = PlayerPrefs.GetFloat("effectsVolumePreference");
        else
            effectsVolumeSlider.value = 1;

        if( PlayerPrefs.HasKey("FPSLimit"))
            fpsLimitDropdown.value = 
                        PlayerPrefs.GetInt("FPSLimit");
        else
            fpsLimitDropdown.value = 100;

        if(PlayerPrefs.HasKey("VSyncToggle"))
            vsyncToggle.isOn =
                        bool.Parse(PlayerPrefs.GetString("VSyncToggle"));
        else
            vsyncToggle.isOn = false;

    }
    
}
