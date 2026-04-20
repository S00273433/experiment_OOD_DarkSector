using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeDS
{
    public class Player
    {
        private int health = 12;
        public bool InCombat { get; set; }

        public event EventHandler HealthChanged; // event handler to track health and display in mainwindow player health text block

        public int Health
        {
            set
            {
                if (value < 0)
                {
                    health = 0; // Ensure health does not go below 0
                }
                else if (value > 12)
                {
                    health = 12; // Ensure health does not exceed 12
                }
                else
                {
                    health = value;
                }
                HealthChanged?.Invoke(this, EventArgs.Empty);
            }
            get { return health; }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        public void Heal(int amount)
        {
            Health += amount;
            HealthChanged?.Invoke(this, EventArgs.Empty);
        }

        public Player()
        {

        }
    }
    public class Inventory
    {
        public List<Inventory> Items {  get; set; }
        
        public Inventory()
        {
            Items = new List<Inventory>();
            Items.Add(new Inventory());
        }

        public void AddToInventory(Inventory item)
        {
            Items.Add(item);
        }
    }

    

}
