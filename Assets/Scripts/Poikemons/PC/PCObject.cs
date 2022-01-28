using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PCObject : MonoBehaviour, Interactable
{
    GameController gameController;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    
    public IEnumerator Interact(Transform initiator)
    {
        yield return gameController.StartPC();
    }
}
