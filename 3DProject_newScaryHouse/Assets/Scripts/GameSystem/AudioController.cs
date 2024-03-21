using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.CustomUITool;

/// <summary>
/// Handle audios control on UI GameOptionInterface.
/// </summary>
public class AudioController
{
    private GameObject m_RootUI;
    private Slider slider;
    private Toggle toggle;
    private AudioSource music;
    private Slider effectSlider;
    private Toggle effectToggle;
    List<AudioSource> effectAudios = new List<AudioSource>();

    public AudioController()
    {
        Initialize();
    }
    public void Initialize()
    {
        m_RootUI = UITool.FindUIGameObject("GameOptionInterface");
        slider = UITool.GetUIComponent<Slider>(m_RootUI, "SoundSlider");
        toggle = UITool.GetUIComponent<Toggle>(m_RootUI, "SoundToggle");

        effectSlider = UITool.GetUIComponent<Slider>(m_RootUI, "effectSlider");
        effectToggle = UITool.GetUIComponent<Toggle>(m_RootUI, "SoundEffectToggle");
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("effectAudio"))
        {
            effectAudios.Add(obj.GetComponent<AudioSource>());
            //Debug.Log(obj);
        }
        if (Unity.CustomTool.UnityTool.FindGameObject("Sound") != null)
        {
            music = Unity.CustomTool.UnityTool.FindGameObject("Sound").GetComponent<AudioSource>();
        }

        //music.volume = PlayerPrefs.GetFloat("Volume", slider.maxValue);
        music.volume = GameSettingParamStorage.MusicVolume;
        /*if (music.volume < 1)
        {
            music.volume = 1;
        }*/
        slider.value = GameSettingParamStorage.MusicVolume;
        slider.onValueChanged.AddListener(
                                            (slider) => Volume()
                                         );
        /*slider.onValueChanged.AddListener(
                                            (slider) => Con_sound()
                                         );*/
        toggle.onValueChanged.AddListener(
                                            (toggle) => ControllerAudio()
                                         );

        effectSlider.value = GameSettingParamStorage.EffectVolume;
        effectSlider.onValueChanged.AddListener(
                                                  (slider) => ChangeEffectVolume()
                                               );
        effectToggle.onValueChanged.AddListener(
                                                  (toggle) => EffectControllerAudio()
                                               );
        toggle.isOn = !GameSettingParamStorage.MusicStatus;
        effectToggle.isOn = !GameSettingParamStorage.EffectStatus;
    }
    public void ControllerAudio()
    {
        GameSettingParamStorage.MusicStatus = !toggle.isOn;
        if (!toggle.isOn)
        {
            music.gameObject.SetActive(GameSettingParamStorage.MusicStatus);
            Volume();
        }
        else
        {
            music.gameObject.SetActive(GameSettingParamStorage.MusicStatus);
        }
    }
    public void ChangeEffectVolume()
    {
        GameSettingParamStorage.EffectVolume = effectSlider.value;
        foreach (AudioSource obj in effectAudios)
        {
            obj.volume = GameSettingParamStorage.EffectVolume;
        }
    }
    public void EffectControllerAudio()
    {
        GameSettingParamStorage.EffectStatus = !effectToggle.isOn;
        if (!effectToggle.isOn)
        {
            foreach (AudioSource obj in effectAudios)
            {
                obj.gameObject.SetActive(GameSettingParamStorage.EffectStatus);
            }
        }
        else
        {
            foreach (AudioSource obj in effectAudios)
            {
                obj.gameObject.SetActive(GameSettingParamStorage.EffectStatus);
            }
        }
    } 
    public void Volume()
    {
        GameSettingParamStorage.MusicVolume = slider.value;
        music.volume = GameSettingParamStorage.MusicVolume;
    }
    
    public void Con_sound()
    {
        GameSettingParamStorage.MusicVolume = slider.value;
        music.volume = GameSettingParamStorage.MusicVolume;
        //PlayerPrefs.SetFloat("Volume", slider.value);
    }
}
