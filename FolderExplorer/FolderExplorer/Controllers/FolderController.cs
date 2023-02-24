using FolderExplorerDAL.Context;
using FolderExplorerDAL.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FolderExplorer.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllowOrigin")]
    public class FolderController : Controller
    {
        readonly FolderRepository _repository;
        public FolderController(FolderExplorerContext context)
        {
            _repository = new FolderRepository(context);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _repository.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id = 0)
        {
            if (_repository.GetCount() == 0)
                return NotFound(new { error = "DB is empty" });
            return id == 0 ? Json(_repository.GetFirst()) 
                           : Json(await _repository.GetAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetChildren(int id)
        {
            if (_repository.GetCount() == 0)
                return NotFound(new { error = "DB is empty" });
            var children = _repository.GetChildren(id);
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
            int i = 1;
            Folder rootFolder = _repository.GetRootFolders()
                                          .FirstOrDefault(f => f.Name == folders[i++]);
            if (rootFolder == null)
                return NotFound(new { error = "invalid path" });
            rootFolder.Children = _repository.GetChildren(rootFolder.Id);
            for (; i < folders.Count; i++)
            {
                rootFolder = rootFolder.Children.FirstOrDefault(f => f.Name == folders[i]);                    
                if (rootFolder == null)
                    return NotFound(new { error = "invalid path" });
                rootFolder = _repository.GetWithChildren(rootFolder.Id);
            }
            rootFolder.Children = null;
            rootFolder.Parent = null;
            rootFolder.ParentId = 0;
            return Json(rootFolder);
        }
        [HttpGet]
        public async Task<IActionResult> GetRoot() => Json(_repository.GetRootFolders());
        [HttpPost]
        public async Task<IActionResult> SetRange([FromBody] List<Folder> folders)
        {
            if (folders.FirstOrDefault(x => x.ParentId == null) != null)
            {
                await _repository.RemoveAllAsync();
                await _repository.AddRangeAsync(folders);
                await _repository.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }
        }
    }
}
