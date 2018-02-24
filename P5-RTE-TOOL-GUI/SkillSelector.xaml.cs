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
    /// Interaction logic for SkillSelector.xaml
    /// </summary>
    public partial class SkillSelector : Window
    {

        public string SkillSelection = "";
        public bool HasConfirmedSelection = false;
        public List<object> TrueCollection = new List<object>();
        public List<object> LastCollection = new List<object>();

        public SkillSelector()
        {
            InitializeComponent();
        }

        private void SkillSelector_Load(object sender, EventArgs e)
        {
            //Store the true skillList collection for later use
            for (int i = 0; i < skillList.Items.Count; i++)
                TrueCollection.Add(skillList.Items[i]);
            LastCollection = TrueCollection;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (skillList.SelectedItem != null)
            {
                SkillSelection = skillList.SelectedItem.ToString();
                SkillSelection = SkillSelection.Remove(0, SkillSelection.Length - 4);
                HasConfirmedSelection = true;
                Close();
            }
            else
                MessageBox.Show("Please select an entry from the list!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /*private void searchButton_Click(object sender, EventArgs e)
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
            skillList.Items.Clear();
            foreach (object item in curCollection)
                skillList.Items.Add(item);
            //Set LastCollection as curCollection
            LastCollection = curCollection;
        }*/
    }
}
