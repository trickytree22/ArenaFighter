using System;
using System.Collections.Generic;

namespace ArenaFighter
{
    class Battle
    {
        public static List<List<Round>> FightStats = new List<List<Round>>();
        public string OpponentName;
        public int BattleVenue;

        public Battle(Character PC, Character opponent, int fightNumber)
        {
            FightStats.Add(new List<Round>());
            OpponentName = opponent.Name;
            BattleVenue = Character.RandomDieRoll(0, Arena.ArenaList.Count - 1);
            Arena.DisplayArena(BattleVenue);
            int internalcounter = 0;
            while (PC.IsAlive && opponent.IsAlive)
            {
                FightStats[fightNumber].Add(new Round(PC, opponent, BattleVenue));
                FightStats[fightNumber][internalcounter].DisplayRound();
                internalcounter++;
            }
            Console.WriteLine($"You have been paid for participating and now have {opponent.FightPurse()} dead presidents"); //opponent.FightPurse() returns current opponents worth + previous bankroll
        }
    }
}
