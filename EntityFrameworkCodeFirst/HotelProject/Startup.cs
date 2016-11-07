using System;
using HotelProject.Models;

namespace HotelProject
{
    public class Startup
    {
        static void Main()
        {
            HotelContext context=new HotelContext();
            Occupancies occupancies = new Occupancies
            {
                PhoneCharge = false
            };
        }
    }
}
