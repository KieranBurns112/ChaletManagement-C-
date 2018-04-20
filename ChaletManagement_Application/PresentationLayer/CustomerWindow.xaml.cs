//Kieran James Burns
//Window used for user interaction with all customers 
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
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        public void customerBoxRefresh()       //Refresh the content within the box that displays all customers on the page
        {
            CustomerBox.Items.Clear();
            if (MainWindow.AllCustomers.Customers.Count == 0)
            {
                BoxHeader.Content = "No Customers currently available";
            }
            else
            {
                BoxHeader.Content = "ID    -    Name    -    Address";
                foreach (var customer in MainWindow.AllCustomers.Customers)
                {
                    CustomerBox.Items.Add(customer.CustomerID + " - " + customer.CustomerName + " - " + customer.CustomerAddress);
                }
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Calls the appropriate classes to create a new customer object
        {
            AddEditCustomer AEC = new AddEditCustomer("N");
            AEC.Show();
            AEC.titleLabel.Content = "New Customer";  
            Close();
        }

        private void bookingsButton_Click(object sender, RoutedEventArgs e) //Calls the appropriate class to load BookingsWindow
        {
            try
            {
                String[] selectedLine = CustomerBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                BookingsWindow BW = new BookingsWindow(selectedInt);
                BW.Show();
                BW.BookingsBoxRefresh();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Click on an item in the left had list to select it!");
            }
}

        private void saveButton_Click(object sender, RoutedEventArgs e)     //Calls class to save the current data of the program to a file
        {
            SaveButtonFacade.SaveButtonPressed();
        }

        private void amendButton_Click(object sender, RoutedEventArgs e)    //Calls the appropriate classes to modify an existing customer object
        {
            try
            {
                String[] selectedLine = CustomerBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                AddEditCustomer AEC = new AddEditCustomer("A");
                AEC.Show();
                AEC.loadData(selectedInt);
                AEC.titleLabel.Content = "Edit Customer";
                Close();
            }
            catch
            {
                MessageBox.Show("Click on an item in the left hand list to select it!");
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)   //Calls the appropriate classes to delete an existing customer object
        {
            try
            {
                String[] selectedLine = CustomerBox.SelectedItem.ToString().Split(' ');
                int selectedInt = Int32.Parse(selectedLine[0]);
                MessageBoxResult deleteConfirm = MessageBox.Show("Once a Customer record is deleted, it's gone forever! Are you sure you want to delete this record?", "Are you sure?", MessageBoxButton.YesNo);
                if (deleteConfirm == MessageBoxResult.Yes)
                {
                    MainWindow.AllCustomers.deleteCustomer(selectedInt);
                    this.customerBoxRefresh();
                }
            }
            catch
            {
                MessageBox.Show("Click on an item in the left hand list to select it!");
            }
        }
    }
}
