//Kieran James Burns
//Contains the list structure to be used by the Presentation Layer to store all 
//data used by the program whilst running as well as being a facade to link each 
//of the appropriate methods for adding, accessing and deleting data from the 
//Business Layer to the Presentation Layer for use by the appropriate controls 
//within the Presentation Layer.
//Last modified: 03/12/2017
//Facade Design Pattern

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    [Serializable]
    public class CustomerListFacade
    {
        //Used to store each customer.
        public List<Customer> Customers = new List<Customer>(); 

        //Method to add a new customer to the list of customers.
        public void addCustomer(Customer customer)  
        {
            Customers.Add(customer);   
        }

        //Method to find acustomer within the list of customers.
        public Customer findCustomer(int CustomerID)    
        {
            foreach (Customer cFind in Customers)
            {
                if (CustomerID == cFind.CustomerID)
                {
                    return cFind;
                }
            }
            return null;
        }

        //Method to delete a customer from the list of customers.
        public void deleteCustomer(int CustomerID)  
        {
            Customer cDel = this.findCustomer(CustomerID);
            if (cDel != null)
            {
                Customers.Remove(cDel);
            }
        }

        //Method to add a new booking to a set customer within the 
        //list of customers.
        public void addBooking(int CustomerID, Booking booking) 
        {
            Customer customer = findCustomer(CustomerID);
            customer.Bookings.Add(booking);        
        }

        //Method to find a booking of a set customer within the list 
        //of customers.
        public Booking findBooking(int CustomerID, int BookingRef) 
        {
            Customer customer = findCustomer(CustomerID);
            foreach (Booking bFind in customer.Bookings)
            {
                if (BookingRef == bFind.BookingRef)
                {
                    return bFind;
                }
            }
            return null;
        }

        //Method to delete a booking of a set customer within the 
        //list of customers. 
        public void deleteBooking(int CustomerID, int BookingRef)   
        {
            Customer customer = findCustomer(CustomerID);
            Booking bDel = this.findBooking(CustomerID, BookingRef);
            if (bDel != null)
            {
                customer.Bookings.Remove(bDel);
            }
        }

        //Method to add a new guest to a set booking of a set customer 
        //within the list of customers.
        public void addGuest(int CustomerID, int BookingRef, Guest guest)   
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            booking.Guests.Add(guest);
        }

        //Method to find a guest within a set booking of a set customer 
        //within the list of customers.
        public Guest findGuest(int CustomerID, int BookingRef, String PassportNo)   
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            foreach (Guest gFind in booking.Guests)
            {
                if (PassportNo == gFind.PassportNo)
                {
                    return gFind;
                }
            }
            return null;
        }

        //Method to delete a guest from a set booking of a set customer 
        //within the list of customers.
        public void deleteGuest(int CustomerID, int BookingRef, String PassportNo) 
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            Guest gDel = this.findGuest(CustomerID, BookingRef, PassportNo);
            if (gDel != null)
            {
                booking.Guests.Remove(gDel);
            }
        }

        //Method to add a new set of car hire details to a set booking of a set 
        //customer within the list of customers.
        public void addHire(int CustomerID, int BookingRef,HireCar hire)   
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            booking.Hires.Add(hire);
        }

        //Method to find the set of car hire details within a set booking of a
        //set customer within the list of customers.
        public HireCar findHire(int CustomerID, int BookingRef, String DriverName)  
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            foreach (HireCar hFind in booking.Hires)
            {
                if (DriverName == hFind.DriverName)
                {
                    return hFind;
                }
            }
            return null;
        }

        //Method to delete the set of car hire details from a set booking of
        //a set customer within the list of customers.
        public void deleteHire(int CustomerID, int BookingRef, String DriverName)   
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            HireCar hDel = this.findHire(CustomerID, BookingRef, DriverName);
            if (hDel != null)
            {
                booking.Hires.Remove(hDel);
            }
        }

        //Method to calculate the cost per day of a set booking of a set 
        //customer  within the list of customers for the duration of the 
        //booking.
        public Double costPerDay(int CustomerID, int BookingRef)    
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            Double perDay = 60 + (25 * booking.Guests.Count);
            return perDay;
        }

        //Method to calculate the total cost of all extras of a set booking of a 
        //set customer within the list of customers.
        public Double extrasCost(int CustomerID, int BookingRef)    
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            Double extras = 0;

            if (booking.BreakfastExtra)
                extras += (5 * booking.Guests.Count);

            if (booking.EveningMealExtra)
                extras += (10 * booking.Guests.Count);

            if (booking.Hires.Count != 0)
            {
                foreach (var hire in booking.Hires)
                {
                    Double HireDuration = (hire.ReturnDate - hire.HireDate).TotalDays;
                    extras += (HireDuration * 50);
                }
            }

            return extras;
        }

        //Method to calculate the total cost of an entire stay of a set booking
        //of a set customer within the list of customers given the cost per day 
        //and extras cost worked out prior.
        public Double calculateCost(int CustomerID, int BookingRef, Double perDay, Double extras) 
        {
            Booking booking = findBooking(CustomerID, BookingRef);

            Double stayDuration = (booking.DepartureDate - booking.ArrivalDate).TotalDays;

            Double totalCost = (perDay * stayDuration) + extras;

            return totalCost;
        }

        //Method to return a list of all bookings for a set customer within 
        //the list of customers.
        public List<Booking> getBookings(int CustomerID)    
        {
            Customer customer = findCustomer(CustomerID);
            List<Booking> returnBookings = customer.Bookings;
            return returnBookings;
        }

        //Method to return a list of all guests for a set booking of a set 
        //customer within the list of customers.
        public List<Guest> getGuests(int CustomerID, int BookingRef) 
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            List<Guest> returnGuests = booking.Guests;
            return returnGuests;
        }

        //Method to return the car hire details for a set booking of a set 
        //customer within the list of customers.
        public List<HireCar> getHire(int CustomerID, int BookingRef) 
        {
            Booking booking = findBooking(CustomerID, BookingRef);
            List<HireCar> returnHire = booking.Hires;
            return returnHire;
        }
    }
}
