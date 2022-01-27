using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonChoiceGiverV2 : MonoBehaviour, ISavable, Interactable
{
    [SerializeField] Pokemon pokemonToGive;

    [SerializeField] GameObject pokemonQuestObject;

    [SerializeField] GameObject poliwagPlaceholder;
    [SerializeField] GameObject machopPlaceholder;
    [SerializeField] GameObject abraPlaceholder;

    [SerializeField] GameObject brotherState1;
    [SerializeField] GameObject brotherState2;

    public bool used { get; set; } 

    private void Awake()
    {
        if (used == true)
        {
            brotherState1.SetActive(false);
            brotherState2.SetActive(true);

        }
    }

    public IEnumerator Interact(Transform initiator)
    {
        if (used == false)
            yield return GiveChoicePokemon(initiator);
        else
            yield break;
    }


    public IEnumerator GiveChoicePokemon (Transform player)
    {
            if (player.GetComponent<PokemonParty>().Pokemons.Count > 0)
        {
            yield return DialogManager.Instance.ShowDialogText($"You have already taken a pokemon. Don't be greedy.");
            yield break;
        }
            int selectedChoice = 0;
            yield return DialogManager.Instance.ShowDialogText($"This pokeball contains {pokemonToGive.Base.name}. Would you like to take this pokemon?",
                choices: new List<string>() { "Yes", "No" },
                onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex);

            if (selectedChoice == 0)
            {
                //Yes
                if (used == false)
                {
                    pokemonToGive.init();
                    used = true;
                    
                    var playerParty = player.GetComponent<PokemonParty>();
                    playerParty.AddPokemon(pokemonToGive);
                    playerParty.PartyUpdated();

                    pokemonQuestObject.SetActive(false);

                    if (poliwagPlaceholder != null)
                        poliwagPlaceholder.SetActive(true);
                    if (machopPlaceholder != null)
                        machopPlaceholder.SetActive(true);
                    if (abraPlaceholder != null)
                        abraPlaceholder.SetActive(true);
                    
                    yield return DialogManager.Instance.ShowDialogText($"{pokemonToGive.Base.Name} joined the party!");
                    brotherState1.SetActive(false);
                    brotherState2.SetActive(true);
                }






            }
            else if (selectedChoice == 1)
            {
                //No
                yield return DialogManager.Instance.ShowDialogText($"Okay, take your time! Must be a tough decision.");
            }
        
        else
        {
            yield return DialogManager.Instance.ShowDialogText($"You have already taken a pokemon. Don't be greedy.");
        }
    }
    public object CaptureState()
    {
        return used;
    }
    public void RestoreState(object state)
    {
        used = (bool)state;
    }

}
