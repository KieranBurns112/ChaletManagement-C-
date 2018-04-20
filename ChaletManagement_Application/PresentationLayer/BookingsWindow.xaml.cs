//Kieran James Burns
//Window used for user interaction with all bookings of a set customer decided by the passed in customer ID
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
    /// Interaction logic for BookingsWindow.xaml
    /// </summary>
    public partial class BookingsWindow : Window
    {
        public BookingsWindow(int currentCustomerIDIn)
        {
            InitializeComponent();
            currentCustomerID = currentCustomerIDIn;
        }

        static int currentCustomerID;

        public void BookingsBoxRefresh()    //Refresh the content within the box that displays all bookings on the page
        {
            BookingBox.Items.Clear();
            List<Booking> currentBookings = MainWindow.AllCustomers.getBookings(currentCustomerID);
            if (currentBookings.Count == 0)
            {
                BoxHeader.Content = "No Bookings currently available";
            }
            else
            {
                BoxHeader.Content = "Booking Reference  -  Chalet ID  -  Arrival date  -  Departure Date";
                foreach (var booking in currentBookings)
                {
                    BookingBox.Items.Add(booking.BookingRef + "     -     " + booking.ChaletID + "     -     " + booking.ArrivalDate.ToString("d") + "     -     " + booking.DepartureDate.ToString("d"));
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Calls the appropriate classes to create a new booking object
        {
            AddEditBooking AEB = new AddEditBooking("N", currentCustomerID);
            AEB.Show();
            AEB.titleLabel.Content = "New Booking";
            Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e) //Calls the appropriate class to load CustomerWindow
        {
            CustomerWindow CW = new CustomerWindow();
            CW.Show();
            CW.customerBoxRefresh();
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e) //Calls class to save the current data of the program to a file
        {
            SaveButtonFacade.SaveButtonPressed();
        }

        private void amendButton_Click(object sender, RoutedEventArgs e)    //Calls the appropriate classes to modify an existing booking object
        {
            try
            {
                String[] selectedLine = BookingBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                AddEditBooking AEB = new AddEditBooking("A", currentCustomerID);
                AEB.Show();
                AEB.loadData(selectedInt);
                AEB.titleLabel.Content = "Edit Booking";
                Close();
            }
            catch
            {
                MessageBox.Show("Click on an item in the above list to select it!");
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)   //Calls the appropriate classes to delete an existing booking object
        {
            try
            {
                String[] selectedLine = BookingBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                MessageBoxResult deleteConfirm = MessageBox.Show("Once a Booking is deleted, it's gone forever! Are you sure you want to delete this record?", "Are you sure?", MessageBoxButton.YesNo);
                if (deleteConfirm == MessageBoxResult.Yes)
                {
                    MainWindow.AllCustomers.deleteBooking(currentCustomerID,selectedInt);
                    this.BookingsBoxRefresh();
                }
            }
            catch
            {
                MessageBox.Show("Click on an item in the above list to select it!");
            }
        }

        private void detailsButton_Click(object sender, RoutedEventArgs e)  //Calls the appropriate class to load DetailsWindow
        {
            try
            {
                String[] selectedLine = BookingBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                DetailsWindow DW = new DetailsWindow(currentCustomerID, selectedInt);
                DW.Show();
                DW.PageRefresh();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Click on an item in the above list to select it!");
            }
        }
    }
}
