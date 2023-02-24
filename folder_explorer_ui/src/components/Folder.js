import icon from './folder.png'

function Folder(props) {
    return(
        <a className="folderBox" href={props.path} onClick={() => sessionStorage.setItem('isClick', true)}>
            <div>
                <img src={icon} className='imageDiv'></img>
            </div>
            <h2 className='folderName'>{props.name}</h2>
        </a>
    );
}

export default Folder;