using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.IComparable;

namespace DataStructureList
{
    [Serializable]
    internal class Information : IComparable
    {
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        // Default Constructor
        public Information()
        { }

        // Overloaded Constructor setting Name, Category, Structure
        // and Definition fields at object instantiation
        public Information(string NewName, string NewCategory, string NewStructure, string NewDefinition)
        {
            // Capitalizing first letter for Name property
            Name = NewName.Substring(0, 1).ToUpper() + NewName.Substring(1);
            Category = NewCategory;
            Structure = NewStructure;
            Definition = NewDefinition;
        }

        public int CompareTo(object obj)
        {
            Information compare = obj as Information;
            return Name.CompareTo(compare.Name);
        }

        #region GETTERS/SETTERS
        public string GetName()
        {
            return Name;
        }
        public void SetName(string NewName)
        {
            // Capitalizing first letter
            Name = NewName.Substring(0, 1).ToUpper() + NewName.Substring(1);
        }
        public string GetCategory()
        {
            return Category;
        }
        public void SetCategory(string NewCategory)
        {
            Category = NewCategory;
        }
        public string GetStructure()
        {
            return Structure;
        }
        public void SetStructure(string NewStructure)
        {
            Structure = NewStructure;
        }
        public string GetDefinition()
        {
            return Definition;
        }
        public void SetDefinition(string NewDefinition)
        {
            Definition = NewDefinition;
        }
        #endregion GETTERS/SETTERS

        // Custom method for displaying objects in ListView
        public ListViewItem Display()
        {
            ListViewItem lvi = new ListViewItem(GetName());
            lvi.SubItems.Add(GetCategory());
            return lvi;
        }
    }
}
