//Kieran James Burns
//Window used for user input to create or modify a set of car hire details
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
    /// Interaction logic for addCarHire.xaml
    /// </summary>
    public partial class addCarHire : Window
    {
        public addCarHire(String newOrEditIn, int passedCustomerID, int passedBookingRef)
        {
            InitializeComponent();
            this.newOrEdit = newOrEditIn;
            currentCustomerID = passedCustomerID;
            currentBookingRef = passedBookingRef;
        }

        static int currentCustomerID;
        static int currentBookingRef;
        String newOrEdit;
        HireCar currentHire = new HireCar();

        public void loadData()      //Controls to load in the data of an existing set of car hire details
        {   
            List <HireCar> currentHireFind = MainWindow.AllCustomers.getHire(currentCustomerID, currentBookingRef);
            foreach (var hire in currentHireFind)
            {
                currentHire = hire;
            }

            nameInBox.Text = currentHire.DriverName;
            hireInBox.Text = currentHire.HireDate.ToString();
            returnInBox.Text = currentHire.ReturnDate.ToString();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)  //Controls to take in, validate and add the fields to create or amend a set if car hire details upon button press
        {
            int Errors = 0;

            try
            {
                currentHire.DriverName = nameInBox.Text;
                nameError.Content = "";
                if (currentHire.DriverName.Equals(""))
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
                currentHire.HireDate = DateTime.Parse(hireInBox.Text);
                hireError.Content = "";
            }
            catch
            {
                Errors++;
                hireError.Content = "!";
            }

            try
            {
                currentHire.ReturnDate = DateTime.Parse(returnInBox.Text);
                returnError.Content = "";
                if (currentHire.HireDate.Date > currentHire.ReturnDate.Date)
                {
                    Errors++;
                    returnError.Content = "!";
                }
            }
            catch
            {
                Errors++;
                returnError.Content = "!";
            }

            if (Errors == 0)
            {
                if (newOrEdit.Equals("N"))
                {
                    MainWindow.AllCustomers.addHire(currentCustomerID,currentBookingRef,currentHire);
                }
                DetailsWindow DW = new DetailsWindow(currentCustomerID, currentBookingRef);
                DW.Show();
                DW.PageRefresh();
                this.Close();
            }
            else
            {
                MessageBox.Show("There are " + Errors + " invalid entries, they have each been marked with '!'. Please fix these then try again!");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)   //Cancel operation if a set of car hire details exists then open the appropriate window
        {
            List<HireCar> currentHire = MainWindow.AllCustomers.getHire(currentCustomerID,currentBookingRef);
            if (currentHire.Count == 0)
            {
                MessageBox.Show("Cannot cancel until hire completed! To delete a hire go to Manage Booking Details > Edit Extras then deselect 'Hire a Car'");
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

