using System;
using System.Collections.Generic;

namespace ArenaFighter
{
    
    class Program
    {
        public List<Battle> FightRecord = new List<Battle>();
        
        static void Main(string[] args)
        {
            Arena.GenerateNewArenas(6);
            Arena.DisplayArenas();
            Character PlayerCharacter = new Character(true);
            List<Battle> FightRecord = new List<Battle>();
            while ((PlayerCharacter.IsAlive == true) && (PlayerCharacter.IsRetired != true))
            {
                Character Opponent = new Character(false);
                FightRecord.Add(new Battle(PlayerCharacter, Opponent, FightRecord.Count));
                if (PlayerCharacter.IsAlive)
                {
                    Console.WriteLine("If you want to retire your fighter, press 'y', press any other key to keep fighting.");
                    if (Console.ReadKey().Key == ConsoleKey.Y)
                    {
                        PlayerCharacter.RetireCharacter();
                    }
                }
            }
            Character.FoughtOpponents = FightRecord.Count;
            int playerscore = Character.Wealth + (Character.FoughtOpponents * (PlayerCharacter.IsAlive ? 32 : 18));
            string UserInput;
            do
            {
                Console.WriteLine($"You finished the game with a score of {playerscore}");
                Console.WriteLine("Do you want to view the battlelog? Type the corresponding battlenumber to view that fight, or 'x' when you're done.");
                for (int i = 0; i < FightRecord.Count; i++)
                {
                    Console.WriteLine($"Battle {i}: vs. {FightRecord[i].OpponentName} at {Arena.ArenaList[FightRecord[i].BattleVenue].ArenaName}");
                }
                UserInput = Console.ReadLine();
                int battlenumber=-1;
                bool isNumber = int.TryParse(UserInput, out battlenumber);
                if ((battlenumber < FightRecord.Count) && (isNumber))
                {
                    Console.WriteLine(battlenumber);
                    foreach(Round round in Battle.FightStats[battlenumber])
                    {
                        round.DisplayRound();
                        //Battle.FightStats[battlenumber][round].DisplayRound();
                    }
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            } while (UserInput != "x");
            
        }
    }
}
