using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaFighter
{
    class Arena
    {
        internal bool IsBeneficial { get; private set; } //Has a beneficial effect
        internal bool IsDetrimental { get; private set; } //Has a detrimental effect
        internal bool IsDiscriminating { get; private set; } //Randomly affects only one of the combatants, both if false.
        internal bool IsRandom { get; private set; } //Not an ongoing effect, random chance per round.
        internal int EffectPotency { get; private set; }
        internal bool AffectsHealthOrStrength { get; private set; } //Health if true, Strength if false. 
        public static List<Arena> ArenaList { get; private set; }
        public string ArenaName { get; private set;}
        public static Random ArenaSeed = new Random();

        enum ArenaNames
        {
            Stadium,
            Ground,
            Coliseum,
            Arena,
            Park,
            Megadome
        };

        enum ArenaAdjectives
        {
            the,
            old,
            worn,
            newer,
            park,
            arena
        };

        public Arena()
        {
            ArenaName = Enum.GetName(typeof(ArenaAdjectives), Character.RandomDieRoll(0, 5)) + " " + Enum.GetName(typeof(ArenaNames), Character.RandomDieRoll(0, 5));
            if (ArenaSeed.NextDouble() >= 0.5)
            {
                IsBeneficial = true;
            }
            else
            {
                IsDetrimental = true;
            }
            IsDiscriminating = ArenaSeed.NextDouble() >= 0.5 ? true : false;
            IsRandom = ArenaSeed.NextDouble() >= 0.5 ? true : false;
            AffectsHealthOrStrength = ArenaSeed.NextDouble() >= 0.5 ? true : false;
            EffectPotency = Character.RandomDieRoll(1, 4);
        }
        
        public static void GenerateNewArenas(int AmountOfArenas)
        {
            ArenaList = new List<Arena>();
            for (int i=0; i<AmountOfArenas; i++)
            {
                Arena _newArena = new Arena();
                ArenaList.Add(_newArena);
            }
        }

        public static void DisplayArenas()
        {
            for(int i=0; i < ArenaList.Count; i++)
            {
                string _arenatype = ArenaList[i].IsBeneficial ? "beneficial" : "detrimental";
                string _arenadiscriminate = ArenaList[i].IsDiscriminating ? "a random" : "both";
                string _arenarandom = ArenaList[i].IsRandom ? "random" : "persistent";
                string _arenastat = ArenaList[i].AffectsHealthOrStrength ? "health" : "strength";
                Console.WriteLine($"{ArenaList[i].ArenaName} is an arena with a {_arenatype} effect on {_arenadiscriminate} combatants {_arenastat}. The effect is {_arenarandom}.");
            }
        }

        public static void DisplayArena(int arenaNr)
        {
            string _arenatype = ArenaList[arenaNr].IsBeneficial ? "beneficial" : "detrimental";
            string _arenadiscriminate = ArenaList[arenaNr].IsDiscriminating ? "a random" : "both";
            string _arenarandom = ArenaList[arenaNr].IsRandom ? "random" : "persistent";
            string _arenastat = ArenaList[arenaNr].AffectsHealthOrStrength ? "health" : "strength";
            Console.WriteLine($"The battle will be fought at {ArenaList[arenaNr].ArenaName}. It is an arena with a {_arenatype} effect on {_arenadiscriminate} combatants {_arenastat}. The effect is {_arenarandom}. \n");
        }
    }
}
