using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructureList
{
    internal class Information
    {
        private string Name;
        private string Category;
        private string Structure;
        private string Definition;

        #region GETTERS/SETTERS
        public string getName()
        {
            return Name;
        }
        public void setName(string Name)
        {
            this.Name = Name;
        }
        public string getCategory()
        {
            return Category;
        }
        public void setCategory(string Category)
        {
            this.Category = Category;
        }
        public string getStructure()
        {
            return Structure;
        }
        public void setStructure(string Structure)
        {
            this.Structure = Structure;
        }
        public string getDefinition()
        {
            return Definition;
        }
        public void setDefinition(string Definition)
        {
            this.Definition = Definition;
        }
        #endregion GETTERS/SETTERS
    }
}
