using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{

    public AudioSource playSound;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log($"Triggered collision for {playSound.name} from {collision.name}");
            if (!playSound.isPlaying)
                MusicController.PlayMusic(playSound);
        }
        //playSound.Play();
        //MusicController.playSound(clip);
    }
}
