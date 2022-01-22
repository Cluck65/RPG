using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopState {  Menu, Buying, Selling, Busy }

public class ShopController : MonoBehaviour
{
    ShopState state;

    public static ShopController i { get; private set; }
    private void Awake()
    {
        i = this;
    }
    public IEnumerator StartTrading(Merchant merchant)
    {
        yield return StartMenuState();
        

    }

    IEnumerator StartMenuState()
    {
        state = ShopState.Menu;

        int selectedChoice = 0;
        yield return DialogManager.Instance.ShowDialogText("Hey, how can I help?",
            waitForInput: false,
            choices: new List<string>() { "Buy", "Sell", "Quit" },
            onChoiceSelected: choiceIndex => selectedChoice = choiceIndex);

        if (selectedChoice == 0)
        {
            //Buy
        }
        else if (selectedChoice == 1)
        {
            //Sell
        }
        else if (selectedChoice == 2)
        {
            //Quit
            yield break;
        }
    }
}
