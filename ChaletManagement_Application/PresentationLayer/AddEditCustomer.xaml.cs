//Kieran James Burns
//Window used for user input to create or modify a customer
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
    /// Interaction logic for AddEditCustomer.xaml
    /// </summary>
    public partial class AddEditCustomer : Window
    {
        public AddEditCustomer(String newOrEditIn)
        {
            InitializeComponent();
            this.newOrEdit = newOrEditIn;
        }

        String newOrEdit;
        Customer currentCustomer = new Customer();     

        public void loadData(int currentCustomerID) //Controls to load in the data of an existing customer
        {
            currentCustomer = MainWindow.AllCustomers.findCustomer(currentCustomerID);
            nameInBox.Text = currentCustomer.CustomerName;
            addressInBox.Text = currentCustomer.CustomerAddress;  
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Controls to take in, validate and add the fields to create or amend a customer upon button press
        {            
            int Errors = 0;

            try
            {
                currentCustomer.CustomerName = nameInBox.Text;
                nameError.Content = "";
                if (currentCustomer.CustomerName.Equals(""))
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
                currentCustomer.CustomerAddress = addressInBox.Text;
                addressError.Content = "";
                if (currentCustomer.CustomerAddress.Equals(""))
                {
                    Errors++;
                    addressError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                addressError.Content = "!";
            }

            if (Errors == 0)
            {
                if (newOrEdit.Equals("N"))
                {
                    MainWindow.CustomerIDGen++;
                    currentCustomer.CustomerID = MainWindow.CustomerIDGen;
                    MainWindow.AllCustomers.addCustomer(currentCustomer);
                    MessageBoxResult makeNewBooking = MessageBox.Show("Would you like to add a booking to this customer? This can also be done via Manage Customers > View Bookings of Selected Customer.", "Add a booking?", MessageBoxButton.YesNo);
                    if (makeNewBooking == MessageBoxResult.Yes)
                    {
                        AddEditBooking AEB = new AddEditBooking("N", currentCustomer.CustomerID);
                        AEB.Show();
                        AEB.titleLabel.Content = "New Booking";
                        Close();
                    }
                    else
                    {
                        CustomerWindow CW = new CustomerWindow();
                        CW.Show();
                        CW.customerBoxRefresh();
                        this.Close();
                    }
                }
                else
                {
                    CustomerWindow CW = new CustomerWindow();
                    CW.Show();
                    CW.customerBoxRefresh();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("There are " + Errors + " invalid entries, they have each been marked with '!'. Please fix these then try again!");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e) //Cancel operation and return to the CustomerWindow
        {
            CustomerWindow CW = new CustomerWindow();
            CW.Show();
            CW.customerBoxRefresh();
            this.Close();
        }
    }
}
