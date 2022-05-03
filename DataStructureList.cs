using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        #region ADD
        private void AddEntry()
        {
            if (ValidName(textBoxName.Text) && AllFieldsNotNull())
            {
                Information addItem = new Information(textBoxName.Text, comboBoxCategory.Text, RadioButtonString(), textBoxDefinition.Text);
                Wiki.Add(addItem);
                toolStripStatusLabel.Text = "Entry added";
                SortAndDisplay();
                ClearAndReset();
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddEntry();
        }
        #endregion ADD

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
        #region VALID NAME / CHECKS
        public bool ValidName(string check)
        {
            if (!Wiki.Exists(duplicate => duplicate.GetName().Equals(check, StringComparison.OrdinalIgnoreCase)))
                return true;
            else
                ErrorMessage(0);
                return false;
        }

        // Checks that user has entered data in all four fields
        public bool AllFieldsNotNull()
        {
            if (!string.IsNullOrEmpty(textBoxName.Text) &&
                !string.IsNullOrEmpty(comboBoxCategory.Text) &&
                !string.IsNullOrEmpty(RadioButtonString()) &&
                !string.IsNullOrEmpty(textBoxDefinition.Text))
                return true;
            else
            {
                ErrorMessage(1);
                return false;
            }
        }

        // Checks that user has selected an item in the ListView
        public bool ListViewSelected()
        {
            if (listView.SelectedItems.Count == 1)
            {
                return true;
            }
            else
            {
                ErrorMessage(2);
                return false;
            }
        }

        // Outputs error messages to the tool strip status label
        private void ErrorMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 0: // ValidName() check failed
                    toolStripStatusLabel.Text = "ERROR: Duplicate Entry";
                    break;
                case 1: // AllFieldsNotNull() failed
                    toolStripStatusLabel.Text = "ERROR: Please enter data in all four fields";
                    break;
                case 2: // ListViewSelected() failed
                    toolStripStatusLabel.Text = "ERROR: Please select an item in the ListView";
                    break;
                default:
                    break;
            }
        }
        #endregion VALID NAME / CHECKS

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
        #region DELETE
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (ListViewSelected())
            {
                DialogResult delChoice = MessageBox.Show("Do you wish to delete this item?",
                    "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (delChoice == DialogResult.Yes)
                    Wiki.RemoveAt(listView.SelectedIndices[0]);
                toolStripStatusLabel.Text = "Item deleted";
                SortAndDisplay();
                ClearAndReset();
            }
        }
        #endregion DELETE

        // Q6.8 Create a button method that will save the edited record of the currently
        // selected item in the ListView. All the changes in the input controls will be
        // written back to the list. Display an updated version of the sorted list at the
        // end of this process.
        #region EDIT
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (ListViewSelected())
            {
                DialogResult editChoice = MessageBox.Show("Do you wish to edit this entry?",
                "Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (editChoice == DialogResult.Yes)
                {
                    // TODO: See if there's an edit/update method built-in
                    //Information editedEntry = new Information(textBoxName.Text, comboBoxCategory.Text, RadioButtonString(), textBoxDefinition.Text);
                    //Wiki.Add(editedEntry);
                    
                    Wiki.RemoveAt(listView.SelectedIndices[0]);
                    AddEntry();
                    toolStripStatusLabel.Text = "Entry edited";
                    SortAndDisplay();
                    ClearAndReset();
                }
            }
        }
        #endregion EDIT

        // Q6.9 Create a single custom method that will sort and then display the Name and
        // Category from the wiki information in the list.
        #region SORT / DISPLAY
        private void SortAndDisplay()
        {
            listView.Items.Clear();
            Wiki.Sort();
            foreach (var item in Wiki)
            {
                listView.Items.Add(item.Display());
            }
        }
        #endregion SORT / DISPLAY

        // Q6.10 Create a button method that will use the builtin binary search to find a
        // Data Structure name. If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView. At the end
        // of the search process the search input TextBox must be cleared.
        #region BINARY SEARCH
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Information target = new Information();
            target.SetName(textBoxSearch.Text);
            int search = Wiki.BinarySearch(target);

            if (search >= 0)
            {
                listView.Items[search].BackColor = Color.Blue;
                listView.Items[search].ForeColor = Color.White;
                toolStripStatusLabel.Text = "Found";
            }
            else
                toolStripStatusLabel.Text = "Not found";

            textBoxSearch.Clear();
        }
        #endregion BINARY SEARCH

        // Q6.11 Create a ListView event so a user can select a Data Structure Name from the
        // list of Names and the associated information will be displayed in the related text
        // boxes combo box and radio button.
        private void listView_Click(object sender, EventArgs e)
        {
            int index = listView.SelectedIndices[0];
            textBoxName.Text = Wiki[index].GetName();
            comboBoxCategory.SelectedItem = Wiki[index].GetCategory();
            if (Wiki[index].GetStructure() == "Linear")
                RadioButtonHighlight(0);
            else
                RadioButtonHighlight(1);
            textBoxDefinition.Text = Wiki[index].GetDefinition();
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
            //SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            //saveFileDialogVG.InitialDirectory = Application.StartupPath;
            //saveFileDialogVG.Filter = "DAT file|*.dat";
            //saveFileDialogVG.Title = "Save a DAT File";
            //saveFileDialogVG.FileName = defaultFileName;
            //saveFileDialogVG.DefaultExt = "bin";
            //saveFileDialogVG.ShowDialog();
            //string fileName = saveFileDialogVG.FileName;
            //if (saveFileDialogVG.FileName != "")
            //{
            //    saveRecord(fileName);
            //}
            //else
            //{
            //    saveRecord(defaultFileName);
            //}
            //toolStripStatusLabel.Text = "Saved data to .dat file";
        }
        private void saveRecord(string saveFileName)
        {
            //try
            //{
            //    using (Stream stream = File.Open(saveFileName, FileMode.Create))
            //    {
            //        BinaryFormatter bin = new BinaryFormatter();
            //        for (int x = 0; x < rowSize; x++)
            //        {
            //            for (int y = 0; y < colSize; y++)
            //            {
            //                bin.Serialize(stream, myArray[x, y]);
            //            }
            //        }
            //    }
            //}
            //catch (IOException ex)
            //{
            //    toolStripStatusLabel.Text = "Could not save .dat file";
            //}
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            //nextEmptyRow = 0;
            //OpenFileDialog openFileDialogVG = new OpenFileDialog();
            //openFileDialogVG.InitialDirectory = Application.StartupPath;
            //openFileDialogVG.Filter = "DAT Files|*.dat";
            //openFileDialogVG.Title = "Select a DAT File";
            //if (openFileDialogVG.ShowDialog() == DialogResult.OK)
            //{
            //    openRecord(openFileDialogVG.FileName);
            //}
            //toolStripStatusLabel.Text = "Opened .dat file";
        }
        private void openRecord(string openFileName)
        {
            //try
            //{
            //    using (Stream stream = File.Open(openFileName, FileMode.Open))
            //    {
            //        BinaryFormatter bin = new BinaryFormatter();
            //        for (int x = 0; x < rowSize; x++)
            //        {
            //            for (int y = 0; y < colSize; y++)
            //            {
            //                myArray[x, y] = (string)bin.Deserialize(stream);
            //            }
            //            nextEmptyRow++;
            //        }
            //    }
            //}
            //catch (IOException ex)
            //{
            //    toolStripStatusLabel.Text = "Could not open .dat file";
            //}
            //DisplayArray();
        }
        #endregion SAVE/LOAD

        // Q6.15 The Wiki application will save data when the form closes.
        private void DataStructureList_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
