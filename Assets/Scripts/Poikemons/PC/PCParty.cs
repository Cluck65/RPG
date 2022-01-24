using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PCParty : MonoBehaviour
{

    [SerializeField] List<Pokemon> pokemons;


    public event Action OnUpdated;


    public List<Pokemon> Pokemons
    {
        get
        {
            return pokemons;
        }
        set
        {
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
    private void Start()
    {

    }


    public Pokemon GetHealthyPokemon()
    {
        return pokemons.Where(x => x.HP > 0).FirstOrDefault();
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



    public void PartyUpdated()
    {
        OnUpdated?.Invoke();
    }
    public static PokemonParty GetPlayerParty()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PokemonParty>();
    }

}

