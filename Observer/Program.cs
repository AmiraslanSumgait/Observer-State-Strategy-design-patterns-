using System;
using System.Collections.Generic;

namespace Observer
{
    interface IRestaurant
    {
        void Update(Veggies veggies);
    }
    abstract class Veggies
    {
        private double _pricePerAzn;
        private List<IRestaurant> _restaurants = new List<IRestaurant>();

        public Veggies(double pricePerPound)
        {
            _pricePerAzn = pricePerPound;
        }

        public void Attach(IRestaurant restaurant)
        {
            _restaurants.Add(restaurant);
        }

        public void Detach(IRestaurant restaurant)
        {
            _restaurants.Remove(restaurant);
        }

        public void Notify()
        {
            foreach (IRestaurant restaurant in _restaurants)
            {
                restaurant.Update(this);
            }

            Console.WriteLine("");
        }

        public double PricePerAzn
        {
            get { return _pricePerAzn; }
            set
            {
                if (_pricePerAzn != value)
                {
                    _pricePerAzn = value;
                    Notify(); //Qiymət dəyişiklikləri barədə musterilere avtomatik olaraq xəbərdar edir
                }
            }
        }
    }
    class Carrots : Veggies
    {
        public Carrots(double price) : base(price) { }
    }
    class Restaurant : IRestaurant
    {
        private string _name;

        private double _purchaseThreshold;

        public Restaurant(string name, double purchaseThreshold)
        {
            _name = name;
            _purchaseThreshold = purchaseThreshold;
        }

        public void Update(Veggies veggie)
        {
            Console.WriteLine("Notified {0} of {1}'s "
                               + " price change to {2:C} per azn.",
                               _name, veggie.GetType().Name, veggie.PricePerAzn);
            if (veggie.PricePerAzn < _purchaseThreshold)
            {
                Console.WriteLine(_name + " wants to buy some "
                                  + veggie.GetType().Name + "!");
            }
        }
    }
    class Program
    {
        static void Main(string[] args) { 
    
            Carrots carrots = new Carrots(0.82);
            carrots.Attach(new Restaurant("Royal castle", 0.77));
            carrots.Attach(new Restaurant("Sahil restoran", 0.74));
            carrots.Attach(new Restaurant("Cafecity Ganjlik", 0.75));

            // her qiymet deyisiliyinde notify metodu vasitesi ile musterilere xeberdarliq gonderilir
            carrots.PricePerAzn = 0.79;
            carrots.PricePerAzn = 0.76;
            carrots.PricePerAzn = 0.74;
            carrots.PricePerAzn = 0.81;
        }
    }
}
