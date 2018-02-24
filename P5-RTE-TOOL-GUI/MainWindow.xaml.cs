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
            if ((TMAPIradiobutton.IsChecked == true) && (CCAPIradiobutton.IsChecked == false) && (RPCS3radiobutton.IsChecked == false))
            {
                PS3API.ChangeAPI(SelectAPI.TargetManager);
                usingPS3Lib = true;
            }
            //setToolStrip("API set to: " + PS3API.GetCurrentAPIName(), Color.Black);
        }

        private void CCAPIradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            if (CCAPIradiobutton.IsChecked == true && TMAPIradiobutton.IsChecked == false && RPCS3radiobutton.IsChecked == false)
            {
                PS3API.ChangeAPI(SelectAPI.ControlConsole);
                usingPS3Lib = true;
            }
            //setToolStrip("API set to: " + PS3API.GetCurrentAPIName(), Color.Black);
        }

        private void RPCS3radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            if (RPCS3radiobutton.IsChecked == true && CCAPIradiobutton.IsChecked == false && TMAPIradiobutton.IsChecked == false)
            {
                ConnectAttachbutton.IsEnabled = false;
                usingPS3Lib = false;
                //setToolStrip("API set to: RPCS3", Color.Black);
            }
            else if (RPCS3radiobutton.IsChecked == false)
            {
                ConnectAttachbutton.IsEnabled = true;
            }
        }

        private void connectbutton_Click(object sender, RoutedEventArgs e)
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
                //setToolStrip("Connected to target!", Color.Green);
                return true;
            }
            else
                //setToolStrip("Could not connect to target!", Color.Red);
            return false;
        }

        private bool processAttach()
        {
            if (PS3API.AttachProcess())
            {
                personaSlot.IsEnabled = true;
                //setToolStrip("Attached to process!", Color.Green);
                return true;
            }
            else
                //setToolStrip("Could not attach to process!", Color.Red);
            return false;
        }

        private void GetInfobutton_Click(object sender, RoutedEventArgs e)
        {
            /*
            personaInput.Text = Offsets.GetPersona((int)personaSlot.Value);
            levelInput.Value = Offsets.GetLevel((int)personaSlot.Value);
            stInput.Value = Offsets.GetStat((int)personaSlot.Value, "St");
            maInput.Value = Offsets.GetStat((int)personaSlot.Value, "Ma");
            enInput.Value = Offsets.GetStat((int)personaSlot.Value, "En");
            agInput.Value = Offsets.GetStat((int)personaSlot.Value, "Ag");
            luInput.Value = Offsets.GetStat((int)personaSlot.Value, "Lu");
        private void skillGetButton_Click(object sender, EventArgs e)
        {
            skillInput.Text = Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value);
            setToolStrip("Skill " + skillSlot.Value + " bytes retrieved as: " + Offsets.GetSkill((int)personaSlot.Value, (int)skillSlot.Value), Color.Black);
        }
             */
        }
    }
}
