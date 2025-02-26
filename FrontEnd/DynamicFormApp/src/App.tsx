import { useState } from "react";
import FormList from "./components/FormList";
import FormDisplay from "./components/FormDisplay";
import { Form } from "./types/form";
import "./App.css";

function App() {
  const [selectedForm, setSelectedForm] = useState<Form | null>(null);

  const handleFormSelect = (form: Form) => {
    setSelectedForm(form);
  };

  return (
    <div className="app">
      <h1>Formularios Dinámicos</h1>
      <FormList onFormSelect={handleFormSelect} />
      {selectedForm && <FormDisplay form={selectedForm} />}
    </div>
  );
}

export default App;
