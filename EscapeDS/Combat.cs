using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EscapeDS
{
    public class Combat 
    {
        public enum CombatState { ChooseAction, ReturnFire, ResolvingCombat, CombatEnded } //different stages for each turn
        CombatState currentState;
        //class that runs the combat encounters in the game. pulls stats from whatever card is currently active in the databse. 

        //needs turns
        //1. select action - shoot, reload, heal drone, flank
        //2. resolve chosen action. 
        //3. shoot - if shoot, roll dice and apply result - hits, jams, and misses. Then, roll enemy Hit Die, resolve result. Damage on hits is stored in Cards database
        //4. Compare rolled hits with enemy Strength, which is stored in Cards database. minus-- strength with every hit, when strength is 0, enemy killed, receive rewards.

        private Card card;
        private Player player;



        int Strength;
        int RangedDamage;

        public Random AttackDie = new Random();
        public Random HitDie = new Random();
        public int CurrentCard { get; set; }
        
        public Combat(Card card)
        {
            this.card = card;
            int cardID = card.CardID;
            CurrentCard = cardID;
            Strength = card.Strength;
            

            //Compare the card's strentgh against the payer's dice roll every turn.
            //when strength is reduced to 0, the player wins and gets the reward.

            //Then, roll car's hit dice. if hit, reduce player health. 0 = game over.
        }
        

        public string ActionChoice;
        public string WeaponChoice;
        public int W1AttackDamage = 2;
        public int W1Ammo = 2;
        public bool W1IsJammed = false;

        public int W2AttackDamage = 1;
        public int W2Ammo = 1;
        public bool W2IsJammed = false;
        
        
        public void DoCombat(Player player, Card card, string ActionChoice)
        {
            this.card = card;  // Initialize the card field first
            Strength = card.Strength;
            CurrentCard = card.CardID;
            this.player = player; // Initialize the player field after the card field

            currentState = CombatState.ChooseAction;
            while (currentState == CombatState.ChooseAction)
            {
                Console.WriteLine("Choose action: 1) Attack \n2)Flank \n3) Heal\n Reload");
                ActionChoice = Console.ReadLine().ToLower();

                switch (ActionChoice)
                {
                    case "1": //choose to attack, select weapon, roll attack die, resolve result, then roll hit die and resolve result.
                        {
                            
                            int damage;
                            Console.WriteLine($"enemy has {card.Strength}");
                            Console.WriteLine("Attack! \nSelect which weapon slot to use. \n1) 2)");
                            WeaponChoice = Console.ReadLine().ToLower();

                            switch (WeaponChoice)
                            {
                                
                                case "1":
                                    {
                                        if (W1Ammo > 0)
                                        {
                                            TryHitTarget();
                                            damage = W1AttackDamage;
                                            this.card.Strength -= damage;

                                        }
                                        break;
                                    }

                                case "2":
                                    {
                                        if (W2Ammo > 0) //needs something like IsJammed, IsReloading -> CanFire = false; which then resets on new turn. 
                                        {
                                            switch (TryHitTarget())
                                            {
                                                case 1:
                                                    {
                                                        damage = 0;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        damage = 0;
                                                        W2IsJammed = true;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        damage = W2AttackDamage;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        damage = W2AttackDamage;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        break;
                                                    }
                                                case 5:
                                                    {
                                                        damage = W2AttackDamage;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        break;
                                                    }
                                                case 6:
                                                    {
                                                        damage = W2AttackDamage * 2;
                                                        Console.WriteLine("Crit!");
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                    }

                                                    break;
                                            }

                                        }
                                        break;
                                    }

                                default:
                                    break;
                            }
                            break;
                        }

                    case "2":
                        {
                            break;
                        }

                    default:
                        break;
                }
            }

        }

        private void ResolveHitDie(Player player, Card card)
        {
            currentState = CombatState.ResolvingCombat;
            int result;
            result = HitDie.Next(1, 4);

            switch(result)
            {
                case 1:
                    {
                        player.TakeDamage(0);
                        Console.WriteLine("Enemy missed return fire");
                        break;
                    }

                case 2:
                    {
                        player.TakeDamage(card.RangeDamage);
                        Console.WriteLine($"Enemy dealt {card.RangeDamage}");
                        break;
                    }

                case 3:
                    {
                        player.TakeDamage(card.RangeDamage);
                        Console.WriteLine($"Enemy dealt {card.RangeDamage}");
                        break;
                    }


                  default:;
                    break;
            }

        }

        public int TryHitTarget()
        {
            currentState = CombatState.ReturnFire;
            int result;
            result = AttackDie.Next(1, 7);
            switch (result)
            {
                case 1:
                {
                        Console.WriteLine("miss");
                        break;

                    }

                case 2:
                    {
                        Console.WriteLine("Jam");
                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine("Hit");
                        break;
                    }
                    case 4:
                    {
                        Console.WriteLine("Hit");
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Hit");
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Double hit!");
                        break;
                    }

            }


            return result; //just to clear error
        }


    }
}
