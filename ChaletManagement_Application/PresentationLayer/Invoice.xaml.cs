//Kieran James Burns
//Window used to display a generated invoice of a set booking of a set customer, given a passed in customer ID and Booking Reference
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
    /// Interaction logic for Invoice.xaml
    /// </summary>
    public partial class Invoice : Window
    {
        public Invoice()
        {
            InitializeComponent();
        }


        public void fillBox(int currentCustomerID, int currentBookingRef)   //Fills the box with the details of the invoice
        {
            InvoiceBox.Items.Clear();
            Double dailyCost = MainWindow.AllCustomers.costPerDay(currentCustomerID, currentBookingRef);
            InvoiceBox.Items.Add("Basic cost: £" + dailyCost + " per night.");

            Double extrasCost = MainWindow.AllCustomers.extrasCost(currentCustomerID, currentBookingRef);
            InvoiceBox.Items.Add("Extras for whole stay: £" + extrasCost);

            InvoiceBox.Items.Add("");
            Double totalCost = MainWindow.AllCustomers.calculateCost(currentCustomerID, currentBookingRef, dailyCost, extrasCost);
            InvoiceBox.Items.Add("The total cost of your stay (Including all extras):");
            InvoiceBox.Items.Add("£" + totalCost);
        }

        private void backButton_Click(object sender, RoutedEventArgs e) //Closes the current window
        {
            Close();
        }
    }
}
