using System;
using System.Collections.Generic;
using System.Linq;

public static class FactionsUtils
{
    private const FactionsRelation Neutral = FactionsRelation.Neutral;
    private const FactionsRelation Enemy = FactionsRelation.Enemy;
    private const FactionsRelation Ally = FactionsRelation.Ally;
    private static readonly FactionsRelation[][] _factionsRelations = new FactionsRelation[][]
    {
       //       Neutral  Player   Ally     Enemy1   Enemy2   Enemy3
       new [] { Ally,    Neutral, Neutral, Neutral, Neutral, Neutral, }, // Neutral
       new [] { Neutral, Ally,    Ally,    Enemy,   Enemy,   Enemy,   }, // Player
       new [] { Neutral, Ally,    Ally,    Enemy,   Enemy,   Enemy,   }, // Ally
       new [] { Neutral, Enemy,   Enemy,   Ally,    Enemy,   Enemy,   }, // Enemy1
       new [] { Neutral, Enemy,   Enemy,   Enemy,   Ally,    Enemy,   }, // Enemy2
       new [] { Neutral, Enemy,   Enemy,   Enemy,   Enemy,   Ally,    }, // Enemy3
    };

    static FactionsUtils()
    {
        var factions = (Faction[])Enum.GetValues(typeof(Faction));
        if (_factionsRelations.Length < factions.Length || _factionsRelations.Any(x => x.Length < factions.Length))
        {
            Logger.Error("Factions table is invalid");
            return;
        }

        FactionsRelations = factions
            .Select((Faction, Index) => (Faction, Index))
            .ToDictionary(x => x.Faction, x => (IReadOnlyDictionary<Faction, FactionsRelation>)factions
                .Select((Faction, Index) => (Faction, Index))
                .ToDictionary(y => y.Faction, y => _factionsRelations[x.Index][y.Index]));
    }

    public static IReadOnlyDictionary<Faction, IReadOnlyDictionary<Faction, FactionsRelation>> FactionsRelations { get; private set; }

    public static FactionsRelation GetFactionsRelation(Faction faction1, Faction faction2)
    {
        if (!FactionsRelations.ContainsKey(faction1) || !FactionsRelations.ContainsKey(faction2))
        {
            return FactionsRelation.None;
        }
        return FactionsRelations[faction1][faction2];
    }

    public static bool IsAllyTo(this Faction faction1, Faction faction2) => GetFactionsRelation(faction1, faction2) == FactionsRelation.Ally;
    public static bool IsNeutralTo(this Faction faction1, Faction faction2) => GetFactionsRelation(faction1, faction2) == FactionsRelation.Neutral;
    public static bool IsEnemyTo(this Faction faction1, Faction faction2) => GetFactionsRelation(faction1, faction2) == FactionsRelation.Enemy;
}

[Flags]
public enum Faction
{
    Neutral = 1,
    Player = 2,
    Ally = 4,
    Enemy1 = 8,
    Enemy2 = 16,
    Enemy3 = 32,
}

public enum FactionsRelation
{
    None = 0,
    Ally = 1,
    Neutral = 2,
    Enemy = 3,
}
