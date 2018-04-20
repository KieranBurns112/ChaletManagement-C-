//Kieran James Burns
//Contains each of the constructors for each of the propertes to be used within 
//each Booking.
//Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public class Booking
    {
        private int _bookingRef;
        private int _chaletId;
        private DateTime _arrivalDate;
        private DateTime _departureDate;
        private Boolean _breakfastExtra;
        private Boolean _eveningMealExtra;

        //Used to store each guest assigned to a booking
        public List<Guest> Guests = new List<Guest>();

        //Used to store all of the hire car data assigned to a booking.
        public List<HireCar> Hires = new List<HireCar>();

        //Used to handle the booking reference assigned to each booking.
        public int BookingRef     
        {
            get
            {
                return _bookingRef;
            }

            set
            {
                _bookingRef = value;
            }
        }
        
        //Used to handle the chalet ID assigned to each booking.
        public int ChaletID     
        {
            get
            {
                return _chaletId;
            }

            set
            {
                _chaletId = value;
            }
        }

        //Used to handle the arrival date assigned to each booking.
        public DateTime ArrivalDate     
        {
            get
            {
                return _arrivalDate;
            }

            set
            {
                _arrivalDate = value;
            }
        }

        //Used to handle the departure assigned to each booking.
        public DateTime DepartureDate    
        {
            get
            {
                return _departureDate;
            }

            set
            {
                _departureDate = value;
            }
        }

        //Used to handle the boolean value dictating if a booking 
        //contains the breakfast extra for each booking.
        public Boolean BreakfastExtra   
        {
            get
            {
                return _breakfastExtra;
            }

            set
            {
                _breakfastExtra = value;
            }
        }

        //Used to handle the boolean value dictating if a booking 
        //contains the evening meal extra for each booking.
        public Boolean EveningMealExtra     
        {
            get
            {
                return _eveningMealExtra;
            }

            set
            {
                _eveningMealExtra = value;
            }
        }
    }
}
