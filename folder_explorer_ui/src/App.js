import './App.css';
import { HashRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import NavBar from './components/NavBar'
import FolderBlock from './components/FolderBlock';

function App() {
  return (
    <div className="mainBox">
        <NavBar></NavBar>
        <Router>
          <Routes>
              <Route path="/*" element={<FolderBlock/>} />
              <Route path="/fileTest/" element={<NavBar/>}></Route>
          </Routes>
      </Router>
    </div>
  );
}

export default App;
