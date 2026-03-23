using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EscapeDS
{
    public class Combat 
    {
        public enum CombatState { ChooseAction, ReturnFire, ResolvingCombat } //different stages for each turn
        //class that runs the combat encounters in the game. pulls stats from whatever card is currently active in the databse. 

        //needs turns
        //1. select action - shoot, reload, heal drone, flank
        //2. resolve chosen action. 
        //3. shoot - if shoot, roll dice and apply result - hits, jams, and misses. Then, roll enemy Hit Die, resolve result. Damage on hits is stored in Cards database
        //4. Compare rolled hits with enemy Strength, which is stored in Cards database. minus-- strength with every hit, when strength is 0, enemy killed, receive rewards.
        
        Card card;
        Player Player;

        int Strength;
        int RangedDamage;

        public Random AttackDie = new Random();
        public Random HitDie = new Random();
        public int CurrentCard { get; set; }
        
        public Combat()
        {
            int cardID = this.card.CardID;
            CurrentCard = cardID;
            Strength = this.card.Strength;
            

            //Compare the card's strentgh against the payer's dice roll every turn.
            //when strength is reduced to 0, the player wins and gets the reward.

            //Then, roll car's hit dice. if hit, reduce player health. 0 = game over.
        }
        

        public string ActionChoice;
        public string WeaponChoice;
        public int W1AttackDamage = 2;
        public int W1Ammo = 2;

        public int W2AttackDamage = 1;
        public int W2Ammo = 1;
        
        
        public void DoCombat(Player player, Card card, string ActionChoice)
        {
            
            Console.WriteLine("Choose action: 1) Attack \n2)Flank \n3) Heal\n Reload");
            ActionChoice = Console.ReadLine().ToLower();

            switch (ActionChoice)
            {
                case "1":
                {
                        int damage;
                        Console.WriteLine("Attack! \nSelect which weapon slot to use. \n1) ");
                        WeaponChoice = Console.ReadLine().ToLower();
                        
                        switch (WeaponChoice)
                        {
                            case "1":
                                {
                                    if (W1Ammo > 0)
                                    {
                                        damage = W1AttackDamage;
                                        this.card.Strength -= damage;
                                        TargetHit();
                                    }
                                    break; 
                                }

                            case "2":
                                {
                                    if (W2Ammo > 0)
                                    {
                                        damage = W2AttackDamage;
                                        this.card.Strength -= damage;
                                        TargetHit();
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

        public int TargetHit()
        {
            int result;
            result = AttackDie.Next(1, 6);
            switch (result)
            {
                case 1:
                    {

                        break;
                    }
            
            }


            return 0; //just to clear error
        }


    }
}
