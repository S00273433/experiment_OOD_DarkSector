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
        CombatState currentState = CombatState.ChooseAction;
        //class that runs the combat encounters in the game. pulls stats from whatever card is currently active in the databse. 

        //needs turns
        //1. select action - shoot, reload, heal drone, flank
        //2. resolve chosen action. 
        //3. shoot - if shoot, roll dice and apply result - hits, jams, and misses. Then, roll enemy Hit Die, resolve result. Damage on hits is stored in Cards database
        //4. Compare rolled hits with enemy Strength, which is stored in Cards database. minus-- strength with every hit, when strength is 0, enemy killed, receive rewards.

        //combat basically fully implemented 01/04/2026 in Tester. needs to link to XAML and such, not started yet...

        private Card card;
        private Player player;
        private Inventory Inventory;
        private EventsTurns EvTurns;



        int Strength;
        int RangedDamage;
        int Health;
        int Reward;

        public Random AttackDie = new Random();
        public Random HitDie = new Random();
        public int CurrentCard { get; set; }
        
        public Combat(Card card, Player player, EventsTurns EvTurns)
        {
            this.player = player;
            this.card = card;
            this.EvTurns = EvTurns;
            int cardID = card.CardID;
            CurrentCard = cardID;
            Strength = card.Strength;
            Health = player.Health;
            Reward = card.CardReward;


            //Compare the card's strentgh against the payer's dice roll every turn.
            //when strength is reduced to 0, the player wins and gets the reward.

            //Then, roll car's hit dice. if hit, reduce player health. 0 = game over.
        }
        

        public string ActionChoice;
        public string WeaponChoice;
        public int W1AttackDamage = 1;
        public int W1Ammo = 1;
        public bool W1IsJammed = false;
        public bool W1IsEmpty = false;

        public int W2AttackDamage = 2;
        public int W2Ammo = 2;
        public bool W2IsJammed = false;
        public bool W2IsEmpty = false;

        public bool IsHealUsed = false; //heal drone can only be used once per combat, needs to reset on new combat.

        public void DoCombat(Player player, Card card, string ActionChoice)
        {

            
            currentState = CombatState.ChooseAction;
            while (currentState == CombatState.ChooseAction)
            {
                
                Console.WriteLine("\nChoose action: \n1) Attack \n2) Heal\n 3)Reload");
                ActionChoice = Console.ReadLine().ToLower();

                switch (ActionChoice)
                {
                    case "1": //choose to attack, select weapon, roll attack die, resolve result, then roll hit die and resolve result.
                        {
                            //attack case
                            int damage;
                            Console.WriteLine($"enemy has {card.Strength} health");
                            currentState = CombatState.ResolvingCombat;
                            Console.WriteLine("Attack! \nSelect which weapon slot to use. \n1) \n2)");
                            WeaponChoice = Console.ReadLine().ToLower();

                            switch (WeaponChoice)
                            {
                                
                                case "1":
                                    {
                                        if (W1Ammo > 0)
                                        {
                                            switch (TryHitTarget())
                                            {
                                                case 1:
                                                    {
                                                        damage = 0;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W1Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        damage = 0;
                                                        W1IsJammed = true;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        damage = W1AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W1Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        damage = W1AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W1Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 5:
                                                    {
                                                        damage = W1AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        currentState = CombatState.ChooseAction;
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W1Ammo--;
                                                        break;
                                                    }
                                                case 6:
                                                    {
                                                        damage = W1AttackDamage * 2;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine("Crit!");
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W1Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                            }

                                        }
                                        if (W1Ammo <= 0)
                                        {
                                            Console.WriteLine("Weapon empty, please choose another action");
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
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W2Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 2:
                                                    {
                                                        damage = 0;
                                                        W2IsJammed = true;
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 3:
                                                    {
                                                        damage = W2AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W2Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 4:
                                                    {
                                                        damage = W2AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W2Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 5:
                                                    {
                                                        damage = W2AttackDamage;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card); 
                                                        Console.WriteLine($" enemy health now {card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W2Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    }
                                                case 6:
                                                    {
                                                        damage = W2AttackDamage * 2;
                                                        card.TakeDamage(damage);
                                                        Console.WriteLine("Crit!");
                                                        Console.WriteLine($"enemy takes {damage}");
                                                        ResolveHitDie(player, card);
                                                        Console.WriteLine($" enemy health now { card.Strength}");
                                                        Console.WriteLine($" player health now {player.Health}");
                                                        W2Ammo--;
                                                        currentState = CombatState.ChooseAction;
                                                        break;
                                                    } 
                                            }

                                        }
                                        if (W2Ammo <= 0)
                                        {
                                            Console.WriteLine("Weapon empty, please choose another action");
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
                            if(IsHealUsed)
                            {
                                Console.WriteLine("Heal drone already used, please choose another action");
                                currentState = CombatState.ChooseAction;
                                break;
                            }
                            //HEAL case - Extra round to attack, no return fire.
                            player.Heal(1);
                            Console.WriteLine($"heal drone activated - Health increased by 1\n health now{player.Health}");
                            Console.WriteLine("Heal unavailable until next combat");
                            IsHealUsed = true;
                            currentState = CombatState.ChooseAction;    
                            break;
                        }

                    case "3":
                        {
                            //RELOAD case - reload weapon, no return fire. needs to check if weapon is jammed, if so, unjam it. 
                            Console.WriteLine("Select which weapon slot to reload. \n1) \n2)");
                            WeaponChoice = Console.ReadLine().ToLower();
                            switch (WeaponChoice)
                            {
                                case "1":
                                    {
                                        W1Ammo = 1;
                                        W1IsJammed = false;
                                        Console.WriteLine("Weapon 1 reloaded");
                                        currentState = CombatState.ChooseAction;
                                        break;
                                    }
                                case "2":
                                    {
                                        W2Ammo = 2;
                                        W2IsJammed = false;
                                        Console.WriteLine("Weapon 2 reloaded");
                                        currentState = CombatState.ChooseAction;
                                        break;
                                    }
                                default:
                                    Console.WriteLine("Invalid weapon choice, please choose another action");
                                    currentState = CombatState.ChooseAction;
                                    break;
                            }
                            break;

                        }


                    default:
                        break;
                }
                if (card.Strength == 0)
                {
                    currentState = CombatState.CombatEnded;
                    EndCombat(card);
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


        public void EndCombat(Card card)
        {
            player.Heal(Reward);
            Console.WriteLine($"Enemy defeated - Healed by {Reward}");
            Console.WriteLine($"Player health now {Health}");
            EvTurns.LeaveCombat();

        }


    }
}
