using FolderExplorerDAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FolderExplorerDAL.Repositories
{
    public class FolderRepository : GenericRepository<Folder>
    {
        public FolderRepository(DbContext context) : base(context)
        {
        }
        public Folder GetFirst() => table.FirstOrDefault();
        public int GetCount() => table.Count();
        public Folder GetWithChildren(int id) => table.Include(f => f.Children)
                                                      .FirstOrDefault(f => f.Id == id);
        public List<Folder> GetChildren(int id) => table.Include(f => f.Children)
                                                        .FirstOrDefault(f => f.Id == id)
                                                        .Children
                                                        .ToList();
        public List<Folder> GetRootFolders() => table.Where(f => f.ParentId == null)
                                                     .ToList();
    }
}
