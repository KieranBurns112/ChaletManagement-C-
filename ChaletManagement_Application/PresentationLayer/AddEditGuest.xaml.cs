//Kieran James Burns
//Window used for user input to create or modify a guest
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
    /// Interaction logic for AddExitGuest.xaml
    /// </summary>
    public partial class AddExitGuest : Window
    {
        public AddExitGuest(String newOrEditIn, Boolean newBookingIn, int passedCustomerID, int passedBookingRef)
        {
            InitializeComponent();
            this.newOrEdit = newOrEditIn;
            this.currentCustomerID = passedCustomerID;
            this.currentBookingRef = passedBookingRef;
            this.newBooking = newBookingIn;
        }

        int currentCustomerID;
        int currentBookingRef;
        String newOrEdit;
        Boolean newBooking;

        Guest currentGuest = new Guest();
        public void loadData(String currentPassportNo)  //Controls to load in the data of an existing guest
        {
            currentGuest = MainWindow.AllCustomers.findGuest(currentCustomerID,currentBookingRef,currentPassportNo);
            nameInBox.Text = currentGuest.GuestName;
            passNoInBox.Text = currentGuest.PassportNo;
            ageInBox.Text = currentGuest.GuestAge.ToString();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Controls to take in, validate and add the fields to create or amend a guest upon button press
        {
            int Errors = 0;

            try
            {
                currentGuest.GuestName = nameInBox.Text;
                nameError.Content = "";
                if (currentGuest.GuestName.Equals("")) 
                {
                    Errors++;
                    nameError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                nameError.Content = "!";

            }

            try
            {
                currentGuest.PassportNo = passNoInBox.Text;
                passNoError.Content = "";
                if (currentGuest.PassportNo.Equals("") || currentGuest.PassportNo.Length > 10 || currentGuest.PassportNo.Contains(" "))
                {
                    Errors++;
                    passNoError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                passNoError.Content = "!";
            }

            try
            {
                currentGuest.GuestAge = Int32.Parse(ageInBox.Text);
                ageError.Content = "";
                if (currentGuest.GuestAge > 101)
                {
                    Errors++;
                    ageError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                ageError.Content = "!";
            }

            if (Errors == 0)
            {
                if (newOrEdit.Equals("N"))
                {
                    MainWindow.AllCustomers.addGuest(currentCustomerID, currentBookingRef, currentGuest);
                    Booking currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID, currentBookingRef);
                    if (currentBooking.Guests.Count <= 6)
                    {
                        MessageBoxResult makeNewBooking = MessageBox.Show("Would you like to add another Guest? This can also be done from Manage Booking Details > Add New Guest.", "Add another Guest?", MessageBoxButton.YesNo);
                        if (makeNewBooking == MessageBoxResult.Yes)
                        {
                            AddExitGuest AEG = new AddExitGuest("N", true, currentCustomerID, currentBookingRef);
                            AEG.Show();
                            this.Close();
                        }
                        else if (newBooking == true)
                        {
                            AddEditExtras AEE = new AddEditExtras(currentCustomerID, currentBookingRef);
                            AEE.Show();
                            this.Close();
                        }
                        else
                        {
                            DetailsWindow DW = new DetailsWindow(currentCustomerID, currentBookingRef);
                            DW.Show();
                            DW.PageRefresh();
                            this.Close();
                        }
                    }
                    else if (newBooking == true)
                    {
                        AddEditExtras AEE = new AddEditExtras(currentCustomerID, currentBookingRef);
                        AEE.Show();
                        this.Close();
                    }
                    else
                    {
                        DetailsWindow DW = new DetailsWindow(currentCustomerID, currentBookingRef);
                        DW.Show();
                        DW.PageRefresh();
                        this.Close();
                    }
                }
                else
                {
                    DetailsWindow DW = new DetailsWindow(currentCustomerID, currentBookingRef);
                    DW.Show();
                    DW.PageRefresh();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("There are " + Errors + " invalid entries, they have each been marked with '!'. Please fix these then try again!");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)   //Cancel operation if at least 1 guest exists then open the appropriate window
        {
            List<Guest> currentGuests = MainWindow.AllCustomers.getGuests(currentCustomerID, currentBookingRef);
            if (currentGuests.Count == 0)
            {
                MessageBox.Show("Cannot cancel, at least 1 guest must be added to a booking!");
            }
            else
            {
                if (newBooking == true)
                {
                    AddEditExtras AEE = new AddEditExtras(currentCustomerID, currentBookingRef);
                    AEE.Show();
                    this.Close();
                }
                else
                {
                    DetailsWindow DW = new DetailsWindow(currentCustomerID, currentBookingRef);
                    DW.Show();
                    DW.PageRefresh();
                    this.Close();
                }
            }
        }
    }
}
