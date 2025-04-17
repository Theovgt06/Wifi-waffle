using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSoundParametres : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject SFXButton;

    [Header("On/Off Textures")]
    [SerializeField] private Sprite SFXButtonOn;
    [SerializeField]private Sprite SFXButtonOff;
    [SerializeField]private Sprite soundButtonOn;
    [SerializeField]private Sprite soundButtonOff;


    private bool isSfxOn;
    private bool isMusicOn;


    void Start()
    {
        isSfxOn= true;
        isMusicOn = true;
    }
    public void SetSFXVolume(float level)
    { 
        audioMixer.SetFloat("SFXVolume",Mathf.Log10(level)*20);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(level)*20);
    }
    
    
    public void ToggleSFX(){
        if(isSfxOn){
            SFXButton.GetComponent<Image>().sprite = SFXButtonOff;
            audioManager.PlaySfx(audioManager.buttons);
            audioManager.SFXSource.mute = true;
            isSfxOn = false;
        }else if(isSfxOn == false)
        {
            SFXButton.GetComponent<Image>().sprite = SFXButtonOn;
            audioManager.PlaySfx(audioManager.buttons);
            audioManager.SFXSource.mute = false;
            isSfxOn = true;
        }
    }

    public void ToggleMusic(){
        if(isMusicOn){
            soundButton.GetComponent<Image>().sprite = soundButtonOff;
            audioManager.PlaySfx(audioManager.buttons);
            audioManager.musicSource.mute = true;
            isMusicOn = false;
        }else if(isMusicOn == false)
        {
            soundButton.GetComponent<Image>().sprite = soundButtonOn;
            audioManager.PlaySfx(audioManager.buttons);
            audioManager.musicSource.mute = false;
            isMusicOn = true;
        }
    }
}
