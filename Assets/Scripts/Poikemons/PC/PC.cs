using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    [SerializeField] List<PokemonSlot> slots;

    public List<PokemonSlot> Slots => slots;
    public static PC GetPC()
    {
        return FindObjectOfType<PlayerController>().GetComponent<PC>();
    }
}

[Serializable]
public class PokemonSlot
{
    [SerializeField] PokemonBase pokemon;


    public PokemonBase Pokemon => pokemon;


}