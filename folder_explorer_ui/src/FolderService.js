export class FolderService {
    baseUrl = 'https://localhost:7071/folder/';
    async getRoot() {
        let response = await fetch(this.baseUrl + 'GetRoot');
        let data = await response.json();
        return data;
    }

    async getByPath(path) {
        let response = await fetch(this.baseUrl + 'GetByPath?path=' + path);
        let data = await response.json();
        response = await fetch(this.baseUrl + 'GetChildren?id=' + data.id);
        data = await response.json();
        return data;
    }
    async getAll() {
        let response = await fetch(this.baseUrl + 'GetAll');
        let data = await response.json();
        return data;
    }
    async setRange(range) {
        let response = await fetch(this.baseUrl+'SetRange' ,{
            method:"POST",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(range)
        })
        return response;
    }
}