//Kieran James Burns
//Window used for user input to modify selected extras
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
    /// Interaction logic for AddEditExtras.xaml
    /// </summary>
    public partial class AddEditExtras : Window
    {
        public AddEditExtras(int currentCustomerIDIn,int currentBookingRefIn)
        {
            InitializeComponent();
            currentCustomerID = currentCustomerIDIn;
            currentBookingRef = currentBookingRefIn;
        }


        static int currentCustomerID;
        static int currentBookingRef;


        private void addButton_Click(object sender, RoutedEventArgs e)  //Controls to take in and validate the fields to change selected extras upon button press
        {      
            Booking currentBooking = MainWindow.AllCustomers.findBooking(currentCustomerID, currentBookingRef);

            if (breakfastBox.IsChecked == true)
            {
                currentBooking.BreakfastExtra = true;
            }
            else
            {
                currentBooking.BreakfastExtra = false;
            }

            if (dinnerBox.IsChecked == true)
            {
                currentBooking.EveningMealExtra = true;
            }
            else
            {
                currentBooking.EveningMealExtra = false;
            }
            
            if (carHireBox.IsChecked == true && currentBooking.Hires.Count == 0)
            {
                addCarHire ACH = new addCarHire("N", currentCustomerID, currentBookingRef);
                ACH.Show();
                this.Close();
            }
            else if (carHireBox.IsChecked == true && currentBooking.Hires.Count != 0)
            {
                addCarHire ACH = new addCarHire("A", currentCustomerID, currentBookingRef);
                ACH.Show();
                ACH.loadData();
                this.Close();
            }
            else if (carHireBox.IsChecked == false && currentBooking.Hires.Count != 0)
            {
                currentBooking.Hires.Clear();
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
