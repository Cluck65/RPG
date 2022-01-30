using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    static AudioSource audioSrc;
    static AudioSource prevAudioSrc;

    static AudioSource trainerMusic;
    static AudioSource wildMusic;

    private void Awake()
    {
        trainerMusic = GetComponentInChildren<AudioSource>();
    }

    public static void PlayPreviousMusic()
    {
        if (prevAudioSrc != null)
        audioSrc.Stop();
        audioSrc = prevAudioSrc;
        audioSrc.Play();
    }

    public static void PlayTrainerMusic()
    {

        if (audioSrc != null)
            audioSrc.Stop();
        prevAudioSrc = audioSrc;
        trainerMusic.Play();
    }

    public static void PlayWildMusic()
    {
        if (audioSrc != null)
            audioSrc.Stop();
        prevAudioSrc = audioSrc;
        wildMusic.Play();
    }

    public static void PlayMusic(AudioSource clip)
    {
        if (audioSrc != null)
            audioSrc.Stop();
        prevAudioSrc = audioSrc;
        audioSrc = clip;
        clip.Play();
        
    }

    public IEnumerator PlaySoundEffect(AudioSource effect)
    {
        PlayMusic(effect);
        yield return 0;
    }
}
