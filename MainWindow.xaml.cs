using System;
using System.Collections.Generic;
using System.IO;
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
using LinxToXml.Business;
using LinxToXml.Core;
using System.Xml.Linq;
using System.Data;

namespace LinxToXml
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            GetUsers();
        }


        // Save new User data
        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            var currentTimeStamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

            User USR = new User();
            USR.ID = Convert.ToInt32(currentTimeStamp);
            USR.FirstName = Txt_FirstName.Text;
            USR.LastName = Txt_LastName.Text;
            USR.StreetName = Txt_StreetName.Text;
            USR.HouseNumber = Txt_HouseNumber.Text;
            USR.ApartmentNumber = Txt_ApartmentNumber.Text;
            USR.PostalCode = Txt_PostalCode.Text;
            USR.Town = Txt_Town.Text;
            USR.PhoneNumber = Txt_PhoneNumber.Text;
            USR.DateofBirth = Convert.ToDateTime(Dt_DateofBirth.SelectedDate).ToShortDateString();


            UserManager UM = new UserManager();
            UM.Add(USR);

            MessageBox.Show("New User Saved");
            ClearControls();
            GetUsers();
        }

        // Cancel operation
        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }


        // Submit edited User data
        private void Btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            User USR = new User();
            USR.ID = Convert.ToInt32(((DataRowView)Dg_Users.SelectedItem).Row["ID"]);
            USR.FirstName = Txt_FirstName.Text;
            USR.LastName = Txt_LastName.Text;
            USR.StreetName = Txt_StreetName.Text;
            USR.HouseNumber = Txt_HouseNumber.Text;
            USR.ApartmentNumber = Txt_ApartmentNumber.Text;
            USR.PostalCode = Txt_PostalCode.Text;
            USR.Town = Txt_Town.Text;
            USR.PhoneNumber = Txt_PhoneNumber.Text;
            USR.DateofBirth = Convert.ToDateTime(Dt_DateofBirth.SelectedDate).ToShortDateString();


            UserManager UM = new UserManager();
            UM.Update(USR);

            MessageBox.Show("User Edited");
            ClearControls();
            GetUsers();
        }

        // Delete selected User data
        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            UserManager UM = new UserManager();
            UM.Delete(Convert.ToInt32(((DataRowView)Dg_Users.SelectedItem).Row["ID"]));

            MessageBox.Show("User Deleted");
            ClearControls();
            GetUsers();
        }

        // Calculate Date of Birth
        private void Dt_DateofBirth_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dt_DateofBirth.SelectedDate != null)
            {
                Tb_Age.Text = (DateTime.Now.Year - Dt_DateofBirth.SelectedDate.Value.Year).ToString();
            }
            else
            {
                Tb_Age.Text = "";
            }

        }

        // Fill Form Controls with selected User data
        private void Dg_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            object item = Dg_Users.SelectedItem;
            if (item != null)
            {
                string ID = ((DataRowView)Dg_Users.SelectedItem).Row["ID"].ToString();

                Txt_FirstName.Text = ((DataRowView)Dg_Users.SelectedItem).Row["FirstName"].ToString();
                Txt_LastName.Text = ((DataRowView)Dg_Users.SelectedItem).Row["LastName"].ToString();
                Txt_StreetName.Text = ((DataRowView)Dg_Users.SelectedItem).Row["StreetName"].ToString();
                Txt_HouseNumber.Text = ((DataRowView)Dg_Users.SelectedItem).Row["HouseNumber"].ToString();
                Txt_ApartmentNumber.Text = ((DataRowView)Dg_Users.SelectedItem).Row["ApartmentNumber"].ToString();
                Txt_PostalCode.Text = ((DataRowView)Dg_Users.SelectedItem).Row["PostalCode"].ToString();
                Txt_Town.Text = ((DataRowView)Dg_Users.SelectedItem).Row["Town"].ToString();
                Txt_PhoneNumber.Text = ((DataRowView)Dg_Users.SelectedItem).Row["PhoneNumber"].ToString();
                Dt_DateofBirth.SelectedDate = Convert.ToDateTime(((DataRowView)Dg_Users.SelectedItem).Row["DateofBirth"].ToString());
            }
        }




        // Get all User data
        public void GetUsers()
        {
            DataSet dataSet = new DataSet();
            UserManager UM = new UserManager();

            dataSet = UM.GetAll();

            if (dataSet != null)
            {
                Dg_Users.ItemsSource = dataSet.Tables[0].DefaultView;
            }

        }

        // Clear Form Controls
        public void ClearControls()
        {
            Txt_FirstName.Text = string.Empty;
            Txt_LastName.Text = string.Empty;
            Txt_StreetName.Text = string.Empty;
            Txt_HouseNumber.Text = string.Empty;
            Txt_ApartmentNumber.Text = string.Empty;
            Txt_PostalCode.Text = string.Empty;
            Txt_Town.Text = string.Empty;
            Txt_PhoneNumber.Text = string.Empty;
            Dt_DateofBirth.SelectedDate = null;
        }


    }
}
