using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    [Header("---------------Audio Sources---------------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;
    [SerializeField] public AudioMixer audioMixer;

    [Header("---------------Audio Clip---------------")]
    public AudioClip menumusic;
    public AudioClip gamemusic;
    public AudioClip backgroundnoise;
    public AudioClip death;
    public AudioClip walk;
    public AudioClip jump;
    public AudioClip damageTaken;
    public AudioClip landing;
    public AudioClip shooting;
    public AudioClip screenWarp;
    public AudioClip itemCollected;
    public AudioClip buttons;
    public AudioClip frogDamaged;
    public AudioClip pinkDamaged;
    public AudioClip voodooDamaged;
    public AudioClip spotted;
    public AudioClip frogShooting;
    public AudioClip pinkShooting;
    private string currentScene = "";


    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName != currentScene)
        {
            currentScene = sceneName;
            if (sceneName == "MainMenu")
            {
                PlayMusic(sceneName,menumusic);

            }else if (sceneName == "MainScene")
            {
                PlayMusic(sceneName,gamemusic);
            }
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute ;
    }

    public void LowPassMusic(bool isPaused)
    {
        if(isPaused)
        {
            audioMixer.SetFloat("MusicLowPass",800f);
        }else{
            audioMixer.SetFloat("MusicLowPass",5000f);
        }
    }

    public void PlayMusic(string sceneName,AudioClip clip )
    {
        if (musicSource != null)
        {
            if (sceneName == "MainMenu")
            {
                Debug.Log("PlayMusic() appelé");
                musicSource.clip = clip;
            }else if (sceneName == "MainScene")
            {
                musicSource.clip = clip;
            }               
                musicSource.loop = true; // Set the music to loop
                
                musicSource.Play();
        }
        else
        {
            Debug.LogWarning("MusicSource ou Backgroundmusic n'est pas assigné !");
        }
    }

    public void PlaySfx(AudioClip clip)
    {
       SFXSource.PlayOneShot(clip);
    }
}
