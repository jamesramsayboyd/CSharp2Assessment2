using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataStructureList
{
    public partial class DataStructureList : Form
    {
        public DataStructureList()
        {
            InitializeComponent();
        }
        // Q6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();

        // Q6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category,
        // Radio group for the Structure and Multiline TextBox for the Definition.
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // TODO: Implement ValidName() to check for duplicate entry
            Information addItem = new Information();
            addItem.setName(textBoxName.Text);
            addItem.setCategory(comboBoxCategory.Text);
            addItem.setStructure(RadioButtonString());
            addItem.setDefinition(textBoxDefinition.Text);
            Wiki.Add(addItem);
            SortAndDisplay();
            ClearAndReset();
        }

        // Q6.4 Create and initialise a global string array with the six categories
        // as indicated in the Data Structure Matrix. Create a custom method to
        // populate the ComboBox when the Form Load method is called.
        #region COMBOBOX
        static string[] categories = { "Array", "List", "Tree", "Graphs", "Abstract", "Hash" };
        private void PopulateCombobox()
        {
            comboBoxCategory.Items.AddRange(categories);
        }
        private void DataStructureList_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
        }
        #endregion COMBOBOX

        // Q6.5 Create a custom ValidName method which will take a parameter string
        // value from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        public bool ValidName(string check)
        {
            if (Wiki.Exists(duplicate => duplicate.Equals(check)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Q6.6 Create two methods to highlight and return the values from the Radio button
        // GroupBox. The first method must return a string value from the selected radio
        // button (Linear or Non-Linear). The second method must send an integer index which
        // will highlight an appropriate radio button.
        #region RADIO BUTTONS
        private string RadioButtonString()
        {
            if (radioButtonLinear.Checked == true)
                return "Linear";
            else
                return "Non-Linear";
        }

        private void RadioButtonHighlight(int index)
        {
            if (index == 0)
                radioButtonLinear.Checked = true;
            else
                radioButtonNonLinear.Checked = true;
        }
        #endregion RADIO BUTTONS


        // Q6.7 Create a button method that will delete the currently selected record in the
        // ListView. Ensure the user has the option to backout of this action by using a
        // dialog box. Display an updated version of the sorted list at the end of this process.
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                DialogResult delChoice = MessageBox.Show("Do you wish to delete this item?",
                    "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (delChoice == DialogResult.Yes)
                    Wiki.RemoveAt(listView.SelectedIndices[0]);
                toolStripStatusLabel.Text = "Item deleted";
                SortAndDisplay();
                ClearAndReset();
            }
            else
                toolStripStatusLabel.Text = "Please select an item to delete";
        }

        // Q6.8 Create a button method that will save the edited record of the currently
        // selected item in the ListView. All the changes in the input controls will be
        // written back to the list. Display an updated version of the sorted list at the
        // end of this process.
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count != 0)
            {
                DialogResult editChoice = MessageBox.Show("Do you wish to edit this entry?",
                "Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (editChoice == DialogResult.Yes)
                {
                    Information editedEntry = new Information();
                    editedEntry.setName(textBoxName.Text);
                    editedEntry.setCategory(comboBoxCategory.Text);
                    editedEntry.setStructure(RadioButtonString());
                    editedEntry.setDefinition(textBoxDefinition.Text);

                    // TODO: See if there's an edit/update method built-in
                    Wiki.RemoveAt(listView.SelectedIndices[0]);
                    Wiki.Add(editedEntry);
                    toolStripStatusLabel.Text = "Entry edited";
                    SortAndDisplay();
                    ClearAndReset();
                }
            }
            else
                toolStripStatusLabel.Text = "Please select an item to edit";
        }

        // Q6.9 Create a single custom method that will sort and then display the Name and
        // Category from the wiki information in the list.
        private void SortAndDisplay()
        {
            listView.Items.Clear();
            Wiki.Sort();
            foreach (var item in Wiki)
            {
                ListViewItem lvi = new ListViewItem(item.getName());
                lvi.SubItems.Add(item.getCategory());
                listView.Items.Add(lvi);
            }
        }

        // Q6.10 Create a button method that will use the builtin binary search to find a
        // Data Structure name. If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView. At the end
        // of the search process the search input TextBox must be cleared.
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Information target = new Information();
            target.setName(textBoxSearch.Text);
            if (Wiki.BinarySearch(target) >= 0)
            {
                // highlight in listview
                statusStrip1.Text = "Found";
            }
            textBoxSearch.Clear();
        }

        // Q6.11 Create a ListView event so a user can select a Data Structure Name from the
        // list of Names and the associated information will be displayed in the related text
        // boxes combo box and radio button.
        private void listView_Click(object sender, EventArgs e)
        {
            int index = listView.SelectedIndices[0];
            textBoxName.Text = Wiki[index].getName();
            comboBoxCategory.SelectedItem = Wiki[index].getCategory();
            if (Wiki[index].getStructure() == "Linear")
                RadioButtonHighlight(0);
            else
                RadioButtonHighlight(1);
            textBoxDefinition.Text = Wiki[index].getDefinition();
        }

        // Q6.12 Create a custom method that will clear and reset the Textboxes, ComboBox
        // and Radio button
        public void ClearAndReset()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            comboBoxCategory.SelectedItem = null;
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
        }

        // Q6.13 Create a double click event on the Name TextBox to clear the TextBboxes,
        // ComboBox and Radio button.
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearAndReset();
        }

        // Q6.14 Create two buttons for the manual open and save option; this must use a dialog
        // box to select a file or rename a saved file. All Wiki data is stored/retrieved using
        // a binary file format.
        #region SAVE/LOAD
        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {

        }
        #endregion SAVE/LOAD

        // Q6.15 The Wiki application will save data when the form closes.
        private void DataStructureList_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

    }
}
