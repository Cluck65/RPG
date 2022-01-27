using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonChoiceGiver : MonoBehaviour, ISavable
{
    [SerializeField] Pokemon pokemonToGive;

    [SerializeField] GameObject poliwagItem;
    [SerializeField] GameObject machopItem;
    [SerializeField] GameObject abraItem;

    [SerializeField] GameObject poliwagPlaceholder;
    [SerializeField] GameObject machopPlaceholder;
    [SerializeField] GameObject abraPlaceholder;

    [SerializeField] GameObject brotherState1;
    [SerializeField] GameObject brotherState2;

    bool used;

    private void Awake()
    {
        if (used == true)
        {
            brotherState1.SetActive(false);
            brotherState2.SetActive(true);

        }
    }
    public IEnumerator GiveChoicePokemon (Transform player, Dialog dialog)
    {
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
                var playerParty = player.GetComponent<PokemonParty>();
                playerParty.AddPokemon(pokemonToGive);
                playerParty.PartyUpdated();

                poliwagItem.SetActive(false);
                machopItem.SetActive(false);
                abraItem.SetActive(false);

                if (poliwagPlaceholder != null)
                    poliwagPlaceholder.SetActive(true);
                if (machopPlaceholder != null)
                    machopPlaceholder.SetActive(true);
                if (abraPlaceholder != null)
                    abraPlaceholder.SetActive(true);
                used = true;
                yield return DialogManager.Instance.ShowDialogText($"{pokemonToGive.Base.Name} joined the party!");
                brotherState1.SetActive(false);
                brotherState2.SetActive(true);
            } else
                    {
                        yield return DialogManager.Instance.ShowDialogText($"You have already taken a pokemon. Don't be greedy.");
                    }
                
           


        }
        else if (selectedChoice == 1)
        {
            //No
            yield return DialogManager.Instance.ShowDialogText($"Okay, take your time! Must be a tough decision.");
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
