using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Object = UnityEngine.Object;

# region Enum
/// <summary>
/// An enumeration that holds every sound effect in the game.
/// </summary>
public enum SoundFile
{
    BadAction,
    ButtonClick,
    DidConnect,
    LevelUp,
    LowHealthAlert,
    NodeDied1,
    NodeDied2,
    NodeDied3,
    NodeTakingDamage,
    ResourcesMoved,
    UsedPowerUp,
    VirusDied,
    something
}
# endregion

public class SoundManager : SingletonBehavior<SoundManager>
{
    float soundPitchMin = .75f;
    float soundPitchMax = 1.0f;

    /// <summary>
    /// A collection of all of the sound effects in the game.
    /// </summary>
    private Dictionary<SoundFile, AudioClip> SoundEffects
    { get; set; }

    /// <summary>
    /// Source for the sound effects
    /// </summary>
    private static AudioSource SoundEffectSource
    { get; set; }

    /// <summary>
    /// Source for the BGM
    /// </summary>
    private static AudioSource BGMSource
    { get; set; }

    protected override void Init()
    {
        SoundEffects = new Dictionary<SoundFile, AudioClip>();

        //Create a temporary dictionary that loads all of the Audio files from a specific location.
        //Key = name of file, Value = file itself.
        Dictionary<string, AudioClip> clips = Resources.LoadAll<AudioClip>(Constants.AUDIO_FILE_LOCATION).ToDictionary(t => t.name);

        //Iterates through the loaded sound files and adds them to the Enum to AudioClip dictionary.
        foreach (KeyValuePair<string, AudioClip> c in clips)
        {
            SoundEffects.Add((SoundFile)Enum.Parse(typeof(SoundFile), c.Key, true), c.Value);
        }

        //Creates a single sound effect source. Can play every sound in the game through this unless you want to have different effects
        //such as different pitch/volume for different sources.
        if (SoundEffectSource == null)
        {
            Debug.Log("Creating SoundEffectSource");
            SoundEffectSource = new GameObject("SoundEffectSource", typeof(AudioSource)).GetComponent<AudioSource>();
            DontDestroyOnLoad(SoundEffectSource.gameObject);
        }

        //Creates a single background music source.
        if (BGMSource == null)
        {
            Debug.Log("Creating BGMSource");
            BGMSource = new GameObject("BGMSource", typeof(AudioSource)).GetComponent<AudioSource>();
            BGMSource.volume = .5f;
            BGMSource.loop = true;
            DontDestroyOnLoad(BGMSource.gameObject);
        }

        //ChangeBGM(Resources.Load<AudioClip>("Sound/Music/DancingMidgets"));
    }

    /// <summary>
    /// Updates the audio manager
    /// </summary>
    public void Update()
    {
        //Slows the pitch of the audio down whenenver the player activates the slow time feature.
        SoundEffectSource.pitch = Mathf.Lerp(soundPitchMin, soundPitchMax, (Time.timeScale - .5f) * 2);
        //BGMSource.pitch = Mathf.Lerp(soundPitchMin, soundPitchMax, (Time.timeScale - .5f) * 2);
    }

    /// <summary>
    /// Plays a single sound effect
    /// </summary>
    /// <param name="sound">The sound effect we want to play</param>
    public void DoPlayOneShot(SoundFile sound, Vector3? location = null, float volumeScale = 1)
    {
        if (location == null)
            location = Vector3.zero;
        //SoundEffectSource.PlayOneShot(SoundEffects[sound], volumeScale * Constants.AUDIO_SOUND_EFFECT_VOLUME_MULTIPLIER);
        AudioSource.PlayClipAtPoint(SoundEffects[sound], (Vector3)location, volumeScale * Constants.AUDIO_SOUND_EFFECT_VOLUME_MULTIPLIER);
    }

    /// <summary>
    /// Changes the BGM to something else
    /// </summary>
    /// <param name="sound">The bgm sound we want to play</param>
    public void ChangeBGM(SoundFile sound)
    {
        BGMSource.clip = SoundEffects[sound];
        BGMSource.Play();
    }
}