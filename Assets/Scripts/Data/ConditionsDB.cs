using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionId = kvp.Key;
            var condition = kvp.Value;

            condition.Id = conditionId;
        }
    }


    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>() 
    {
        {
            ConditionID.psn,
            new Condition()
            {
            Name = "Poison",
            StartMessage = "has been poisoned!",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.DecreaseHP(pokemon.MaxHp / 8);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} was hurt by the poison!");
                }
            }
        },
         {
            ConditionID.brn,
            new Condition()
            {
            Name = "Burn",
            StartMessage = "has been burned!",
                OnAfterTurn = (Pokemon pokemon) =>
                {
                    pokemon.DecreaseHP(pokemon.MaxHp / 16);
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} was hurt by the burn!");
                }
            }
        },
         {
            ConditionID.par,
            new Condition()
            {
            Name = "Paralysed",
            StartMessage = "has been paralysed!",
            OnBeforeMove =  (Pokemon pokemon) =>
            {
                if (Random.Range(1, 5) == 1)
                {
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is paralyzed and can't move!");
                    return false;
                }

                return true;
            }
            }
        },
         {
            ConditionID.frz,
            new Condition()
            {
            Name = "Freeze",
            StartMessage = "has been frozen!",
            OnBeforeMove =  (Pokemon pokemon) =>
            {
                if (Random.Range(1, 5) == 1)
                {
                    pokemon.CureStatus();
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} broke out of the ice!");
                    return true;
                }

                return false;
            }
            }
        },
         {
            ConditionID.slp,
            new Condition()
            {
            Name = "Sleep",
            StartMessage = "has fallen asleep!",
            OnStart = (Pokemon pokemon) =>
            {
                //Sleep for 1-3 turns
                pokemon.StatusTime = Random.Range(1, 4);
                Debug.Log($"Will be asleep for {pokemon.StatusTime} moves");

            },
            OnBeforeMove =  (Pokemon pokemon) =>
            {
                if(pokemon.StatusTime <= 0)
                {
                    pokemon.CureStatus();
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} woke up!");
                    return true;
                }

                pokemon.StatusTime--;
                pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is fast asleep.");
                return false;
            }
            }
        },

         //Volatile Status Conditions
         {
            ConditionID.confusion,
            new Condition()
            {
            Name = "Confusion",
            StartMessage = "is  confused!",
            OnStart = (Pokemon pokemon) =>
            {
                //Confused for 1-4 turns
                pokemon.VolatileStatusTime = Random.Range(1, 5);
                Debug.Log($"Will be confused for {pokemon.VolatileStatusTime} moves");

            },
            OnBeforeMove =  (Pokemon pokemon) =>
            {
                if(pokemon.VolatileStatusTime <= 0)
                {
                    pokemon.CureVolatileStatus();
                    pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} snapped out of its confusion!");
                    return true;
                }
                pokemon.VolatileStatusTime--;

                // 50% chance to do a move
                if (Random.Range(1, 3) == 1)
                    return true;

                //Hurt by confusion 
                pokemon.StatusChanges.Enqueue($"{pokemon.Base.Name} is confused!");
                pokemon.DecreaseHP(pokemon.MaxHp / 8);
                pokemon.StatusChanges.Enqueue($"It hurt itself due to confusion");
                return false;

            }

            }
        }


    };

    public static float GetStatusBonus(Condition condition)
    {
        if (condition == null)
            return 1f;
        else if (condition.Id == ConditionID.slp || condition.Id == ConditionID.frz)
            return 2f;
        else if (condition.Id == ConditionID.par || condition.Id == ConditionID.psn || condition.Id == ConditionID.brn)
            return 1.5f;

        return 1f;
    }

}


public enum ConditionID
{
    none, psn, brn, slp, par, frz,
    confusion
}