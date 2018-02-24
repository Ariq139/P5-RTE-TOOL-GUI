using P5_RTM_Tool_v2;
using Persona5HookTest;
using PS3Lib;
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

namespace P5_RTE_TOOL_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public readonly bool Debug = false;

        public static PS3API PS3API = new PS3API();
        public static ProcessMemoryAccessor RPCS3API;

        public static bool usingPS3Lib = true;

        //public object ConnectAttachbutton { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void personaSlot_Loaded(object sender, RoutedEventArgs e)
        {
           
            List<string> data = new List<string>();
            data.Add("1");
            data.Add("2");
            data.Add("3");
            data.Add("4");
            data.Add("5");
            data.Add("6");
            data.Add("7");
            data.Add("8");
            data.Add("9");
            data.Add("10");
            data.Add("11");
            data.Add("12");

            // ... Get the ComboBox reference.
            var comboBox = sender as ComboBox;

            // ... Assign the ItemsSource to the List.
            comboBox.ItemsSource = data;

        }

        private void personaSlot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

        }

        private void TMAPIradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((TMAPIradiobutton.IsChecked == true) && (CCAPIradiobutton.IsChecked == false) && (RPCS3radiobutton.IsChecked == false))
                {
                    ConnectAttachbutton.IsEnabled = true;
                    PS3API.ChangeAPI(SelectAPI.TargetManager);
                    usingPS3Lib = true;
                }
            }
            catch { }
        }

        private void CCAPIradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CCAPIradiobutton.IsChecked == true && TMAPIradiobutton.IsChecked == false && RPCS3radiobutton.IsChecked == false)
                {
                    PS3API.ChangeAPI(SelectAPI.ControlConsole);
                    ConnectAttachbutton.IsEnabled = true;
                    usingPS3Lib = true;
                }
            }
            catch { }
        }

        private void RPCS3radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RPCS3radiobutton.IsChecked == true && CCAPIradiobutton.IsChecked == false && TMAPIradiobutton.IsChecked == false)
                {

                    ConnectAttachbutton.IsEnabled = false;
                    usingPS3Lib = false;
                }
                else if (RPCS3radiobutton.IsChecked == false)
                {
                    ConnectAttachbutton.IsEnabled = true;
                }
            }
            catch { }
        }

        private void Connectbutton_Click(object sender, RoutedEventArgs e)
        {
            if (usingPS3Lib) //TMAPI or CCAPI
                targetConnect();
            else //RPCS3
            {
                try
                {
                    RPCS3API = new ProcessMemoryAccessor("rpcs3", 0x100000000);
                    personaSlot.IsEnabled = true;
                    //setToolStrip("Connected to RPCS3!", Color.Green);
                }
                catch (Exception)
                {
                    MessageBox.Show("Please open the RPCS3 game process before connecting!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //setToolStrip("Could not connect to RPCS3!", Color.Red);
                }
            }
        }
        private bool targetConnect()
        {
            if (PS3API.ConnectTarget())
            {
                attachButton.IsEnabled = true;
                MessageBox.Show("Connected to target");
                return true;
            }
            else
                MessageBox.Show("Could not connect to target");
            return false;
        }

        private bool processAttach()
        {
            if (PS3API.AttachProcess())
            {
                MessageBox.Show("Attached to process");
                return true;
            }
            else
                MessageBox.Show("Could not attach to process");
            return false;
        }

        private void GetInfobutton_Click(object sender, RoutedEventArgs e)
        {
            
            //personaInput.Text = Offsets.GetPersona((int)personaSlot.Value);
            //levelInput.Value = Offsets.GetLevel((int)personaSlot.Value);
            StAmount.Content = Offsets.GetStat((int)personaSlot.SelectedItem, "St");
            MaAmount.Content = Offsets.GetStat((int)personaSlot.SelectedItem, "Ma");
            EnAmount.Content = Offsets.GetStat((int)personaSlot.SelectedItem, "En");
            AgAmount.Content = Offsets.GetStat((int)personaSlot.SelectedItem, "Ag");
            LuAmount.Content = Offsets.GetStat((int)personaSlot.SelectedItem, "Lu");

        //private void skillGetButton_Click(object sender, EventArgs e)
        //{
        //    skillInput.Text = Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value);
        //    setToolStrip("Skill " + skillSlot.Value + " bytes retrieved as: " + Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value), Color.Black);
        //}
             
        }
    }
}
