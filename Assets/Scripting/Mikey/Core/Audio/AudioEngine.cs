/*
   12.07.2026 - 22.04 Created by Omer Faruk Simsek (farthenginer)
*/
using System.Collections.Generic;
using UnityEngine;

public class AudioEngine : MonoBehaviour
{
    //Instance
    public static AudioEngine audioEngine;

    public AudioSource _source;

    public List<TCAS_AlertClip> _tcasClips;
    public List<GPWS_AlertClip> _gpwsClips;

    Dictionary<AudioPool.TCAS_Sounds, AudioClip> dictionaryTCAS;
    Dictionary<AudioPool.GPWS_Sounds, AudioClip> dictionaryGPWS;

    void Awake()
    {
        if (audioEngine == null)
        {
            audioEngine = this; //Instance
        }

        dictionaryTCAS = new Dictionary<AudioPool.TCAS_Sounds, AudioClip>();

        foreach (var clip in _tcasClips)
            dictionaryTCAS.Add(clip.sound, clip.clip);

        dictionaryGPWS = new Dictionary<AudioPool.GPWS_Sounds, AudioClip>();

        foreach (var clip in _gpwsClips)
            dictionaryGPWS.Add(clip.sound, clip.clip);
    }

    /// <summary>
    /// Plays the specified TCAS alert sound.
    /// </summary>
    /// <param name="sound">
    /// The TCAS alert sound to play (Enum).
    /// </param>
    public void PlayTCAS(AudioPool.TCAS_Sounds sound)
    {
        if (dictionaryTCAS.TryGetValue(sound, out AudioClip clip))
            _source.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays the specified GPWS alert sound.
    /// </summary>
    /// <param name="sound">
    /// The GPWS alert sound to play (Enum).
    /// </param>
    public void PlayGPWS(AudioPool.GPWS_Sounds sound)
    {
        if (dictionaryGPWS.TryGetValue(sound, out AudioClip clip))
            _source.PlayOneShot(clip);
    }
}

[System.Serializable]
public class TCAS_AlertClip
{
    public AudioPool.TCAS_Sounds sound;
    public AudioClip clip;
}
[System.Serializable]
public class GPWS_AlertClip
{
    public AudioPool.GPWS_Sounds sound;
    public AudioClip clip;
}

public class AudioPool
{
    //GPWS SFX List
    public enum GPWS_Sounds
    {
        lowGear,
        lowFlaps,
        terrain,
        bankAngle,
        sinkRate,
        minimums,
        overspeed,
        whoop,
        approachingMinimums,
        stall,
        retard,
        autoPilotDisconnected0,
        gpws2500,
        gpws1000,
        gpws500,
        gpws400,
        gpws300,
        gpws200,
        gpws100,
        gpws50,
        gpws40,
        gpws30,
        gpws20,
        gpws10,
        gpws5,

    }

    //TCAS SFX List
    public enum TCAS_Sounds
    {
        traffic,
        climb,
        climbNow,
        descend,
        descendNow,
        clearOfConflict,
        monitorVerticalSpeed
    }
}
