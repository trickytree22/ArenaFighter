using System;

namespace ArenaFighter
{
    class Character
    {
        public string Name { get; private set; }
        public int Strength { get; private set; }
        public int Health { get; private set; }
        private int MaxHealth;
        public bool IsAlive { get; private set; }
        public bool IsRetired { get; private set; }
        public bool IsControlledCharacter { get; private set; }
        public static int FoughtOpponents { get; set; }
        internal static int Wealth { get; set; }

        enum FighterNames
        {
            Duchess,
            Yellowbelly,
            Maverick,
            Kojak,
            Bert,
            Ernie,
            Kermit,
            Piggy,
            Cookiemonster
        };



        public Character(bool playerFighter)
        {
            if (playerFighter)
            {
                Console.WriteLine("Input name of your arena fighter");
                Name = Console.ReadLine();
                do
                {
                    string _happy = RandomizeAttributes(this);
                    Console.WriteLine($"Are these stats ok? \n {_happy} \n Press 'y' to confirm, any other key to try again.");
                } while (Console.ReadKey().Key != ConsoleKey.Y);
            }
            else
            {
                Name = Enum.GetName(typeof(FighterNames), RandomDieRoll(0, 8));
                MaxHealth = RandomDieRoll(12, 50);
                Health = MaxHealth;
                Strength = RandomDieRoll(1, 6);
            }
            IsControlledCharacter = playerFighter;
            IsAlive = true;
            IsRetired = false;

        }

        internal int FightPurse()
        {
            int _fightpurse = this.MaxHealth * this.Strength;
            Wealth += _fightpurse;
            return Wealth;
        }

        internal void TakeBeating(int hit)
        {
            Health -= hit;
            if (Health < 1)
            {
                IsAlive = false;
            }
        }

        public string AdjustHealth(int points, bool healthOperator)
        {
            string _HealthString;
            if (!healthOperator)
            { 
                Health -= points;
                _HealthString = $"{Name} receives {points} damage.";
            }
            else
            { 
                Health += points;
                _HealthString = $"{Name} receives {points} health.";
            }
            if (Health > MaxHealth)
            { Health = MaxHealth; }
            return _HealthString;
        }

        internal void RetireCharacter()
        {
            this.IsRetired = true;
        }

        private string RandomizeAttributes(Character character)
        {
            character.MaxHealth = RandomDieRoll(35,50);
            character.Health = character.MaxHealth;
            character.Strength = RandomDieRoll(1, 6);
            return $"Health {character.MaxHealth} \n Strength {character.Strength}";
        }


        public static int RandomDieRoll(int minValue, int maxValue)
        {
            Random rnd = new Random();
            int adjustedMax = maxValue + 1;
            int dice = rnd.Next(minValue, adjustedMax);
            return dice;
        }
    }
}
