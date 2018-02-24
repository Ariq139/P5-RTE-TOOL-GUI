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
            //if (TMAPIradiobutton. && !CCAPIradiobutton.Checked && !RPCS3radiobutton.Checked)
            //{
                PS3API.ChangeAPI(SelectAPI.TargetManager);
                usingPS3Lib = true;
            //}
            //setToolStrip("API set to: " + PS3API.GetCurrentAPIName(), Color.Black);
        }

        private void CCAPIradiobutton_Checked(object sender, RoutedEventArgs e)
        {
            //if (CCAPIradioButton.Checked && !TMAPIradioButton.Checked && !RPCS3radioButton.Checked)
            //{
                PS3API.ChangeAPI(SelectAPI.ControlConsole);
                usingPS3Lib = true;
            //}
            //setToolStrip("API set to: " + PS3API.GetCurrentAPIName(), Color.Black);
        }

        private void RPCS3radiobutton_Checked(object sender, RoutedEventArgs e)
        {
            //if (RPCS3radioButton.Checked && !CCAPIradioButton.Checked && !TMAPIradioButton.Checked)
            //{
                //ConnectAttachbutton = false;
                usingPS3Lib = false;
                //setToolStrip("API set to: RPCS3", Color.Black);
            //}
            //else if (!RPCS3radioButton.Checked)
            //{
                //connectAndAttachButton.Enabled = true;
            //}
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
                    //personaSlot.Enabled = true;
                    //setToolStrip("Connected to RPCS3!", Color.Green);
                }
                catch (Exception)
                {
                    //MessageBox.Show("Please open the RPCS3 game process before connecting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //setToolStrip("Could not connect to RPCS3!", Color.Red);
                }
            }
        }
        private bool targetConnect()
        {
            if (PS3API.ConnectTarget())
            {
                //attachButton.Enabled = true;
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
                //personaSlot.Enabled = true;
                //setToolStrip("Attached to process!", Color.Green);
                return true;
            }
            else
                //setToolStrip("Could not attach to process!", Color.Red);
            return false;
        }
    }
}
