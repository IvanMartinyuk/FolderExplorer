import { useEffect, useState } from 'react';
import { FolderService } from '../FolderService';
import Folder from './Folder';

function FolderBlock() {
    const [folders, setFolders] = useState();
    const [backLink, setBack] = useState();
    const [isRoot, setIsRoot] = useState();

    let service = new FolderService();
    useEffect(() => {
        if(window.location.pathname == '/')
            setIsRoot(true)
        else
            setIsRoot(false)
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
    
    function getBack() {
        let folders = window.location.pathname.split('/');
        let path = '';
        console.log(folders.length)
        for(let i = 0; i < folders.length - 1; i++)
        {
            if(i + 1 != folders.length - 1)
                path += folders[i] + '/';
            else
                path += folders[i];
        }
        if(folders.length == 2)
            path = window.location.origin;
        setBack(path)
    }
    
    return (
        <div className="flex">
            {   !isRoot&&
                <a onClick={() => getBack()} href={backLink}>Back</a>
                }
            <div className='folderBlock flex' onClick={() => setIsRoot(false)}>
                {folders}
            </div>
        </div>
    );
}

export default FolderBlock;