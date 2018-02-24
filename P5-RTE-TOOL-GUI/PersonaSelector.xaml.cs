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
using System.Windows.Shapes;

namespace P5_RTE_TOOL_GUI
{
    /// <summary>
    /// Interaction logic for PersonaSelector.xaml
    /// </summary>
    public partial class PersonaSelector : Window
    {

        public string PersonaSelection = "XXXX";
        public bool HasConfirmedSelection = false;
        public List<object> TrueCollection = new List<object>();
        public List<object> LastCollection = new List<object>();
        public bool SearchChanged = false;

        public PersonaSelector()
        {
            InitializeComponent();
        }

        private void PersonaSelector_Load(object sender, EventArgs e)
        {
            //Store the true personaList collection for later use
            for (int i = 0; i < personaList.Items.Count; i++)
                TrueCollection.Add(personaList.Items[i]);
            LastCollection = TrueCollection;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string search = searchInput.Text;
            List<object> curCollection = new List<object>();

            if (search != "")
            {
                //Go through LastCollection and add potential searches to curCollection
                foreach (object item in LastCollection)
                {
                    if (item.ToString().Contains(search, StringComparison.InvariantCultureIgnoreCase))
                        curCollection.Add(item);
                }
            }
            else
                curCollection = TrueCollection;

            //Clear personaList and fill it with curCollection
            personaList.Items.Clear();
            foreach (object item in curCollection)
                personaList.Items.Add(item);
            //Set LastCollection as curCollection
            LastCollection = curCollection;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (personaList.SelectedItem != null)
            {
                PersonaSelection = personaList.SelectedItem.ToString();
                PersonaSelection = PersonaSelection.Remove(0, PersonaSelection.Length - 4);
                HasConfirmedSelection = true;
                Close();
            }
            else
                MessageBox.Show("Please select an entry from the list!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
