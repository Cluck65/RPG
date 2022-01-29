using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{

    public AudioSource playSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Debug.Log("Triggered collision for " + playSound.name);
        if (!playSound.isPlaying)
            MusicController.PlayMusic(playSound);
        //playSound.Play();
        //MusicController.playSound(clip);
    }
}
