using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------------Audio Sources---------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SfxSource;

    [Header("---------------Audio Clip---------------")]
    public AudioClip backgroundmusic;
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

    private void Start()
    {
        PlayMusic();
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute ;
    }
    public void PlayMusic()
    {
        if (musicSource != null && backgroundmusic != null)
        {
            Debug.Log("PlayMusic() appel�");
            musicSource.clip = backgroundmusic;
         
            musicSource.loop = true; // Set the music to loop
            
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("MusicSource ou Backgroundmusic n'est pas assign� !");
        }
    }

    public void PlaySfx(AudioClip clip)
    {
       SfxSource.PlayOneShot(clip);
    }
}
