import { useState, useEffect } from "react";
import { Form, FormField } from "../../types/form";
import "./form-input-edit.component.css";

interface FormInputEditProps {
  form: Form;
  onClose: () => void;
  onSave: (form: Form) => Promise<void>;
}

function FormInputEditComponent({ form, onClose, onSave }: FormInputEditProps) {
  const [name, setName] = useState(form.name);
  const [fields, setFields] = useState<FormField[]>(
    form.fields.length > 0 ? form.fields : [{ label: "", fieldType: "text" }]
  );

  useEffect(() => {
    if (form.id === 0 && form.fields.length === 0) {
      setFields([{ label: "", fieldType: "text" }]);
    }
  }, [form]);

  const handleAddField = () => {
    setFields([...fields, { label: "", fieldType: "text" }]);
  };

  const handleFieldChange = (
    index: number,
    key: keyof FormField,
    value: string
  ) => {
    const updatedFields = [...fields];
    updatedFields[index] = { ...updatedFields[index], [key]: value };
    setFields(updatedFields);
  };

  const handleNameChange = (value: string) => {
    setName(value);
  };

  const handleRemoveField = (index: number) => {
    setFields(fields.filter((_, i) => i !== index));
  };

  const isFormValid = () => {
    return (
      name.trim() !== "" &&
      fields.length > 0 &&
      fields.every((field) => field.label.trim() !== "")
    );
  };

  const handleSubmit = async () => {
    if (!isFormValid()) {
      alert("Debe llenar los campos");
      return;
    }
    const updatedForm: Form = { id: form.id, name, fields };
    try {
      await onSave(updatedForm);
      alert(
        form.id
          ? "Formulario modificado con éxito"
          : "Formulario creado con éxito"
      );
    } catch (error) {
      console.error("Error saving form:", error);
      alert("Error al crear/modificar el formulario");
    }
  };

  return (
    <div className="modal">
      <div className="modal-content">
        <h2>{form.id ? "Editar Formulario" : "Crear Formulario"}</h2>
        <div className="modal-body">
          <input
            type="text"
            value={name}
            onChange={(e) => handleNameChange(e.target.value)}
            placeholder="Nombre del formulario"
            className={name.trim() === "" ? "input-error" : ""}
          />
          {fields.map((field, index) => (
            <div key={index} className="field-edit">
              <input
                type="text"
                value={field.label}
                onChange={(e) =>
                  handleFieldChange(index, "label", e.target.value)
                }
                placeholder="Etiqueta"
                className={field.label.trim() === "" ? "input-error" : ""}
              />
              <select
                value={field.fieldType}
                onChange={(e) =>
                  handleFieldChange(index, "fieldType", e.target.value)
                }
              >
                <option value="text">Texto</option>
                <option value="number">Número</option>
                <option value="date">Fecha</option>
                <option value="checkbox">Checkbox</option>
              </select>
              <button onClick={() => handleRemoveField(index)}>Eliminar</button>
            </div>
          ))}
          <button onClick={handleAddField}>Agregar Campo</button>
        </div>
        <div className="modal-actions">
          <button onClick={handleSubmit} disabled={!isFormValid()}>
            Guardar
          </button>
          <button onClick={onClose}>Cancelar</button>
        </div>
      </div>
    </div>
  );
}

export default FormInputEditComponent;
