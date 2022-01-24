using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{

    [SerializeField] List<Pokemon> pokemons;


    public event Action OnUpdated;


    public List<Pokemon> Pokemons {
        get {
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

    public void AddPokemon (Pokemon newPokemon)
    {
        if (pokemons.Count < 6)
        {
            pokemons.Add(newPokemon);
            OnUpdated?.Invoke();
        }
        else
        {
            //Add to the PC once thats implemented
        }
    }
    public void RemovePokemon(Pokemon pokemonToRemove)
    {
            pokemons.Remove(pokemonToRemove);
            OnUpdated?.Invoke();
    }

    public void SwitchPokemonToFirst(Pokemon newFirstPokemon)
    {
        string newFirstPokemonName = newFirstPokemon.Base.Name;
        int newFirstPokemonIndex = pokemons.IndexOf(pokemons.Where(p => p.Base.Name == newFirstPokemonName).FirstOrDefault());
        Swap(pokemons, newFirstPokemonIndex, 0);
        OnUpdated?.Invoke();  
    }

    public static void Swap<T>(IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }

    public IEnumerator CheckForEvolutions()
    {
        foreach (var pokemon in pokemons)
        {
            var evolution = pokemon.CheckForEvolution();
            if (evolution != null)
            {
                yield return EvolutionManager.i.Evolve(pokemon, evolution);
            }
        }
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
