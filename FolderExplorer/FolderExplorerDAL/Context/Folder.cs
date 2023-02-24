using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorerDAL.Context
{
    [Serializable]
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Folder? Parent { get; set; }
        public virtual IEnumerable<Folder>? Children { get; set; }
    }
}
