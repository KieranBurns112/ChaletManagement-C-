//Kieran James Burns
//Contains each of the constructors for each of the propertes to be used within 
//each Customer.
//Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public class Customer
    {
        private String _customerName;
        private String _customerAddress;
        private int _customerId;

        //Used to store each booking assigned to a customer
        public List<Booking> Bookings = new List<Booking>();

        //Used to handle the customer name assigned to each customer
        public String CustomerName      
        {
            get
            {
                return _customerName;
            }

            set
            {
                _customerName = value;
            }
        }

        //Used to handle the customer address assigned to each customer
        public String CustomerAddress   
        {
            get
            {
                return _customerAddress;
            }

            set
            {
                _customerAddress = value;
            }
        }

        //Used to handle the customer ID assigned to each customer
        public int CustomerID      
        {
            get
            {
                return _customerId;
            }

            set
            {
                _customerId = value;
            }
        }

    }
}
