//Kieran James Burns
//Contains each of the constructors for each of the propertes to 
//be used within each bookings hire car extra if it is selected.
//Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public class HireCar
    {
        private String _driverName;
        private DateTime _hireDate;
        private DateTime _returnDate;

        //Used to handle the driver name assigned to each set 
        //of car hire details.
        public String DriverName   
        {
            get
            {
                return _driverName;
            }

            set
            {
                _driverName = value;
            }
        }

        //Used to handle the date of hire assigned to each set 
        //of car hire details.
        public DateTime HireDate    
        {
            get
            {
                return _hireDate;
            }

            set
            {
                _hireDate = value;
            }
        }

        //Used to handle the date of return assigned to each 
        //set of car hire details.
        public DateTime ReturnDate  
        {
            get
            {
                return _returnDate;
            }

            set
            {
                _returnDate = value;
            }
        }
    }
}
