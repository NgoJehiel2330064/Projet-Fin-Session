using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Simulateur
    {
        public Restaurant LeRestaurant { get; set; }

        public Simulateur(Restaurant restaurant) 
        {
            LeRestaurant = restaurant;
        }

        public void LancerSimulation()
        {

        }
    }
}
