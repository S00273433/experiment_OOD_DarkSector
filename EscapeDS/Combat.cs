using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace EscapeDS
{
    public class Combat


        //this class became such a headache, I had to refactor it about 4 times in the last few days of the project. it runs super slowly unfortunately
    {
        public enum CombatState { ChooseAction, ReturnFire, ResolvingCombat, CombatEnded }
        private CombatState currentState = CombatState.ChooseAction;

        private Card card;
        private Player player;
        private EventsTurns EvTurns;

        private ManualResetEvent weaponChoiceEvent = new ManualResetEvent(false); //this was suggested by an AI and on stackoverflow, i've never heard of it before this, but research tells me its often used in games and complex recursives like this. Not sure if bad prtactice or not

        public Random AttackDie = new Random();
        public Random HitDie = new Random();
        public int CurrentCard { get; set; }

        public string ActionChoice;
        public string WeaponChoice;
        public int W1AttackDamage = 1;
        public int W1Ammo = 1;
        public bool W1IsJammed = false;

        public int W2AttackDamage = 2;
        public int W2Ammo = 2;
        public bool W2IsJammed = false;

        public bool IsHealUsed = false;

        // Event for combat log updates
        public event EventHandler<CombatLogEventArgs> CombatLogUpdated; //event handler to update combat info scrollviewer

        public class CombatLogEventArgs : EventArgs
        {
            public string Message { get; set; }
        }

        public Combat(Card card, Player player, EventsTurns EvTurns)
        {
            this.player = player;
            this.card = card;
            this.EvTurns = EvTurns;
            int cardID = card.CardID;
            CurrentCard = cardID;
        }

        public void StartCombat(Card newCard) // every time this is called all bools, states and weapon stats are reset
        {
            this.card = newCard;
            currentState = CombatState.ChooseAction;
            IsHealUsed = false;
            W1Ammo = 1;
            W1IsJammed = false;
            W2Ammo = 2;
            W2IsJammed = false;

            LogCombat($"--- Combat Started: {card.CardName} ---");
            LogCombat($"Enemy Strength: {card.Strength} | Enemy Range Damage: {card.RangeDamage}");
            LogCombat("Choose an action: Attack (1), Heal (2), or Reload (3)");
        }

        public void ProcessActionChoice(string action) //handles 
        {
            ActionChoice = action;
            ProcessTurn();
        }

        public void ProcessWeaponChoice(string weaponChoice) //refactor method to avoid console.wtritelines this was built on in testing stage
        {
            WeaponChoice = weaponChoice;
            weaponChoiceEvent.Set();
        }

        private void ProcessTurn() //refactor method to avoid console.wtritelines this was built on in testing stage
        {
            if (currentState != CombatState.ChooseAction)
            {
                LogCombat("Cannot perform action - combat is resolving");
                return;
            }

            switch (ActionChoice?.ToLower())
            {
                case "1": // Attack
                    HandleAttack();
                    break;
                case "2": // Heal
                    HandleHeal();
                    break;
                case "3": // Reload
                    HandleReload();
                    break;
                default:
                    LogCombat("Invalid action. Choose: Attack (1), Heal (2), or Reload (3)");
                    break;
            }

            CheckCombatEnd();
        }

        private void HandleAttack() //refactor method to avoid console.wtritelines this was built on in testing stage, many headaches but learned not to code that way ever ever again
        {
            currentState = CombatState.ResolvingCombat;
            LogCombat("You chose to ATTACK. Select weapon: 1 (Laspistol) or 2 (Battle Rifle)");

            weaponChoiceEvent.Reset();
            weaponChoiceEvent.WaitOne(5000); // 5 second timeout

            if (string.IsNullOrEmpty(WeaponChoice))
            {
                LogCombat("No weapon selected - action cancelled");
                currentState = CombatState.ChooseAction;
                return;
            }

            int damage = 0;

            switch (WeaponChoice)
            {
                case "1":
                    damage = ExecuteWeaponAttack(1);
                    break;
                case "2":
                    damage = ExecuteWeaponAttack(2);
                    break;
                default:
                    LogCombat("Invalid weapon choice");
                    break;
            }

            if (damage >= 0)
            {
                ResolveHitDie();
            }

            currentState = CombatState.ChooseAction;
        }

        private int ExecuteWeaponAttack(int weaponSlot) //refactor method to avoid console.wtritelines this was built on in testing stage. needed a bit of AI help with the logic, i dont think it did a great job
        {
            string weaponName = weaponSlot == 1 ? "Laspistol" : "Battle Rifle";
            int damage = 0;
            int ammo = weaponSlot == 1 ? W1Ammo : W2Ammo;
            bool isJammed = weaponSlot == 1 ? W1IsJammed : W2IsJammed;

            // Check ammo and jam status
            if (ammo <= 0)
            {
                LogCombat($"{weaponName} has no ammo! Choose another weapon or reload.");
                return -1;
            }

            if (isJammed)
            {
                LogCombat($"{weaponName} is jammed! You must Reload to use it again.");
                return -1;
            }

            LogCombat($"You fire your {weaponName}!");
            int hitResult = TryHitTarget(); //calls tryhit to see if your shot connects

            if (weaponSlot == 1)
            {
                switch (hitResult)
                {
                    case 1:
                        LogCombat("MISS! Your shot goes wide.");
                        damage = 0;
                        break;
                    case 2:
                        W1IsJammed = true;
                        LogCombat("The Laspistol overheats! It must be reloaded.");
                        damage = 0;
                        break;
                    case 3:
                        damage = W1AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 4:
                        damage = W1AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 5:
                        damage = W1AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 6:
                        damage = W1AttackDamage * 2;
                        this.card.TakeDamage(damage);
                        LogCombat($"*** CRITICAL HIT! *** Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                }
                W1Ammo--;
                LogCombat($"[Laspistol ammo remaining: {W1Ammo}]");
            }
            else if (weaponSlot == 2)
            {
                switch (hitResult)
                {
                    case 1:
                        LogCombat("MISS! Your shot goes wide.");
                        damage = 0;
                        break;
                    case 2:
                        W2IsJammed = true;
                        LogCombat("[WEAPON JAM!] The Battle Rifle jams! It must be reloaded.");
                        damage = 0;
                        break;
                    case 3:
                        damage = W2AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 4:
                        damage = W2AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 5:
                        damage = W2AttackDamage;
                        this.card.TakeDamage(damage);
                        LogCombat($"HIT! Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                    case 6:
                        damage = W2AttackDamage * 2;
                        this.card.TakeDamage(damage);
                        LogCombat($"*** CRITICAL HIT! *** Enemy takes {damage} damage. Enemy health: {card.Strength}");
                        break;
                }
                W2Ammo--;
                LogCombat($"[Battle Rifle ammo remaining: {W2Ammo}]");
            }

            return damage;
        }

        private void HandleHeal()
        {
            if (IsHealUsed)
            {
                LogCombat("Heal drone already used in this combat. Choose another action.");
                currentState = CombatState.ChooseAction;
                return;
            }

            int previousHealth = player.Health;
            player.Heal(1);
            LogCombat($"Your heal drone powers on for a moment. Player healed by 1. Health now {player.Health}");
            
            IsHealUsed = true;
            currentState = CombatState.ChooseAction;
        }

        private void HandleReload() //had to make the reload button reload all weapons. Super annoying otherwise
        {
            LogCombat("You RELOAD all weapons.");
            int prevW1 = W1Ammo;
            int prevW2 = W2Ammo;

            W1Ammo = 1;
            W1IsJammed = false;
            W2Ammo = 2;
            W2IsJammed = false;

            LogCombat($"[Laspistol] {prevW1} → {W1Ammo} ammo");
            LogCombat($"[Battle Rifle] {prevW2} → {W2Ammo} ammo");
            LogCombat("No return fire during reload.");
            currentState = CombatState.ChooseAction;
        }

        private void ResolveHitDie() //enemy counter attacks after every attack
        {
            currentState = CombatState.ResolvingCombat;
            int result = HitDie.Next(1, 4);

            switch (result)
            {
                case 1:
                    LogCombat("Enemy returns fire, and their shot goes wide");
                    break;
                case 2:
                    player.TakeDamage(this.card.RangeDamage);
                    LogCombat($"Enemy returns fire, HIT! Player takes {card.RangeDamage} damage. Player health: {player.Health}");
                    break;
                case 3:
                    player.TakeDamage(this.card.RangeDamage);
                    LogCombat($"Enemy return fire: HIT! Player takes {card.RangeDamage} damage. Player health: {player.Health}");
                    break;
            }
        }

        public int TryHitTarget() //rolls a dice to see if player attack hits 
        {
            int result = AttackDie.Next(1, 7);
            return result;
        }

        private void CheckCombatEnd() //checks if card health is 0, ends combat f yes
        {
            if (card.Strength <= 0)
            {
                currentState = CombatState.CombatEnded;
                EndCombat(card);
            }
        }

        public void EndCombat(Card card) //ends the combat and sets a new card
        {
            int rewardAmount = card.CardReward;
            player.Heal(rewardAmount);
            LogCombat($"*** VICTORY! ***");
            LogCombat($"Enemy defeated! You receive {rewardAmount} health reward. Player health: {player.Health}");
            LogCombat($"--- Combat Ended ---\n");
            EvTurns.LeaveCombat();
        }

        private void LogCombat(string message) //message log in the scrollviewer. ugly but needs must
        {
            CombatLogUpdated?.Invoke(this, new CombatLogEventArgs { Message = message });
        }
    }
}
