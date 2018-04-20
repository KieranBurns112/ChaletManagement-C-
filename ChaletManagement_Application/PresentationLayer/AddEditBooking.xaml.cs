//Kieran James Burns
//Window used for user input to create or modify a booking
//Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessLayer;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for AddEditBooking.xaml
    /// </summary>
    public partial class AddEditBooking : Window
    {
        public AddEditBooking(String newOrEditIn, int passedCustomerID)
        {
            InitializeComponent();
            this.newOrEdit = newOrEditIn;
            currentCustomerID = passedCustomerID;
        }

        static int currentCustomerID;
        String newOrEdit;
        Customer currentCustomer = MainWindow.AllCustomers.findCustomer(currentCustomerID);
        Booking currentBooking = new Booking();

        public void loadData(int currentBookingRef) //Controls to load in the data of an existing booking
        {
           currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID,currentBookingRef);
           idInBox.Text = currentBooking.ChaletID.ToString();
           arrivalInBox.Text = currentBooking.ArrivalDate.ToString("d");
           departureInBox.Text = currentBooking.DepartureDate.ToString("d");
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Controls to take in, validate and add the fields to create or amend a booking upon button press
        {
            int Errors = 0;

            try
            {
                currentBooking.ChaletID = Int32.Parse(idInBox.Text);
                idError.Content = ""; 
                if (currentBooking.ChaletID > 10 || currentBooking.ChaletID < 1)
                {
                    Errors++;
                    idError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                idError.Content = "!";

            }

            try
            {
                currentBooking.ArrivalDate = DateTime.Parse(arrivalInBox.Text);
                arrivalError.Content = "";
            }
            catch
            {
                Errors++;
                arrivalError.Content = "!";
            }

            try
            {
                currentBooking.DepartureDate = DateTime.Parse(departureInBox.Text);
                departureError.Content = "";
                if (currentBooking.ArrivalDate.Date > currentBooking.DepartureDate.Date)
                {
                    Errors++;
                    departureError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                departureError.Content = "!";
            }


            if (Errors == 0)
            {
                bool hit = false;
                foreach(var thisCustomer in MainWindow.AllCustomers.Customers)
                {
                    foreach (var thisBooking in MainWindow.AllCustomers.getBookings(thisCustomer.CustomerID))
                    {
                        if (thisBooking.ChaletID == currentBooking.ChaletID)
                        {
                            if (thisBooking.ArrivalDate <= currentBooking.DepartureDate && currentBooking.ArrivalDate <= thisBooking.DepartureDate)
                            {
                                hit = true;
                            }
                        }
                    }
                }
                if(hit == true)
                {
                    Errors++;
                    idError.Content = "!";
                    MessageBox.Show("Selected Chalet is already booked for this date, try a different Chalet!");
                }
            }

            if (Errors == 0)
            {
                if (newOrEdit.Equals("N"))
                {
                    MainWindow.BookingRefGen++;
                    currentBooking.BookingRef = MainWindow.BookingRefGen;
                    currentBooking.BreakfastExtra = false;
                    currentBooking.EveningMealExtra = false;
                    MainWindow.AllCustomers.addBooking(currentCustomerID, currentBooking);
                    AddExitGuest AEG = new AddExitGuest("N", true, currentCustomerID, currentBooking.BookingRef);
                    AEG.Show();
                    this.Close();
                }
                else
                {
                    BookingsWindow BW = new BookingsWindow(currentCustomerID);
                    BW.Show();
                    BW.BookingsBoxRefresh();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("There are " + Errors + " invalid entries, they have each been marked with '!'. Please fix these then try again!");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)     //Cancel operation and return to the BookingWindow
        {
            BookingsWindow BW = new BookingsWindow(currentCustomerID);
            BW.Show();
            BW.BookingsBoxRefresh();
            this.Close();
        }
    }
}
