import { FolderService } from "../FolderService";

function NavBar() {
    function handleClick() {
        let service = new FolderService();
        service.getAll().then(data => {
            data = JSON.stringify(data);
            const file = new Blob([data], { type: "application/json" });
            const fileURL = URL.createObjectURL(file);
            const a = document.createElement("a");
            a.href = fileURL;
            a.download = "folders.json";
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
            URL.revokeObjectURL(fileURL);
        })
      }

      function handleFileChange(event) {
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onload = handleFileRead;
        reader.readAsText(file);
      }

      function handleFileRead(event) {
        const content = event.target.result;
        const data = JSON.parse(content);
        let service = new FolderService();
        service.setRange(data).then(x => console.log(x))
      }

    return (
        <div className='navBar'>
            <a href='/' className='link'>Home</a>
            <button onClick={handleClick}>Export</button>
            <label class="custom-file-upload">
                <input type="file" onChange={handleFileChange}/>
                Import
            </label>
        </div>
    )
}

export default NavBar;