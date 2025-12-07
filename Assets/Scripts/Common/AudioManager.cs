using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip[] musicClips;
    
    [Header("Settings")]
    private bool isMusicOn;

    private int currentMusicIndex = 0; 
    
    private const string MUSIC_KEY = "MusicOn";

    private void Awake()
    {
        AudioManager[] allManagers = FindObjectsOfType<AudioManager>();
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
            if (musicSource != null && musicSource.clip != null && !musicSource.isPlaying && isMusicOn)
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
        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        if (musicSource != null)
        {
            musicSource.mute = !isMusicOn;
            Debug.Log($"[AudioManager] UpdateMusicState: isMusicOn={isMusicOn}, mute={musicSource.mute}, isPlaying={musicSource.isPlaying}, volume={musicSource.volume}");
        }
        else
        {
            Debug.LogError("LỖI: Chưa gán Music Source trong Inspector của AudioManager!");
        }
    }

    // Play nhạc nền từ clip truyền vào
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;
        
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlayMusicByIndex(int index)
    {
        if (musicClips == null || index < 0 || index >= musicClips.Length)
        {
            Debug.LogError($"Invalid music index: {index}");
            return;
        }
        
        currentMusicIndex = index; // Track bài đang phát
        PlayMusic(musicClips[index]);
    }
    
    public void PlayMusicByName(string clipName)
    {
        if (musicClips == null) return;
        
        foreach (AudioClip clip in musicClips)
        {
            if (clip != null && clip.name == clipName)
            {
                PlayMusic(clip);
                return;
            }
        }
        
        Debug.LogWarning($"Music clip '{clipName}' not found!");
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PrefManager.SetBool(MUSIC_KEY, isMusicOn);
        UpdateMusicState();
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            source.mute = !isMusicOn;
            Debug.Log($"[AudioManager] Force mute {source.gameObject.name}: mute={source.mute}");
        }
    }

    public bool IsMusicOn => isMusicOn;
    
    public string[] GetMusicClipNames()
    {
        if (musicClips == null || musicClips.Length == 0)
            return new string[] { "No Music Available" };
        
        string[] names = new string[musicClips.Length];
        for (int i = 0; i < musicClips.Length; i++)
        {
            names[i] = musicClips[i] != null ? musicClips[i].name : $"Empty Slot {i}";
        }
        return names;
    }
    
    public int GetCurrentMusicIndex()
    {
        return currentMusicIndex;
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
        {
            musicSource.volume = Mathf.Clamp01(volume);
            Debug.Log("[AudioManager] Music volume set to: " + musicSource.volume);
        }
    }

}
