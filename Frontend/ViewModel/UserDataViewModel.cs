using Converter;
using Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.ViewModel
{
    class UserDataViewModel : BaseClassViewModel
    {

        public UserData UserData;
        public UserDataViewModel()
        {
            UserData = new UserData();
        }

        public string PdfFolder
        {
            get { return UserData.PdfFolder; }
            set { UserData.PdfFolder = value; OnPropertyChanged("PdfFolder"); }
        }

        public string JpgFolder
        {
            get { return UserData.JpgFolder; }
            set { UserData.JpgFolder = value; OnPropertyChanged("JpgFolder"); }
        }

        public string DocxFolder
        {
            get { return UserData.DocxFolder; }
            set { UserData.DocxFolder = value; OnPropertyChanged("DocxFolder"); }
        }

        public string Hostname
        {
            get { return UserData.Hostname; }
            set { UserData.Hostname = value; OnPropertyChanged("Hostname"); }
        }

        public int Port
        {
            get { return UserData.Port; }
            set { UserData.Port = value; OnPropertyChanged("Port"); }
        }

        public string Username
        {
            get { return UserData.Username; }
            set { UserData.Username = value; OnPropertyChanged("Username"); }
        }

        public string Password
        {
            get { return UserData.Password; }
            set { UserData.Password = value; OnPropertyChanged("Password"); }
        }

        public void RunPipeline()
        {
            Pipeline.Invoke(PdfFolder, JpgFolder, DocxFolder, Hostname, Port, Username, Password);
        }
    }
}
