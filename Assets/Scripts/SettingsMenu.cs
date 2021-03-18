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
        qualityDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        List<string> fpsOptions = new List<string> {"20", "30", "100"};
        List<string> qualityOptions = new List<string>();

        resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;

        for( int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityOptions.Add(QualitySettings.names[i]);
        }

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + 
                    resolutions[i].height;
            resOptions.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        
        resolutionDropdown.AddOptions(resOptions);
        fpsLimitDropdown.AddOptions(fpsOptions);
        qualityDropdown.AddOptions(qualityOptions);

        resolutionDropdown.RefreshShownValue();
        fpsLimitDropdown.RefreshShownValue();
        qualityDropdown.RefreshShownValue();

        LoadSettings();
    
    }

    public void SetFPS(){
        Application.targetFrameRate = fpsLimitDropdown.value;
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
        //effectsVolumeSlider.value = volume;
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
                effectsVolumeSlider.value); 
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
