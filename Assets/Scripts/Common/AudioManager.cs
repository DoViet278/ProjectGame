using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Settings")]
    private bool isMusicOn;
    private bool isSoundOn;

    private const string MUSIC_KEY = "MusicOn";
    private const string SOUND_KEY = "SoundOn";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
            
            // Tự động phát nhạc nếu AudioSource đã có clip gán sẵn trong Inspector
            if (musicSource != null && musicSource.clip != null && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadSettings()
    {
        isMusicOn = PrefManager.GetBool(MUSIC_KEY, true);
        isSoundOn = PrefManager.GetBool(SOUND_KEY, true);

        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        if (musicSource != null)
        {
            musicSource.mute = !isMusicOn;
            Debug.Log($"UpdateMusicState: Mute = {!isMusicOn}");
        }
        else
        {
            Debug.LogError("LỖI: Chưa gán Music Source trong Inspector của AudioManager!");
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null) return;
        
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!isSoundOn || sfxSource == null) return;
        
        sfxSource.PlayOneShot(clip);
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PrefManager.SetBool(MUSIC_KEY, isMusicOn);
        Debug.Log($"ToggleMusic: New State = {isMusicOn}");
        UpdateMusicState();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PrefManager.SetBool(SOUND_KEY, isSoundOn);
    }

    public bool IsMusicOn => isMusicOn;
    public bool IsSoundOn => isSoundOn;
}
