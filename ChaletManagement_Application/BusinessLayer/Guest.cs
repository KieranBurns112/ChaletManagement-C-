//Kieran James Burns
//Contains each of the constructors for each of the propertes to be used within 
//each Guest.
//Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public class Guest
    {
        private String _guestName;
        private int _guestAge;
        private String _passportNo;

        //Used to handle the guest name assigned to each guest.
        public String GuestName 
        {
            get
            {
                return _guestName;
            }

            set
            {
                _guestName = value;
            }
        }

        //Used to handle the guest age assigned to each guest.
        public int GuestAge
        {
            get
            {
                return _guestAge;
            }

            set
            {
                _guestAge = value;
            }
        }

        //Used to handle the passport number assigned to each guest.
        public String PassportNo    
        {
            get
            {
                return _passportNo;
            }

            set
            {
                _passportNo = value;
            }
        }
    }
}
