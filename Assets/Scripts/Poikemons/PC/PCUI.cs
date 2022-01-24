using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PCUIState {  PCStart, PCSelection, PartySelection, Busy, Exit, ChoiceSelection }
public class PCUI : MonoBehaviour
{
    [SerializeField] GameObject pokemonList;
    [SerializeField] PokemonSlotUI pokemonSlotUI;

    [SerializeField] Image pokemonIcon;
    [SerializeField] Text pokemonDescription;

    [SerializeField] Image upArrow;
    [SerializeField] Image downArrow;

    [SerializeField] PartyScreen partyScreen;

    PCUI pcUI;


    int selectedPokemon = 0;
    PCUIState state;

    const int itemsInViewPort = 8;

    List<PokemonSlotUI> slotUIList;

    PC pc;
    RectTransform pokemonListRect;
    private void Awake()
    {
        pc = PC.GetPC();
        pokemonListRect = pokemonList.GetComponent<RectTransform>();
    }

    private void Start()
    {
        UpdatePCList();
    }

    void UpdatePCList()
    {
        //Clear all existing pokemon
        foreach (Transform child in pokemonList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<PokemonSlotUI>();
        foreach (var pokemonSlot in pc.Slots)
        {
            var slotUIObj = Instantiate(pokemonSlotUI, pokemonList.transform);
            slotUIObj.SetData(pokemonSlot);

            slotUIList.Add(slotUIObj);
        }

        UpdateItemSelection();
    }
    public void HandleUpdate(Action onBack)
    {
        if (state == PCUIState.PCStart)
        {
            gameObject.SetActive(true);
            StartCoroutine(StartPC());
        }
        else if (state == PCUIState.PCSelection)
        {
            Action onSelected = () =>
            {
                //WithdrawPokemon
                Debug.Log("Withdrawing " + selectedPokemon);
            };
            Action onBackPCScreen = () =>
            {
                state = PCUIState.PCStart;
            };
            int prevSelection = selectedPokemon;

            if (Input.GetKeyDown(KeyCode.DownArrow))
                ++selectedPokemon;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                --selectedPokemon;

            selectedPokemon = Mathf.Clamp(selectedPokemon, 0, pc.Slots.Count - 1);

            if (prevSelection != selectedPokemon)
                UpdateItemSelection();
            if (Input.GetKeyDown(KeyCode.Z))
                Debug.Log("Testing.");




            else if (Input.GetKeyDown(KeyCode.X))
            {
                state = PCUIState.Exit;
                onBack?.Invoke();
            }
               

        }
        else if (state == PCUIState.PartySelection)
        {
            Action onSelected = () =>
            {
                //Deposit selected pokemon
                Debug.Log("Depositing " + partyScreen.SelectedMember.Base.Name);

            };

            Action onBackPartyScreen = () =>
            {
                ClosePartyScreen();
                state = PCUIState.PCStart;
            };
            partyScreen.HandleUpdate(onSelected, onBackPartyScreen);
        }
        else if (state == PCUIState.Exit)
        {
            state = PCUIState.PCStart;
            onBack?.Invoke();
        }
    }

    public IEnumerator DepositPokemon(PlayerController player)
    {
        var pokemon = partyScreen.SelectedMember;
        Debug.Log("Selected pokemon is " + pokemon.Base.Name);

        pokemon.init();
        player.GetComponent<PokemonParty>().RemovePokemon(pokemon);



        string dialogText = $"{player.Name} deposited {pokemon.Base.Name}!";


        yield return DialogManager.Instance.ShowDialogText("Depositing " + pokemon);
    }
    public IEnumerator WithdrawPokemon(PlayerController player)
    {
        var pokemonToWithdraw = partyScreen.SelectedMember;
        Debug.Log("Selected pokemon is " + pokemonToWithdraw.Base.Name);

        player.GetComponent<PokemonParty>().AddPokemon(pokemonToWithdraw);

        //pokemonToWithdraw.init();

        player.GetComponent<PokemonParty>().AddPokemon(pokemonToWithdraw);



        string dialogText = $"{player.Name} withdrew {pokemonToWithdraw.Base.Name}!";
        yield return DialogManager.Instance.ShowDialogText(dialogText);
    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < slotUIList.Count; i++)
        {
            if (i == selectedPokemon)
                slotUIList[i].NameText.color = GlobalSettings.i.HighlightedColor;
            else
                slotUIList[i].NameText.color = Color.black;
        }

        var pokemon = pc.Slots[selectedPokemon].Pokemon;
        pokemonIcon.sprite = pokemon.FrontSprite;
        pokemonDescription.text = pokemon.Description;

        HandleScrolling();
    }

    void HandleScrolling()
    {
        if (slotUIList.Count <= itemsInViewPort) return;

        float scrollPos = Mathf.Clamp(selectedPokemon - itemsInViewPort / 2, 0, selectedPokemon) * slotUIList[0].Height;
        pokemonListRect.localPosition = new Vector2(pokemonListRect.localPosition.x, scrollPos);

        bool showUpArrow = selectedPokemon > itemsInViewPort / 2;
        upArrow.gameObject.SetActive(showUpArrow);
        bool showDownArrow = selectedPokemon + itemsInViewPort / 2 < slotUIList.Count;
        downArrow.gameObject.SetActive(showDownArrow);
    }

    void OpenPCScreen()
    {
        state = PCUIState.PCSelection;
        this.gameObject.SetActive(true);
    }

    void OpenPartyScreen()
    {
        state = PCUIState.PartySelection;
        partyScreen.gameObject.SetActive(true);
    }

    void ClosePartyScreen()
    {
        state = PCUIState.PCSelection;
        partyScreen.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }



    public IEnumerator StartPC()
    {
        state = PCUIState.PCStart;
        int selectedChoice = 0;
        yield return DialogManager.Instance.ShowDialogText("Booted up the PC. What would you like to do?", false,
            choices: new List<string>() { "Deposit", "Withdraw", "Shut Down" },
            onChoiceSelected: (choiceIndex) => selectedChoice = choiceIndex);
        state = PCUIState.ChoiceSelection;
        if (selectedChoice == 0)
        {
            //Deposit
            state = PCUIState.PartySelection;
            OpenPartyScreen();
        }
        else if (selectedChoice == 1)
        {
            //Withdraw
            state = PCUIState.PartySelection;
            OpenPCScreen();
        }

        else if (selectedChoice == 2)
        { 
            //Exit
            state = PCUIState.Exit;
            gameObject.SetActive(false);
            yield break;
        }
    }

}
