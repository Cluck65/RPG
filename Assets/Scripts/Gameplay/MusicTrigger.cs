using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour, IPlayerTriggerable
{

    public AudioSource playSound;

    public bool TriggerRepeatedly => false;

    public void OnPlayerTriggered(PlayerController player)
    {
        Debug.Log("Triggered PLAYER collision for " + playSound.name);
        if (!playSound.isPlaying)
            MusicController.PlayMusic(playSound);
    }

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
