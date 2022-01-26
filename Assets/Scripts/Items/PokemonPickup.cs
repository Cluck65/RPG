using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonPickup : MonoBehaviour, Interactable, ISavable
{
    [SerializeField] Pokemon pokemon;

    public bool Used { get; set; } = false;



    public IEnumerator Interact(Transform initiator)
    {
        if (!Used)
        {
            pokemon.init();
            initiator.GetComponent<PokemonParty>().AddPokemon(pokemon);

            Used = true;
            
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            string playerName = initiator.GetComponent<PlayerController>().Name;

            yield return DialogManager.Instance.ShowDialogText($"{playerName} received {pokemon.Base.Name}!");
        }

    }

    public object CaptureState()
    {
        return Used;
    }

    public void RestoreState(object state)
    {
        Used = (bool)state;

        if (Used)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
