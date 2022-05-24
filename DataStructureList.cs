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
using System.Diagnostics;
using System.Windows.Forms;

namespace DataStructureList
{
    public partial class DataStructureList : Form
    {
        public DataStructureList()
        {
            InitializeComponent();
            SetToolTips();
            // Creating log file record Trace statement debugging output
            Stream debugOutput = File.Create("DebugOutput.txt");
            Trace.Listeners.Add(new TextWriterTraceListener(debugOutput));
            Trace.AutoFlush = true;
            Trace.WriteLine("***Debug output for Data Structure List program***");
            Trace.WriteLine("");
        }
        // Q6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();

        // Q6.3 Create a button method to ADD a new item to the list.
        // Use a TextBox for the Name input, ComboBox for the Category,
        // Radio group for the Structure and Multiline TextBox for the Definition.
        #region ADD
        private void AddEntry()
        {
            ResetColours();
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
        string[] categories = File.ReadAllLines(@"categories.txt");
        private void PopulateCombobox()
        {
            for (int i = 0; i < categories.Length; i++)
            {
                comboBoxCategory.Items.Add(categories[i]);
            }
        }
        private void DataStructureList_Load(object sender, EventArgs e)
        {
            PopulateCombobox();
        }
        #endregion COMBOBOX

        // Q6.5 Create a custom ValidName method which will take a parameter string
        // value from the Textbox Name and returns a Boolean after checking for duplicates.
        // Use the built in List<T> method “Exists” to answer this requirement.
        #region VALID NAME / CHECKS / UTILITIES
        public bool ValidName(string check)
        {
            Trace.WriteLine("Checking data name \"" + check + "\" for duplicate");
            if (!Wiki.Exists(duplicate => duplicate.GetName().Equals(check, StringComparison.OrdinalIgnoreCase)))
            {
                Trace.WriteLine("ValidName() returns true, no duplicate found");
                Trace.WriteLine("");
                return true;
            }
            else
            {
                Trace.WriteLine("ValidName() returns false, duplicate found");
                Trace.WriteLine("");
                ErrorMessage(0);
                return false;
            }
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
                case 3: // Search button clicked with no text
                    toolStripStatusLabel.Text = "ERROR: Please enter a search target";
                    break;
                case 4: // Save button clicked with empty List
                    toolStripStatusLabel.Text = "ERROR: Please enter a data item";
                    break;
                case 5: // Load function cannot find "wiki.bin" file
                    toolStripStatusLabel.Text = "ERROR: Could not find file \"wiki.bin\"";
                    break;
                default:
                    break;
            }
        }

        private void SetToolTips()
        {
            toolTip.SetToolTip(buttonAdd, "Enter data in all four fields to add it to the list");
            toolTip.SetToolTip(buttonEdit, "Select an item from the list to edit its data");
            toolTip.SetToolTip(buttonDelete, "Select an item from the list to delete it");
            toolTip.SetToolTip(buttonLoad, "Load data from file \"wiki.bin\"");
            toolTip.SetToolTip(buttonSave, "Save data to file \"wiki.bin\"");
            toolTip.SetToolTip(buttonSearch, "Enter a data structure name in the textbox to search");
            toolTip.SetToolTip(textBoxName, "Double-click to clear");
            toolTip.SetToolTip(textBoxDefinition, "Double-click to clear");
            toolTip.SetToolTip(textBoxSearch, "Double-click to clear");
        }
        #endregion VALID NAME / CHECKS / UTILITIES

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
            ResetColours();
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
            ResetColours();
            if (ListViewSelected())
            {
                DialogResult editChoice = MessageBox.Show("Do you wish to edit this entry?",
                "Edit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (editChoice == DialogResult.Yes)
                {
                    // Alternate method
                    //Wiki.RemoveAt(listView.SelectedIndices[0]);
                    //AddEntry();

                    int index = listView.SelectedIndices[0];
                    Wiki[index].SetName(textBoxName.Text);
                    Wiki[index].SetCategory(comboBoxCategory.Text);
                    Wiki[index].SetStructure(RadioButtonString());
                    Wiki[index].SetDefinition(textBoxDefinition.Text);
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

        // Q6.10 Create a button method that will use the built-in binary search to find a
        // Data Structure name. If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView. At the end
        // of the search process the search input TextBox must be cleared.
        #region BINARY SEARCH    
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ResetColours();
            listView.SelectedItems.Clear();
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                ResetColours();
                Information target = new Information();
                target.SetName(textBoxSearch.Text);
                int search = Wiki.BinarySearch(target);
                Trace.WriteLine("Searching for target \"" + target.GetName() + "\"");
                Trace.WriteLineIf(search >= 0, "Search function returns index " + search + ", target found");
                Trace.WriteLineIf(search < 0, "Search function returns index " + search + ", target not found");
                Trace.WriteLine("Exiting search");
                Trace.WriteLine("");
                if (search >= 0)
                {
                    listView.Focus();
                    listView.Items[search].BackColor = Color.Blue;
                    listView.Items[search].ForeColor = Color.White;
                    ShowDataFields(search);
                    toolStripStatusLabel.Text = "Search target \"" + textBoxSearch.Text + "\" found";
                }
                else
                    toolStripStatusLabel.Text = "Search target \"" + textBoxSearch.Text + "\" not found";

                textBoxSearch.Clear();
            }
            else
            {
                ErrorMessage(3);
            }
        }
        #endregion BINARY SEARCH

        // Q6.11 Create a ListView event so a user can select a Data Structure Name from the
        // list of Names and the associated information will be displayed in the related text
        // boxes combo box and radio button.
        #region LISTVIEW CLICK/SHOW DATA
        private void listView_Click(object sender, EventArgs e)
        {
            int index = listView.SelectedIndices[0];
            ShowDataFields(index);
            toolStripStatusLabel.Text = "Showing data for " + Wiki[index].GetName();
            ResetColours();
        }
        // Displays Name, Category, Structure and Definition of selected item in associated textboxes
        public void ShowDataFields(int index)
        {
            textBoxName.Text = Wiki[index].GetName();
            comboBoxCategory.SelectedItem = Wiki[index].GetCategory();
            if (Wiki[index].GetStructure() == "Linear")
                RadioButtonHighlight(0);
            else
                RadioButtonHighlight(1);
            textBoxDefinition.Text = Wiki[index].GetDefinition();
        }
        #endregion LISTVIEW CLICK/SHOW DATA

        // Q6.12 Create a custom method that will clear and reset the Textboxes, ComboBox
        // and Radio button
        #region CLEAR/RESET
        public void ClearAndReset()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            textBoxSearch.Clear();
            comboBoxCategory.SelectedItem = null;
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
        }

