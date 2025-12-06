using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music Clips")]
    [SerializeField] private AudioClip[] musicClips; // Kéo nhiều nhạc nền vào đây
    
    [Header("Sound Effect Clips")]
    [SerializeField] private AudioClip[] sfxClips; // Kéo nhiều SFX vào đây

    [Header("Settings")]
    private bool isMusicOn;
    private bool isSoundOn;

    private int currentMusicIndex = 0; // Track bài đang phát
    
    private const string MUSIC_KEY = "MusicOn";
    private const string SOUND_KEY = "SoundOn";

    private void Awake()
    {
        // Kiểm tra có bao nhiêu AudioManager trong scene
        AudioManager[] allManagers = FindObjectsOfType<AudioManager>();
        Debug.Log($"[AudioManager] Có {allManagers.Length} AudioManager trong scene!");
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"[AudioManager] Instance được set: {gameObject.name}");
            LoadSettings();
            
            // Tự động phát nhạc nếu AudioSource đã có clip gán sẵn và music được bật
            if (musicSource != null && musicSource.clip != null && !musicSource.isPlaying && isMusicOn)
            {
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"[AudioManager] Đã có Instance rồi! Destroy {gameObject.name}");
            Destroy(gameObject);
        }
    }

    private void LoadSettings()
    {
        isMusicOn = PrefManager.GetBool(MUSIC_KEY, true);
        isSoundOn = PrefManager.GetBool(SOUND_KEY, true);

        UpdateMusicState();
        UpdateSoundState();
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

    private void UpdateSoundState()
    {
        if (sfxSource != null)
        {
            sfxSource.mute = !isSoundOn;
            Debug.Log($"[AudioManager] UpdateSoundState: isSoundOn={isSoundOn}, mute={sfxSource.mute}, isPlaying={sfxSource.isPlaying}, volume={sfxSource.volume}");
        }
        else
        {
            Debug.LogError("LỖI: Chưa gán SFX Source trong Inspector của AudioManager!");
        }
    }

    // Play nhạc nền từ clip truyền vào
    public void PlayMusic(AudioClip clip)
    {
        if (musicSource == null || clip == null) return;
        
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    // Play nhạc nền theo index trong mảng musicClips
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
    
    // Play nhạc nền theo tên
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

    // Play SFX từ clip truyền vào
    public void PlaySFX(AudioClip clip)
    {
        if (!isSoundOn || sfxSource == null || clip == null) return;
        
        sfxSource.PlayOneShot(clip);
    }
    
    // Play SFX theo index trong mảng sfxClips
    public void PlaySFXByIndex(int index)
    {
        if (sfxClips == null || index < 0 || index >= sfxClips.Length)
        {
            Debug.LogError($"Invalid SFX index: {index}");
            return;
        }
        
        PlaySFX(sfxClips[index]);
    }
    
    // Play SFX theo tên
    public void PlaySFXByName(string clipName)
    {
        if (sfxClips == null) return;
        
        foreach (AudioClip clip in sfxClips)
        {
            if (clip != null && clip.name == clipName)
            {
                PlaySFX(clip);
                return;
            }
        }
        
        Debug.LogWarning($"SFX clip '{clipName}' not found!");
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PrefManager.SetBool(MUSIC_KEY, isMusicOn);
        Debug.Log($"ToggleMusic: New State = {isMusicOn}");
        UpdateMusicState();
        
        // FORCE mute tất cả AudioSource trong scene
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        Debug.Log($"[AudioManager] Tìm thấy {allSources.Length} AudioSources trong scene");
        foreach (AudioSource source in allSources)
        {
            if (source != sfxSource) // Chỉ mute music sources, không mute SFX
            {
                source.mute = !isMusicOn;
                Debug.Log($"[AudioManager] Force mute {source.gameObject.name}: mute={source.mute}");
            }
        }
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PrefManager.SetBool(SOUND_KEY, isSoundOn);
        Debug.Log($"ToggleSound: New State = {isSoundOn}");
        UpdateSoundState();
        
        // Mute sfxSource if needed
        if (sfxSource != null)
        {
            sfxSource.mute = !isSoundOn;
            Debug.Log($"[AudioManager] Force mute sfxSource: mute={sfxSource.mute}");
        }
    }

    public bool IsMusicOn => isMusicOn;
    public bool IsSoundOn => isSoundOn;
    
    // Helper methods cho Dropdown UI
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
}
