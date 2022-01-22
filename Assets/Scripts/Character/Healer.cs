using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public IEnumerator Heal (Transform player, Dialog dialog)
    {
        int selectedChoice = 0;

        yield return DialogManager.Instance.ShowDialogText("Take a nap with me man. She's in a mood today.",
            choices:  new List<string>() { "Yes", "No" },
            onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex );

        if (selectedChoice == 0)
        {
            //Yes
            yield return Fader.i.FadeIn(0.5f);
            yield return Fader.i.FadeOut(0.5f);
            yield return Fader.i.FadeIn(0.5f);
            var playerParty = player.GetComponent<PokemonParty>();
            playerParty.Pokemons.ForEach(p => p.Heal());
            playerParty.PartyUpdated();

            yield return Fader.i.FadeOut(0.5f);

            yield return DialogManager.Instance.ShowDialogText($"Your pokemon are fully healed now. Get back to it.");
        }
        else if (selectedChoice == 1)
        {
            //No
            yield return DialogManager.Instance.ShowDialogText($"Okay, Come back if you need any healing in future, I'll be here.");
        }


        
    }
}