        // Resets Listview highlighting to default
        public void ResetColours()
        {
            for (int i = 0; i < Wiki.Count; i++)
            {
                listView.Items[i].BackColor = Color.White;
                listView.Items[i].ForeColor = Color.Black;
            }
        }
        #endregion CLEAR/RESET

        // Q6.13 Create a double click event on the Name TextBox to clear the TextBboxes,
        // ComboBox and Radio button.
        #region DOUBLE CLICK DISPLAY
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearAndReset();
            ResetColours();
        }

        private void textBoxDefinition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearAndReset();
            ResetColours();
        }

        private void textBoxSearch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearAndReset();
            ResetColours();
        }
        #endregion DOUBLE CLICK DISPLAY

        // Q6.14 Create two buttons for the manual open and save option; this must use a dialog
        // box to select a file or rename a saved file. All Wiki data is stored/retrieved using
        // a binary file format.
        #region SAVE/LOAD
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveRecords();
        }
        private void SaveRecords()
        {
            if (Wiki.Count > 0)
            {
                Trace.WriteLine("Running SaveRecords() method");
                Trace.WriteLine("Creating file \"wiki.bin\"");
                using (var stream = File.Open("wiki.bin", FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        foreach (var item in Wiki)
                        {
                            Trace.WriteLine("Stream position: " + stream.Position);
                            writer.Write(item.GetName());
                            writer.Write(item.GetCategory());
                            writer.Write(item.GetStructure());
                            writer.Write(item.GetDefinition());
                            Trace.WriteLine("Writing item \"" + item.GetName() + "\" to file");
                        }
                    }
                }
                Trace.WriteLine("All items saved");
                toolStripStatusLabel.Text = "Saved to file \"wiki.bin\"";
            }
            else
                ErrorMessage(4);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            LoadRecords();
            SortAndDisplay();
        }
        private void LoadRecords()
        {
            if (File.Exists("wiki.bin"))
            {
                using (var stream = File.Open("wiki.bin", FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                            Information load = new Information();
                            load.SetName(reader.ReadString());
                            load.SetCategory(reader.ReadString());
                            load.SetStructure(reader.ReadString());
                            load.SetDefinition(reader.ReadString());
                            Wiki.Add(load);
                        }
                    }
                }
                toolStripStatusLabel.Text = "Loaded entries from file \"wiki.bin\"";
            }
            else
            {
                ErrorMessage(5);
            }
        }
        #endregion SAVE/LOAD

        // Q6.15 The Wiki application will save data when the form closes.
        #region FORM CLOSED SAVE
        private void DataStructureList_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveRecords();
        }
        #endregion FORM CLOSED SAVE
    }
}
