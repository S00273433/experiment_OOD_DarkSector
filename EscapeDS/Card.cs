using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EscapeDS
{


    public enum CardType { StartCard, Act1, Act2, Act3, Boss }
    public enum DamageWeakness { Ballistic, Energy, Exposive }
    public enum WeaponType { Ballistic, Energy, Explosive }

    public class Card
    {



        //chapter cards for DarkSector

        public int CardID { get; set; }
        public string CardName { get; set; }
        public string CardDescription { get; set; }

        public string Image { get; set; }

        public CardType TypeOfCard { get; set; }

        public int RangeDamage { get; set; }
        public int MeleeDamage { get; set; } //may cut out
        public int Strength { get; set; } //same as Health 
        public int CardReward { get; set; }
        public int CardPenalty { get; set; }

        public bool IsActive { get; set; }


        public event EventHandler StrengthChanged;

        public void TakeDamage(int amount)
        {
            Strength -= amount;
            StrengthChanged?.Invoke(this, EventArgs.Empty);
        }



        public override string ToString()
        {
            return $"{CardID} {CardName} \n{CardDescription}";
        }

    }

    public class CardData : DbContext
    {
        public CardData() : base("CardDataMar26") { }
        public DbSet<Card> Card { get; set; }


    }


    public class Deck
    {
        private CardData db = new CardData();

        public List<Card> AllCards { get; set; }
        public List<Card> ShuffledCards { get; set; }


        //ctor which creates the deck
        public Deck()
        {
            #region old data
            /*//Generate all cards, 3 (start) + (8 x 3 story cards by act 1 2 and 3) + 3(boss cards) -  (30 in total)

            //Create 3 start cards
            Card sc1 = new Card() { CardID = 1, TypeOfCard = CardType.StartCard, CardName = "Detention", CardDescription = "You awake in a small, dark cell. The inobservant gaurds missed your suit's hidden compartment, where you store a small laser cutter. You melt through the iron bars of the cell and escape, to find the jail otherwise empty. A laspistol lays on the gaurd's desk." };  // start weapon + 1 item card
            Card sc2 = new Card() { CardID = 2, TypeOfCard = CardType.StartCard, CardName = "Security", CardDescription = "You've been seized by gaurds on suspected sabotage charges. They brought you to the detention level, and became distracted when a fight breaks out nearby, which boils over into a class riot. You run for your life, and find a laspistol during the fray." }; //start weapon -  1 act 3 card
            Card sc3 = new Card() { CardID = 3, TypeOfCard = CardType.StartCard, CardName = "The Brig", CardDescription = "You enter the station a slave, property of a depraved slaver. A moment of quick thinking and impactful violence finds you freed, but confused. You begin to search for a way off the ship, slaver's own gun in hand." }; //start weapon + 1 act 3 card

            //Create 8 act 1 cards
            Card a1 = new Card() { CardID = 4, TypeOfCard = CardType.Act1, CardPenalty = -2, CardReward = 2, CardName = "The Assasin 1", CardDescription = "From the darkness ahead, you hear a series of rapid, popping sounds and a flurry of bullets whistle past your ear. Before you can react, there is another burst of fire and a bullet slams into your shoulder, knocking you down.\n\nYOU lose 2HP\n\nWhen silence falls, you edge forward to find the body of a man - his fatal wounds still smouldering. A contract has been fulfilled, a bounty no doubt soon to be collected. There is no sign of the assassin, so you lead the crew on a quick search for items of value.\n\n+2 ITEM Cards" };
            Card a2 = new Card() { CardID = 5, TypeOfCard = CardType.Act1, CardPenalty = -2, CardReward = 1, CardName = "Contraband", CardDescription = "This room is lined with maglocked deposit boxes, filled with contraband confiscated from prisoners.\n Choose one option:\nMove On: Time is of the essence - skip chapter.\nHack: Attempt to hack the twrminal to open some boxes. You must roll WISDOM in one attempt, or lose 2HP. Can be tried 3 times. Success: Obtain an item card." }; //build this out
            Card a3 = new Card() { CardID = 6, TypeOfCard = CardType.Act1, Strength = 6, CardReward = 2, RangeDamage = 1, MeleeDamage = 1, CardName = "Cyborg Gaurd", CardDescription = "The battle-broken limbs of this cyborg guard hiss and whine as it falters towards you, a singular directive still coursing though its fried circuits, 'Surrender. Or. Die.'\n\nChoose one option: \nAttack: Begin Combat.\nEvade: Scramble to run away - the gaurd unleases a volley of lasfire. YOU must roll two hit dice" }; //build out hit dice and combat system
            Card a4 = new Card() { CardID = 7, TypeOfCard = CardType.Act1, CardPenalty = -1, CardName = "Vents", CardDescription = "Desperate to evade detection, you crawl into the foul-smelling ventilation ducts. Within, a deep, rhythmic, throbbing sound is punctuated by the metallic scuttling of unseen creatures... \r\n\r\nNominate one member of the crew to navigate this narrow, branching crawlspace. They must roll two WISDOM in three attempts. \r\n\r\nAfter each roll, all other members of the crew lose 1HP from claustrophobia unless the total has been reached. If the total has not been reached after three attempts, the correct route is found by chance and you all emerge exhausted and shaken, forever haunted by this harrowing experience." };
            Card a5 = new Card() { CardID = 8, TypeOfCard = CardType.Act1, CardPenalty = -2, CardName = "LaserGrid", CardDescription = "The way forward is blocked by an ever-shifting lasergrid - a perplexing pattern of buzzing, burning beams. \r\n\r\nEach member of the crew must choose one of the following methods to try and get through without touching the beams: \r\n\r\nSpeed: Roll CUNNING in one attempt. \r\n\r\nPatience: Roll two WISDOM in three attempts. \r\n\r\nApply these results to each member of the crew individually... \r\n\r\nSuccess: You make it through without injury. \r\n\r\nFailure: You stumble and are badly burned. Lose 2HP. " };
            Card a6 = new Card() { CardID = 9, TypeOfCard = CardType.Act1, CardReward = 2, Strength = 6, MeleeDamage = 2, RangeDamage = 1, CardName = "Engineer", CardDescription = "You pass a room where an engineer is repairing droids. She is so absorbed in the work that you spot an opportunity to steal from her... \r\n\r\nAs a group, choose one option... \r\n\r\nMove On: Why endanger the mission? Turn the next chapter card. \r\n\r\nSteal: Nominate a member of the crew to try and roll CUNNING in one attempt... \r\n\r\nSuccess: You deftly retrieve what you can. Draw two ITEM cards. \r\n\r\nFailure: You fall loudly into the room. Begin combat... " };
            Card a7 = new Card() { CardID = 10, TypeOfCard = CardType.Act1, CardName = "Traveller", Strength = 6, CardReward = 3, CardDescription = "Blasting through his cell wall from the inside, a mysterious traveller steps out into the passage ahead. His arm-cannon crackles with energy as he calmly scans you, evidently assessing your threat level... \r\n\r\nAs a group, choose one option... \r\n\r\nEvade: Make a peace offering. Discard an ITEM and then turn the next chapter card. \r\n\r\nAttack: Take him down! Begin combat... " }; //if evade, remove an item from the inventory and turn the next card. If attack, begin combat
            Card a8 = new Card() { CardID = 11, TypeOfCard = CardType.Act1, CardName = "Barracks", CardReward = 1, CardPenalty = 2, CardDescription = "A wrong turn leads you right into the guard barracks — row upon row of slumbering security personnel. Fortunately for you, a lone guard is on watch duty. She sees you. You both freeze. \r\n\r\nYou must make a choice... \r\n\r\nRun: You roll for cover and sprint into the darkness, but the guard gets off one shot before you vanish. You must roll a HIT DIE. \r\n\r\nSteal and run: You swipe something from the nearest berth, then run for it. You must roll two HIT DICE, then draw an ITEM card. " }; //roll one hit die, or roll 2 and get an item. 

            //Create 8 act 2 cards
            Card a9 = new Card() { CardID = 12, TypeOfCard = CardType.Act2, CardPenalty = -2, CardReward = 2, CardName = "Repair Bot", CardDescription = "In the darkness ahead, the familiar clicking and whirring sound of a repair-bot is suddenly shattered by the roar of an explosion.\nThe passage is filled with light and you see the robot thrown into the air, disintegrating as the force of the blast sends shards of white-hot metal flying towards you. \r\n\r\nYOU lose 2HP. When the debris settles, you approach the machine the droid was working on - a replicator. Among the wreckage of its smouldering innards, two of its recent creations miraculously remain intact. Draw two ITEM cards." };
            Card a10 = new Card() { CardID = 13, TypeOfCard = CardType.Act2, CardPenalty = -1, CardReward = 1, CardName = "Warp Cell", CardDescription = "You hurry on, passing through the engineering deck where a neglected warp cell is in the final stages of destabilisation. It pulses erratically, the overloaded core throwing out crackling tendrils of irradiated energy. \r\n\r\nAll members of the crew immediately lose 1hp from exposure. Then, make a choice... \r\n\r\nGo left: Shielding your eyes, you exit swiftly and unharmed. \r\n\r\nGo right: You dash into the danger zone to raid the supplies, suffering severe burns in the process. Lose an additional 1HP then draw an item card." }; //-1HP extra if right
            Card a11 = new Card() { CardID = 14, TypeOfCard = CardType.Act1, CardReward = 2, Strength = 4, MeleeDamage = 2, RangeDamage = 2, CardName = "Mercs", CardDescription = "Passing through the leisure zone, a crew of mercs block your way, intent on starting trouble. You'll have to fight them one-on-one..." };
            Card a12 = new Card() { CardID = 15, TypeOfCard = CardType.Act2, CardPenalty = 1, CardName = "Trash Compactor", CardDescription = "Leading the crew swiftly onward, your boot catches a concealed switch You turn in despair to see your crewmates disappear through a fals floor behind you, plummeting into a foul-smelling trash compacto below. A moment later, there is the sound of heavy machinery and the walls start closing in... \r\n\r\nTo hack the controls and disable the crusher, you must roll three WISDOM in four attempts. After each attempt, if the total has not beer reached all other members of the crew lose Ip unless they can rol MIGHT in one attempt to brace the crusher. If you have not succeeded after four attempts, you finally manage to deactivate the system by chance and drag your fellow crewmembers out. " }; //WISDOM AND MIGHT rolls
            Card a13 = new Card() { CardID = 16, TypeOfCard = CardType.Act2, CardReward = 2, Strength = 5, RangeDamage = 2, MeleeDamage = 2, CardName = "LizardMen", CardDescription = "Here, two lizardmen are brutally interrogating a quivering captive. \r\n\r\nChoose one option... \r\n\r\nAttack: Give these bullies what they deserve. Begin combat... \r\n\r\nIntimidate: Try and speak their language, ordering them to set the man free by rolling WISDOM in one attempt... \r\n\r\nSuccess: They obey, and the man rewards you. Draw an ITEM card. \r\n\r\nFailure: They laugh, drawing their weapons and vanishing behind a volley of suppressive fire. YOU must roll a HIT DIE. Then, turn the next chapter card. " };
            Card a14 = new Card() { CardID = 17, TypeOfCard = CardType.Act2, CardReward = 2, Strength = 5, MeleeDamage = 1, RangeDamage = 2, CardName = "The Assassin 2", CardDescription = "There is a blinding flash as the fabric of spacetime is torn apart before your very eyes. An assassin bursts through the unstable portal and instantly opens fire! \r\n\r\nYOU must roll a HIT DIE. Then, choose one option... \r\n\r\nEvade: You rush forward as a unit, forcing the assassin back into the portal just as it closes. All members of the crew lose 1HP. \r\n\r\nAttack: You take up defensive positions. Begin combat... \r\n\r\n " };
            Card a15 = new Card() { CardID = 18, TypeOfCard = CardType.Act1, CardReward = 2, CardPenalty = -3, CardName = "Dissection", CardDescription = "You pass a medical bay where someone strangely familiar is being clumsily dissected. Peering in, you notice some unattended supplies... \r\n\r\nChoose one option... \r\n\r\nMove on: It's too risky. Turn the next chapter card. \r\n\r\nSteal: Nominate a member of the crew to roll two CUNNING in two attempts... \r\n\r\nSuccess: You deftly slip inside, carefully retrieving the items within reach. Draw two ITEM cards. \r\n\r\nFailure: You stumble and overturn a large vial of acid, spilling it down your legs. You lose 3up. Barely stifling your cries of agony, you withdraw just as your presence becomes noticed... " };
            Card a16 = new Card() { CardID = 19, TypeOfCard = CardType.Act1, Strength = 6, CardReward = 3, CardName = "Nobleman", CardDescription = "" };

            //Create 8 act 3 cards
            Card a17 = new Card() { CardID = 20, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a18 = new Card() { CardID = 21, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a19 = new Card() { CardID = 22, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a20 = new Card() { CardID = 23, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a21 = new Card() { CardID = 24, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a22 = new Card() { CardID = 25, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a23 = new Card() { CardID = 26, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a24 = new Card() { CardID = 27, TypeOfCard = CardType.Act3, CardName = "def" };
            Card a25 = new Card() { CardID = 28, TypeOfCard = CardType.Act1, CardName = "xyz" };

            //Create 3 boss cards
            Card b1 = new Card() { CardID = 28, TypeOfCard = CardType.Boss, CardName = "boss1" };


            //AllCards = new List<Card>() { sc1, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21, a22, a23, a24, a25, b1 };*/
           
            #endregion

            var query = from card in db.Card
                        select card;

            var results = query.ToList();

            AllCards = results;

        }

        //deal card method which will get the top card
        public Card DealCard(CardType cardType)
        {
            Card cardToDeal;
            ShuffleCards(); // Shuffle the deck before dealing a card
            foreach (Card card in AllCards)
            {
                if (card.TypeOfCard == cardType)
                {
                    cardToDeal = card;
                    AllCards.Remove(card);
                    return cardToDeal;
                }
            }

            return null; // Return null if no card of the specified type is found
        }

        public Card ShuffleCards()
        {

            Random rand = new Random();
            int n = AllCards.Count;
            while (n > 0)
            {
                n--;
                int k = rand.Next(n + 1);
                Card value = AllCards[k];
                AllCards[k] = AllCards[n];
                AllCards[n] = value;
            }
            ShuffledCards = AllCards;
            return null;
        }



    }





    public class BossCard : Card //how do i add these to the database?
    {
        public int BossID { get; set; }
        public string BossName { get; set; }
        public string BossImage { get; set; }
        public int BossStrength;
    }

    public class StartCard : Card //how do i add these to the database?
    {
        public int StartID { get; set; }
        public string StartName { get; set; }
        public string StartImage { get; set; }
        public string StartReward { get; set; }

    }


}

