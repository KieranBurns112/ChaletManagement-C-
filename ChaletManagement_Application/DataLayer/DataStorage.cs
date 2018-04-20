//Kieran James Burns
//Contains the Methods to read from and write to both the customer list 
//(including all its sub lists) and  both the value of the unique Customer ID 
//generator and the unique Booking Reference generator to/from the appropriate 
//storage files.
// Last modified: 03/12/2017

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{

    public class DataStorage
    {
        private const string mainFileName = "allCustomersData.dat";
        private const string generatorsFileName = "generatorsFile.txt";

        //Method used to write all details of the given customer list and set 
        //of unique ID generators to a file to be read on next startup.
        public static void writeToFiles(CustomerListFacade writeList, String writeGenerators)  
        {
            if (File.Exists(mainFileName))
            {
                File.Delete(mainFileName);
            }

            if (File.Exists(generatorsFileName))
            {
                File.Delete(generatorsFileName);
            }
            
            FileStream stream = File.Create(mainFileName);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, writeList);
            stream.Close();

            var genFile = File.Create(generatorsFileName);
            genFile.Close();
            File.WriteAllText(generatorsFileName, writeGenerators);

            return;
        }

        //Method used to read in all details of the Main save file.
        public static CustomerListFacade readFromMainFile() 
        {
            CustomerListFacade readList = new CustomerListFacade();
            if (File.Exists(mainFileName) && new FileInfo(mainFileName).Length != 0)
            {
                FileStream stream = File.OpenRead(mainFileName);
                BinaryFormatter formatter = new BinaryFormatter();
                readList = (CustomerListFacade)formatter.Deserialize(stream);
                stream.Close();
            }
            return readList;
        }

        //Method used to read in all details of the file used to store the 
        //generators positions.
        public static String readGenerators()   
        {
            String generators = "None";
            if (File.Exists(generatorsFileName) && new FileInfo(generatorsFileName).Length != 0)
            {
                generators = File.ReadAllText(generatorsFileName);
            }
            return generators;
        }

        //Method used to clear all saved data in the I/O files
        public static void clearFiles() 
        {
            if (File.Exists(mainFileName))
            {
                File.Delete(mainFileName);
            }

            if (File.Exists(generatorsFileName))
            {
                File.Delete(generatorsFileName);
            }

            return;
        }
    }
}
