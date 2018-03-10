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
                    processName.IsEnabled = true;
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
                    RPCS3API = new ProcessMemoryAccessor(processName.Text, 0x100000000);
                    personaSlot.IsEnabled = true;
                    GetInfobutton.IsEnabled = true;
                    StAmount.IsEnabled = MaAmount.IsEnabled
                        = EnAmount.IsEnabled = AgAmount.IsEnabled
                        = LuAmount.IsEnabled = stSlider.IsEnabled
                        = maSlider.IsEnabled = enSlider.IsEnabled
                        = agSlider.IsEnabled = luSlider.IsEnabled
                        = SetSt.IsEnabled = SetMa.IsEnabled
                        = SetEn.IsEnabled = SetAg.IsEnabled
                        = SetLu.IsEnabled = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("No process found! Please check your RPCS3 process name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SetSt_Click(object sender, RoutedEventArgs e)
        {
            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            Offsets.SetStat(slotnum, "St", Int32.Parse(StAmount.Text));
        }

        private void SetMa_Click(object sender, RoutedEventArgs e)
        {
            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            Offsets.SetStat(slotnum, "Ma", Int32.Parse(MaAmount.Text));
        }

        private void SetEn_Click(object sender, RoutedEventArgs e)
        {
            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            Offsets.SetStat(slotnum, "En", Int32.Parse(EnAmount.Text));
        }

        private void SetAg_Click(object sender, RoutedEventArgs e)
        {
            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            Offsets.SetStat(slotnum, "Ag", Int32.Parse(AgAmount.Text));
        }

        private void SetLu_Click(object sender, RoutedEventArgs e)
        {
            var SelectSlot = personaSlot.Items[personaSlot.SelectedIndex] as ComboBoxItem;

            int slotnum = Convert.ToInt32(SelectSlot.Content);

            Offsets.SetStat(slotnum, "Lu", Int32.Parse(LuAmount.Text));
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

            stSlider.Value = Offsets.GetStat(slotnum, "St");
            maSlider.Value = Offsets.GetStat(slotnum, "Ma");
            enSlider.Value = Offsets.GetStat(slotnum, "En");
            agSlider.Value = Offsets.GetStat(slotnum, "Ag");
            luSlider.Value = Offsets.GetStat(slotnum, "Lu");

            skill1.Content = Offsets.GetSkill(slotnum, 1);
            skill2.Content = Offsets.GetSkill(slotnum, 2);
            skill3.Content = Offsets.GetSkill(slotnum, 3);
            skill4.Content = Offsets.GetSkill(slotnum, 4);
            skill5.Content = Offsets.GetSkill(slotnum, 5);
            skill6.Content = Offsets.GetSkill(slotnum, 6);
            skill7.Content = Offsets.GetSkill(slotnum, 7);
            skill8.Content = Offsets.GetSkill(slotnum, 8);

        }

        
    }
}
