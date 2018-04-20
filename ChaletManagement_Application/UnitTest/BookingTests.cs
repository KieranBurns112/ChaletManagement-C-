//Kieran James Burns
//Contains each of the test methods for the functions in the Business layer 
//relating to the bookings class.
//Last modified: 04/12/2017

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLayer;

namespace UnitTest
{
    [TestClass]
    public class BookingTests
    {
        Customer TestCustomer = new Customer() { CustomerID = 1, CustomerName = "Billy Test", CustomerAddress = "123 Test Street" };
        Booking TestBooking = new Booking() { BookingRef = 1, ChaletID = 1, ArrivalDate = DateTime.Parse("10/12/2018"), DepartureDate = DateTime.Parse("17/12/2018")};

        [TestMethod]
        public void addBooking_test()
        {
            //Set up the class list to have the test added to.
            CustomerListFacade TestCustomers = new CustomerListFacade();
            TestCustomers.addCustomer(TestCustomer);
                    
            //Create the test booking and attempts to add it to the class list.
            TestCustomers.addBooking(1, TestBooking);   
            Boolean result = false;
            //If test booking is successfully added to the class list then set 
            //result to true true, otherwise leave it as false.
            if (TestCustomer.Bookings.Count > 0) 
            {
                result = true;
            }

            //If result is true, pass the test, otherwise fail it.
            Assert.IsTrue(result);  
        }

        [TestMethod]
        public void findBooking_test()
        {
            //Set up the class list to be searched and adds the data to be found.
            CustomerListFacade TestCustomers = new CustomerListFacade();
            TestCustomers.addCustomer(TestCustomer);        
            TestCustomers.addBooking(1, TestBooking);   

            Booking outputBooking = TestCustomers.findBooking(1, 1);
            Boolean result = false;
            //If test booking is successfully found then outputBooking.ChaletID 
            //should = 1, if so, set result to true, otherwise leave it as false.
            if (outputBooking.ChaletID == 1) 
            {
                result = true;
            }

            //If result is true, pass the test, otherwise fail it.
            Assert.IsTrue(result);  
        }

        [TestMethod]
        public void deleteBooking_test()
        {
            //Set up the class list to be searched and adds the data to be found.
            CustomerListFacade TestCustomers = new CustomerListFacade();
            TestCustomers.addCustomer(TestCustomer);
            TestCustomers.addBooking(1, TestBooking);   

            TestCustomers.deleteBooking(1, 1);
            Boolean result = false;
            //If the test booking added to the class list is deleted, set result
            //to true, otherwise leave it as false.
            if (TestCustomer.Bookings.Count == 0) 
            {
                result = true;
            }
    
            //If result is true, pass the test, otherwise fail it.
            Assert.IsTrue(result);  
        }

        [TestMethod]
        public void getBookings_test()
        {
            //Set up the class list to be searched and adds the data to be found.
            CustomerListFacade TestCustomers = new CustomerListFacade();
            TestCustomers.addCustomer(TestCustomer);
            TestCustomers.addBooking(1, TestBooking);
            Booking TestBooking2 = new Booking() { BookingRef = 2, ChaletID = 3, ArrivalDate = DateTime.Parse("21/02/2019"), DepartureDate = DateTime.Parse("28/02/2019")};
            TestCustomers.addBooking(1, TestBooking2);  

            var TestBookings  = TestCustomers.GetBookings(1);
            Boolean result = false;
            //If the all test bookings are added to list TestBookings then the 
            //result to true, otherwise leave it as false.
            if (TestBookings.Count == 2) 
            {
                result = true;
            }
            
            //If result is true, pass the test, otherwise fail it.
            Assert.IsTrue(result);  
        }
    }
}
