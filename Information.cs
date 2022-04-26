using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            Information otherInformation = obj as Information;
            if (otherInformation != null)
                return otherInformation.Name.CompareTo(Name);
            else
                return 0;
        }

        #region GETTERS/SETTERS
        public string getName()
        {
            return Name;
        }
        public void setName(string newName)
        {
            Name = newName;
        }
        public string getCategory()
        {
            return Category;
        }
        public void setCategory(string newCategory)
        {
            Category = newCategory;
        }
        public string getStructure()
        {
            return Structure;
        }
        public void setStructure(string newStructure)
        {
            Structure = newStructure;
        }
        public string getDefinition()
        {
            return Definition;
        }
        public void setDefinition(string newDefinition)
        {
            Definition = newDefinition;
        }
        #endregion GETTERS/SETTERS
    }
}
