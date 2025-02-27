import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { fetchForms } from "../../services/api";
import { FormSummary } from "../../types/form";
import "./form-list.component.css";

function FormListComponent() {
  const [forms, setForms] = useState<FormSummary[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    const loadForms = async () => {
      try {
        const data = await fetchForms();
        setForms(data);
      } catch (error) {
        console.error("Error fetching forms:", error);
      }
    };
    loadForms();
  }, []);

  const handleFormSelect = (formId: number) => {
    navigate(`/edit/${formId}`);
  };

  const handleAddForm = () => {
    navigate("/edit/new");
  };

  return (
    <div className="form-list">
      <h1>Formularios</h1>
      {forms.map((form) => (
        <button key={form.id} onClick={() => handleFormSelect(form.id)}>
          {form.name}
        </button>
      ))}
      <button onClick={handleAddForm}>Agregar Formulario</button>
    </div>
  );
}

export default FormListComponent;
