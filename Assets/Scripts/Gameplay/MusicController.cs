using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    static AudioSource audioSrc;
    static AudioSource secondPrevAudioSrc;
    static AudioSource prevAudioSrc;


    private void Update()
    {
        if (audioSrc.name == TrainerBattle)
            if (audioSrc.time > )
    }
    public static MusicController Instance { get; private set; }


    public static void PlaySecondPreviousMusic()
    {
        if (audioSrc != null)
            audioSrc.Stop();
        audioSrc = secondPrevAudioSrc;
        audioSrc.Play();
    }

    public static void PlayPreviousMusic()
    {
        if (prevAudioSrc != null)
        audioSrc.Stop();
        audioSrc = prevAudioSrc;
        audioSrc.Play();
    }

    public static void PlayMusicBetweenPoints(AudioSource clip, float startTime, float loopPoint)
    {
            clip.time = startTime;
            PlayMusic(clip);
            clip.SetScheduledEndTime(AudioSettings.dspTime + (startTime - loopPoint));
    }

  
    public static void PlayMusic(AudioSource clip)
    {
        if (audioSrc != null)
        {
            if (prevAudioSrc != null)
            {
                secondPrevAudioSrc = prevAudioSrc;
            }
            audioSrc.Stop();
            prevAudioSrc = audioSrc;
        }
        audioSrc = clip;
        clip.Play();
        
    }

    public IEnumerator PlaySoundEffect(AudioSource effect)
    {
        PlayMusic(effect);
        yield return 0;
    }
}
