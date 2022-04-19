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
        #region ADD/EDIT/DELETE
        private void buttonAdd_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        #endregion ADD/EDIT/DELETE

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
        public bool ValidName()
        {
            return true;
            //return Wiki.Exists(textBoxName.Text);
        }

        // Q6.6 Create two methods to highlight and return the values from the Radio button
        // GroupBox. The first method must return a string value from the selected radio
        // button (Linear or Non-Linear). The second method must send an integer index which
        // will highlight an appropriate radio button.

        

        // Q6.7 Create a button method that will delete the currently selected record in the
        // ListView. Ensure the user has the option to backout of this action by using a
        // dialog box. Display an updated version of the sorted list at the end of this process.

        // Q6.8 Create a button method that will save the edited record of the currently
        // selected item in the ListView. All the changes in the input controls will be
        // written back to the list. Display an updated version of the sorted list at the
        // end of this process.

        // Q6.9 Create a single custom method that will sort and then display the Name and
        // Category from the wiki information in the list.

        // Q6.10 Create a button method that will use the builtin binary search to find a
        // Data Structure name. If the record is found the associated details will populate
        // the appropriate input controls and highlight the name in the ListView. At the end
        // of the search process the search input TextBox must be cleared.

        // Q6.11 Create a ListView event so a user can select a Data Structure Name from the
        // list of Names and the associated information will be displayed in the related text
        // boxes combo box and radio button.

        // Q6.12 Create a custom method that will clear and reset the Textboxes, ComboBox
        // and Radio button
        public void ClearReset()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            //comboBoxCategory.
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
        }

        // Q6.13 Create a double click event on the Name TextBox to clear the TextBboxes,
        // ComboBox and Radio button.
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ClearReset();
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

        private void buttonSearch_Click(object sender, EventArgs e)
        {

        }

    }
}
