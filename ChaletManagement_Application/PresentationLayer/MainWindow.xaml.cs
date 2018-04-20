//Kieran James Burns
//Main/Initial window for user interaction
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLayer;
using DataLayer;


namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static CustomerListFacade AllCustomers = new CustomerListFacade();   //Used to store all customer data used whilst the program is running to be accessed by all classes within the Presentation Layer
        public MainWindow()
        {
            InitializeComponent();
        }

        public static int CustomerIDGen = 0;    //Used to store the current highest generated unique customer ID to be used by AddEditCustomer
        public static int BookingRefGen = 0;    //Used to store the current highest generated unique booking reference number to be used by AddEditBooking

        private void startButton_Click(object sender, RoutedEventArgs e)    //When the start button is pressed, attempt to load the data from the storage files into the corresponding variables, then move to CustomerWindow
        {
            AllCustomers = DataStorage.readFromMainFile();

            String readGenerators =  DataStorage.readGenerators();
            if (!readGenerators.Equals("None"))
            {
                String[] splitGenerators = readGenerators.Split(' ');
                CustomerIDGen = Int32.Parse(splitGenerators[0]);
                BookingRefGen = Int32.Parse(splitGenerators[1]);
            }

            CustomerWindow CW = new CustomerWindow();
            CW.Show();
            CW.customerBoxRefresh();
            Close();
        }

        private void clearFilesButton_Click(object sender, RoutedEventArgs e)   //when the clear files button is pressed, call the method in DataLayer.DataStorage to clear all save data files for this program
        {
            DataStorage.clearFiles();
        }
    }
}
