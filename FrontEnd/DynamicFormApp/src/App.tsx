import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import FormListComponent from "./components/form-list/form-list.component";
import FormEditComponent from "./components/form-edit/form-edit.component";
import "./App.css";

function App() {
  return (
    <Router>
      <div className="app">
        <Routes>
          <Route path="/" element={<FormListComponent />} />
          <Route path="/edit/:formId" element={<FormEditComponent />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
