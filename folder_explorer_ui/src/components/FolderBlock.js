import { useEffect, useState } from 'react';
import { FolderService } from '../FolderService';
import Folder from './Folder';

function FolderBlock() {
    const [folders, setFolders] = useState();
    let service = new FolderService();
    useEffect(() => {
        sessionStorage.setItem('isClick', false);
        if(window.location.pathname == '/')
        {
            service.getRoot().then(data => {
            let foldersViews = [];
            data.forEach(folder => {
                foldersViews.push(
                    <Folder name={ folder.name }
                            path={ window.location.href + folder.name }></Folder>
                )
                });
                setFolders(foldersViews);
            })
        }
        else
            service.getByPath(window.location.pathname).then(data => {
                let foldersViews = [];
                data.forEach(folder => {
                    foldersViews.push(
                        <Folder name={ folder.name }
                                path={ window.location.pathname + '/' + folder.name }></Folder>
                    )
                    });
                    setFolders(foldersViews);
                });            
    }, [sessionStorage.getItem('isClick')]);
    
    
    return (
        <div className="flex">
            <div className='folderBlock flex'>
                {folders}
            </div>
        </div>
    );
}

// function AddPath(path)
// {
//     sessionStorage.setItem("path", sessionStorage.getItem('path') + path + "/");
// }

// function RemovePath()
// {
//     let folders = sessionStorage.getItem('path').split('/');
//     let path = '';
//     for(let i = 0; i < folders.length - 1; i++)
//         path += folders[i] + '/';
//     sessionStorage.setItem("path", path);
// }

export default FolderBlock;