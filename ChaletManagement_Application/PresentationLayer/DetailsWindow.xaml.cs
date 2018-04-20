//Kieran James Burns
//Window used for user interaction with all guests and extras of a set booking of a set customer decided by the passed in customer ID
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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        public DetailsWindow(int currentCustomerIDIn, int currentBookingRefIn)
        {
            InitializeComponent();
            currentCustomerID = currentCustomerIDIn;
            currentBookingRef = currentBookingRefIn;
        }

        static int currentCustomerID;
        static int currentBookingRef;

        public void PageRefresh()   //Refresh the content within the boxes and label that displays all guests and extras on the page
        {
            GuestBox.Items.Clear();

            GuestBoxHeader.Content = "Passport Numeber -  Guest Name -  Guest Age";
            List<Guest> currentGuests = MainWindow.AllCustomers.getGuests(currentCustomerID, currentBookingRef);
            foreach (var guest in currentGuests)
            {
                GuestBox.Items.Add(guest.PassportNo + "     -     " + guest.GuestName + "     -     " + guest.GuestAge);
            }

            HireBoxHeader.Content = "Hire details";
            List<HireCar> currentHire = MainWindow.AllCustomers.getHire(currentCustomerID, currentBookingRef);
            if (currentHire.Count != 0)
            {
                foreach (var hire in currentHire)
                {
                    HireBox.Items.Add("Driver Name: " + hire.DriverName);
                    HireBox.Items.Add("Car Hire Date: " + hire.HireDate.ToString("d"));
                    HireBox.Items.Add("Car Return Date: " + hire.ReturnDate.ToString("d"));
                }   
            }
            else
            {
                HireBox.Items.Add("Hire not selected");
                HireBox.Items.Add("To hire a car tick the car hire box in the extras menu");
            }

            ExtrasLabel.Content = "Additional Extras: ";
            Booking currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID, currentBookingRef);
            if (currentBooking.BreakfastExtra && currentBooking.EveningMealExtra)
            {
                ExtrasLabel.Content += ("Breakfast & Evening Meals");
            }
            else if (currentBooking.BreakfastExtra && !currentBooking.EveningMealExtra)
            {
                ExtrasLabel.Content += ("Breakfast");
            }
            else if(!currentBooking.BreakfastExtra && currentBooking.EveningMealExtra)
            {
                ExtrasLabel.Content += ("Evening Meals");
            }
            else
            {
                ExtrasLabel.Content += "None";
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Calls the appropriate classes to create a new guest object
        {
            Booking currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID, currentBookingRef);
            if (currentBooking.Guests.Count <= 6)
            {
                AddExitGuest AEG = new AddExitGuest("N", false, currentCustomerID, currentBookingRef);
                AEG.Show();
                AEG.titleLabel.Content = "New Guest";
                this.Close();
            }
            else
            {
                MessageBox.Show("The maximum guest capacity for this chalet has been reached!");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) //Calls the appropriate class to load BookingsWindow
        {
            BookingsWindow BW = new BookingsWindow(currentCustomerID);
            BW.Show();
            BW.BookingsBoxRefresh();
            this.Close();
        }

        private void amendButton_Click(object sender, RoutedEventArgs e)    //Calls the appropriate classes to modify an existing guest object
        {
            AddExitGuest AEG = new AddExitGuest("A", false, currentCustomerID, currentBookingRef);
            AEG.Show();
            AEG.titleLabel.Content = "Edit Guest";
            this.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)   //Calls the appropriate classes to delete an existing guest object
        {
            Booking currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID, currentBookingRef);
            if (currentBooking.Guests.Count > 1)
            {
                try
                {
                    String[] selectedLine = GuestBox.SelectedItem.ToString().Split(' ');
                    MessageBoxResult deleteConfirm = MessageBox.Show("Once a guest record is deleted, it's gone forever! Are you sure you want to delete this record?", "Are you sure?", MessageBoxButton.YesNo);
                    if (deleteConfirm == MessageBoxResult.Yes)
                    {
                        MainWindow.AllCustomers.deleteGuest(currentCustomerID, currentBookingRef, selectedLine[0]);
                        this.PageRefresh();
                    }
                }
                catch
                {
                    MessageBox.Show("Click on an item in the above list to select it!");
                }
            }
            else
            {
                MessageBox.Show("At least 1 guest must be part of the booking! To delete this booking go to Manage Bookings > Delete Selected Booking");
            }
        }

        private void extrasButton_Click(object sender, RoutedEventArgs e)   //Calls the appropriate classes to modify tje extras of a booking
        {
            AddEditExtras AEE = new AddEditExtras(currentCustomerID, currentBookingRef);
            AEE.Show();
            this.Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e) //Calls class to save the current data of the program to a file
        {
            SaveButtonFacade.SaveButtonPressed();
        }

        private void invoiceButton_Click(object sender, RoutedEventArgs e) //Calls the methods to open a new window to display the invoice for the selected booking
        {
            Invoice I = new Invoice();
            I.Show();
            I.fillBox(currentCustomerID, currentBookingRef);
        }
    }
}
