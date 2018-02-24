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
                    connectButton.IsEnabled = true;
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
                    RPCS3API = new ProcessMemoryAccessor("farseer", 0x100000000);
                    personaSlot.IsEnabled = true;
                    GetInfobutton.IsEnabled = true;
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

            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            personaName.Content = Offsets.GetPersona(slotnum);
            lvl.Content = Offsets.GetLevel(slotnum);

            StAmount.Content = stSlider.Value = Offsets.GetStat(slotnum, "St");
            MaAmount.Content = maSlider.Value = Offsets.GetStat(slotnum, "Ma");
            EnAmount.Content = enSlider.Value = Offsets.GetStat(slotnum, "En");
            AgAmount.Content = agSlider.Value = Offsets.GetStat(slotnum, "Ag");
            LuAmount.Content = luSlider.Value = Offsets.GetStat(slotnum, "Lu");

            skill1.Content = Offsets.GetSkill(slotnum, 1);
            skill2.Content = Offsets.GetSkill(slotnum, 2);
            skill3.Content = Offsets.GetSkill(slotnum, 3);
            skill4.Content = Offsets.GetSkill(slotnum, 4);
            skill5.Content = Offsets.GetSkill(slotnum, 5);
            skill6.Content = Offsets.GetSkill(slotnum, 6);
            skill7.Content = Offsets.GetSkill(slotnum, 7);
            skill8.Content = Offsets.GetSkill(slotnum, 8);

            //private void skillGetButton_Click(object sender, EventArgs e)
            //{
            //    skillInput.Text = Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value);
            //    setToolStrip("Skill " + skillSlot.Value + " bytes retrieved as: " + Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value), Color.Black);
            //}

        }

        private void setSt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
