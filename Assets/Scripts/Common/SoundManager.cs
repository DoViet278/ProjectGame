using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("One Shot Clips")]
    public AudioClip click;
    public AudioClip jump;
    public AudioClip stoneRoll;
    public AudioClip run;
    public AudioClip saw;

    private bool isSoundOn;

    private List<AudioSource> oneShotSources = new List<AudioSource>();

    private Dictionary<string, AudioSource> loopSources = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        isSoundOn = PrefManager.GetBool(KeyManager.KeySoundEnable, true);
    }

    private AudioSource GetFreeOneShotSource()
    {
        foreach (var src in oneShotSources)
        {
            if (!src.isPlaying)
                return src;
        }

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        oneShotSources.Add(newSource);
        return newSource;
    }

    public void PlayOneShotSound(AudioClip clip)
    {
        if (!isSoundOn || clip == null) return;

        AudioSource src = GetFreeOneShotSource();
        src.PlayOneShot(clip);
    }

    public void PlaySound(string name)
    {
        if (!isSoundOn) return;

        switch (name)
        {
            case "click":
                PlayOneShotSound(click);
                break;

            case "jump":
                PlayOneShotSound(jump);
                break;

            case "stone":
                PlayLoop("stone",stoneRoll);
                break;

            case "run":
                PlayLoop("run", run);
                break;

            case "saw":
                PlayLoop("saw", saw);
                break;
        }
    }

    public void PlayLoop(string name, AudioClip clip)
    {
        if (!isSoundOn || clip == null) return;

        if (!loopSources.ContainsKey(name))
        {
            AudioSource newSource = gameObject.AddComponent<AudioSource>();
            newSource.loop = true;
            newSource.playOnAwake = false;
            loopSources.Add(name, newSource);
        }

        AudioSource src = loopSources[name];

        if (!src.isPlaying)
        {
            src.clip = clip;
            src.Play();
        }
    }

    public void StopLoop(string name)
    {
        if (loopSources.ContainsKey(name))
        {
            loopSources[name].Stop();
        }
    }
    public void StopAllLoops()
    {
        foreach (var src in loopSources.Values)
        {
            if (src.isPlaying)
                src.Stop();
        }
    }

    public void ToogleSound()
    {
        isSoundOn = !isSoundOn;
        PrefManager.SetBool(KeyManager.KeySoundEnable, isSoundOn);

        if (!isSoundOn)
            StopAllLoops();
    }

    public bool IsSoundOn => isSoundOn;
}
