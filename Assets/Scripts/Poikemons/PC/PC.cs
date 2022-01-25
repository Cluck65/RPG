using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [SerializeField] List<Pokemon> pokemons;


    [SerializeField] List<PokemonSlot> slots;
    public List<PokemonSlot> Slots => slots;

    public event Action OnUpdated;

    public List<Pokemon> Pokemons {
        get {
            return pokemons;
        }
        set {
            pokemons = value;
            OnUpdated?.Invoke();
        }
    }

    private void Awake()
    {
        foreach (var pokemon in pokemons)
        {
            pokemon.init();
        }
    }


    public static PC GetPC()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PC>();
    }

    public void DepositPokemon(Pokemon pokemonToDeposit)
    {
        pokemons.Add(pokemonToDeposit);
        OnUpdated?.Invoke();
    }
    public void WithdrawPokemon(Pokemon pokemonToWithdraw)
    {
        pokemons.Remove(pokemonToWithdraw);
        OnUpdated?.Invoke();
    }
}

[Serializable]
public class PokemonSlot
{
    [SerializeField] PokemonBase pokemon;


    public PokemonBase Pokemon => pokemon;



}


    






