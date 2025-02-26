import { useState, useEffect } from "react";
import { fetchForms } from "../services/api";
import { Form } from "../types/form";

interface FormListProps {
  onFormSelect: (form: Form) => void;
}

function FormList({ onFormSelect }: FormListProps) {
  const [forms, setForms] = useState<Form[]>([]);

  useEffect(() => {
    const loadForms = async () => {
      try {
        const data = await fetchForms();
        setForms(data);
      } catch (error) {
        console.error("Error loading forms:", error);
      }
    };
    loadForms();
  }, []);

  return (
    <div className="form-list">
      {forms.map((form) => (
        <button key={form.id} onClick={() => onFormSelect(form)}>
          {form.name}
        </button>
      ))}
    </div>
  );
}

export default FormList;
