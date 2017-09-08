using System.Management;
using System;
using System.Windows;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Linq;
using System.Text;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string url = (@"http://1c.mti.ua/39435939/UTP/hs/report/000000052");

        public MainWindow()
        {
            InitializeComponent();
        }

    
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ListView1.Items.Clear();


            Task<XDocument> task = Task.Run(() => GetXmlFromVkAsync());
            XDocument xml = task.Result;
            

             ManagementObjectSearcher searcher_soft = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Product");
            foreach (ManagementObject queryObj in searcher_soft.Get())
            {
                
                Console.WriteLine("Win32_Product instance");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("AssignmentType: {0}", queryObj["AssignmentType"]);
                Console.WriteLine("Caption: {0}", queryObj["Caption"]);
                Console.WriteLine("Description: {0}", queryObj["Description"]);
                Console.WriteLine("HelpLink: {0}", queryObj["HelpLink"]);
                Console.WriteLine("HelpTelephone: {0}", queryObj["HelpTelephone"]);
                Console.WriteLine("IdentifyingNumber: {0}", queryObj["IdentifyingNumber"]);
                Console.WriteLine("InstallDate: {0}", queryObj["InstallDate"]);
                Console.WriteLine("InstallDate2: {0}", queryObj["InstallDate2"]);
                Console.WriteLine("InstallLocation: {0}", queryObj["InstallLocation"]);
                Console.WriteLine("InstallSource: {0}", queryObj["InstallSource"]);
                Console.WriteLine("InstallState: {0}", queryObj["InstallState"]);
                Console.WriteLine("Language: {0}", queryObj["Language"]);
                Console.WriteLine("LocalPackage: {0}", queryObj["LocalPackage"]);
                Console.WriteLine("Name: {0}", queryObj["Name"]);
                Console.WriteLine("PackageCache: {0}", queryObj["PackageCache"]);
                Console.WriteLine("PackageCode: {0}", queryObj["PackageCode"]);
                Console.WriteLine("PackageName: {0}", queryObj["PackageName"]);
                Console.WriteLine("ProductID: {0}", queryObj["ProductID"]);
                Console.WriteLine("RegCompany: {0}", queryObj["RegCompany"]);
                Console.WriteLine("RegOwner: {0}", queryObj["RegOwner"]);
                Console.WriteLine("SKUNumber: {0}", queryObj["SKUNumber"]);
                Console.WriteLine("Transforms: {0}", queryObj["Transforms"]);
                Console.WriteLine("URLInfoAbout: {0}", queryObj["URLInfoAbout"]);
                Console.WriteLine("URLUpdateInfo: {0}", queryObj["URLUpdateInfo"]);
                Console.WriteLine("Vendor: {0}", queryObj["Vendor"]);
                Console.WriteLine("Version: {0}", queryObj["Version"]);
                Console.WriteLine("WordCount: {0}", queryObj["WordCount"]);
                
            }
            //ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Process.Start("Notepad.exe");

        }

        private async Task<XDocument> GetXmlFromVkAsync()
        {

            string user = "web";
            string password = "web";

            var credentials = new NetworkCredential(user, password);
            var handler = new HttpClientHandler { Credentials = credentials };

            using (var client = new HttpClient(handler))
            {

                byte[] bytes = await client.GetByteArrayAsync(url);
                string data = Encoding.Default.GetString(bytes);
          XDocument xmlDoc = XDocument.Parse(data);
                

                return xmlDoc;
                /*
                var query = from user in xmlDoc.Descendants("user")
                            select new User
                            {
                                Id = (int)user.Element("id"),
                                FirstName = user.Element("first_name").Value.ToString(),
                                LastName = user.Element("last_name").Value.ToString()
                            };

                foreach (var user in query)
                {
                    textBoxOutput.Text += user.FirstName + " " + user.LastName;
                }*/
            }
        }
    }
}
