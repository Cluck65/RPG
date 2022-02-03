using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Pokemon/Create new move")]

public class MoveBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] PokemonType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] bool alwaysHits;
    [SerializeField] int pp;
    [SerializeField] int priority;
    [SerializeField] int multiChanceMove;
    [SerializeField] MoveCategory category;
    [SerializeField] MoveEffects effects;
    [SerializeField] List<SecondaryEffects> secondaries;
    [SerializeField] MoveTarget target;
    [SerializeField] bool isTwoTurnMove;
    [SerializeField] string twoTurnMoveDescription;
    

    public string Name { 
        get { return name; }
    }

    public string Description {
        get { return description; }
    }

    public PokemonType Type {
        get { return type; }
    }

    public int Power {
        get { return power; }
    }

    public int Accuracy {
        get { return accuracy; }
    }

    public bool AlwaysHits
    {
        get { return alwaysHits; }
    }

    public int PP {
        get { return pp; }
    }

    public int Priority {
        get { return priority; }
    }

    public MoveCategory Category {
        get { return category; }
    }

    public MoveEffects Effects {
        get { return effects; }
    }
    public List<SecondaryEffects> Secondaries {
        get { return secondaries; }

    }


    public MoveTarget Target {
        get { return target; }
    }
    public bool IsTwoTurnMove {
        get { return isTwoTurnMove; }
    }
    public string TwoTurnMoveDescription
    {
        get { return twoTurnMoveDescription; }
    }
    public int MultiChanceMove
    {
        get { return multiChanceMove; }
    }

}


[System.Serializable]
public class MoveEffects
{
    [SerializeField] List<StatBoost> boosts;
    [SerializeField] ConditionID status;
    [SerializeField] ConditionID volatileStatus;
    [SerializeField] bool recoil;
    [SerializeField] int healAmount;
    [SerializeField] bool isInvunerableThisRound;
    
    public List<StatBoost> Boosts {
        get { return boosts; }
    }

    public ConditionID Status {
        get { return status; }
    }

    public ConditionID VolatileStatus {
        get { return volatileStatus; }
    }

    public bool Recoil
    {
        get { return recoil; }
    }

    public int HealAmount
    {
        get { return healAmount; }
    }

    public bool IsInvunerableThisRound
    {
        get { return isInvunerableThisRound; }
    }
}
[System.Serializable]

public class SecondaryEffects : MoveEffects
{
    [SerializeField] int chance;
    [SerializeField] MoveTarget target;

    public int Chance {
        get { return chance; }
    }

    public MoveTarget Target {
        get { return target; }
    }
}

[System.Serializable]
public class StatBoost
{
    public Stat stat;
    public int boost;

}

public enum MoveCategory
{
    Physical, Special, Status
}


public enum MoveTarget
{
    Foe, Self
}