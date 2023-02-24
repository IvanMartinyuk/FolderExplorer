using FolderExplorerDAL.Context;
using FolderExplorerDAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FolderExplorer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FolderController : Controller
    {
        FolderRepository repository;
        public FolderController(FolderExplorerContext context)
        {
            repository = new FolderRepository(context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await repository.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id = 0)
        {
            if (repository.GetCount() == 0)
                return NotFound(new { error = "DB is empty" });
            return id == 0 ? Json(repository.GetFirst()) 
                           : Json(await repository.GetAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetChildren(int id)
        {
            if (repository.GetCount() == 0)
                return NotFound(new { error = "DB is empty" });
            var children = repository.GetChildren(id);
            foreach(var child in children)
            {
                child.Children = null;
                child.Parent = null;
                child.ParentId = 0;
            }
            return Json(children);
        }
        [HttpGet]
        public async Task<IActionResult> GetByPath(string path)
        {
            List<string> folders = path.Split('/').ToList();
            int i = 0;
            Folder rootFolder = repository.GetRootFolders()
                                          .FirstOrDefault(f => f.Name == folders[i++]);
            if (rootFolder == null)
                return NotFound(new { error = "invalid path" });
            rootFolder.Children = repository.GetChildren(rootFolder.Id);
            for (; i < folders.Count; i++)
            {
                rootFolder = rootFolder.Children.FirstOrDefault(f => f.Name == folders[i]);                    
                if (rootFolder == null)
                    return NotFound(new { error = "invalid path" });
                rootFolder = repository.GetWithChildren(rootFolder.Id);
            }
            rootFolder.Children = null;
            rootFolder.Parent = null;
            rootFolder.ParentId = 0;
            return Json(rootFolder);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoot() => Json(repository.GetRootFolders());
    }
}
