using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonSlotUI : MonoBehaviour
{
    [SerializeField] Text nameText;

    RectTransform pokemonListRect;
    private void Awake()
    {
        pokemonListRect = GetComponent<RectTransform>();
    }
    public Text NameText => nameText;

    public float Height => pokemonListRect.rect.height;
    
    public void SetData(PokemonSlot pokemonSlot)
    {
        nameText.text = pokemonSlot.Pokemon.name;
    }
}
