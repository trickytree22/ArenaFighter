using System;
using System.Collections.Generic;

namespace ArenaFighter
{
    class Round 
    {
       
        public int OpponentAttack { get; set; }
        public int PCAttack { get; set; }
        public string FighterName { get; set; }
        public string OpponentName { get; set; }
        public bool RandomSeed { get; set; }
        public string AppliedEnvironmentals { get; set; }

        public Round(Character PC, Character opponent, int battleVenue)
        {
            FighterName = PC.Name;
            OpponentName = opponent.Name;
            PCAttack = PC.Strength + Character.RandomDieRoll(1, 6);
            OpponentAttack = opponent.Strength + Character.RandomDieRoll(1, 6);
            RandomSeed = (Arena.ArenaSeed.NextDouble() >= 0.5) ? true : false;
            
            if (Arena.ArenaList[battleVenue].IsRandom && !RandomSeed)
            {
                AppliedEnvironmentals = "Nothing out of the ordinary happens\n";
            }
            else 
            {
                AppliedEnvironmentals = ApplyEnvironmental(battleVenue, PC, opponent);
            }

            if (PCAttack > OpponentAttack)
            {
                opponent.TakeBeating(PCAttack);
            }
            else if (OpponentAttack > PCAttack)
            {
                PC.TakeBeating(OpponentAttack);
            }
            else
            {
                //Nothing for now
            }
        }

        public string ApplyEnvironmental(int venue, Character PC, Character opponent)
        {
            string ThisHappens;
            if (Arena.ArenaList[venue].IsDiscriminating)
            {
                if (Arena.ArenaSeed.NextDouble() >= 0.5)
                {
                    ThisHappens = (Arena.ArenaList[venue].AffectsHealthOrStrength)? PC.AdjustHealth(Arena.ArenaList[venue].EffectPotency, Arena.ArenaList[venue].IsBeneficial) : AdjustStrength(venue, PC);
                }
                else
                {
                    ThisHappens = (Arena.ArenaList[venue].AffectsHealthOrStrength) ? opponent.AdjustHealth(Arena.ArenaList[venue].EffectPotency, Arena.ArenaList[venue].IsBeneficial) : AdjustStrength(venue, opponent);
                }
            }
            else
            {
                ThisHappens = (Arena.ArenaList[venue].AffectsHealthOrStrength) ? PC.AdjustHealth(Arena.ArenaList[venue].EffectPotency, Arena.ArenaList[venue].IsBeneficial) : AdjustStrength(venue, PC);
                ThisHappens += (Arena.ArenaList[venue].AffectsHealthOrStrength) ? opponent.AdjustHealth(Arena.ArenaList[venue].EffectPotency, Arena.ArenaList[venue].IsBeneficial) : AdjustStrength(venue, opponent);
            }

            
            return ThisHappens;
        }

        public string AdjustStrength(int venue, Character character)
        {
            bool _GoodOrBad = (Arena.ArenaList[venue].IsBeneficial);
            string _goodOrBad = (_GoodOrBad) ? "positively" : "negatively";
            if (character.IsControlledCharacter)
            {
                PCAttack = (_GoodOrBad) ? PCAttack + Arena.ArenaList[venue].EffectPotency : PCAttack - Arena.ArenaList[venue].EffectPotency;
                if (PCAttack < 0) 
                {
                    PCAttack = 0;
                }
            }
            else
            {
                OpponentAttack = (_GoodOrBad) ? OpponentAttack + Arena.ArenaList[venue].EffectPotency : OpponentAttack - Arena.ArenaList[venue].EffectPotency;
                if (OpponentAttack < 0)
                {
                    OpponentAttack = 0;
                }
            }
            string returnString = $"{character.Name}'s strength is {_goodOrBad} affected by a value of {Arena.ArenaList[venue].EffectPotency}.";
            return returnString;
        }

        public void DisplayRound()
        {
            Console.WriteLine(AppliedEnvironmentals);
            if (this.PCAttack > this.OpponentAttack)
            {
                Console.WriteLine($"{this.FighterName} attacks {this.OpponentName} and deals {this.PCAttack} damage.");
            }
            else if (this.OpponentAttack > this.PCAttack)
            {
                Console.WriteLine($"{this.OpponentName} attacks {this.FighterName} and deals {this.OpponentAttack} damage.");
            }
            else
            {
                Console.WriteLine("The two combatants glare menacingly at eachother.");
            }
        }
    }
}
