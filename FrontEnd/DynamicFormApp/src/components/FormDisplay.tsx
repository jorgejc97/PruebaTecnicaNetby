import { useState } from "react";
import { Form, FormField } from "../types/form";
import { addField, updateField, deleteField } from "../services/api";

interface FormDisplayProps {
  form: Form;
}

function FormDisplay({ form }: FormDisplayProps) {
  const [newFieldLabel, setNewFieldLabel] = useState("");
  const [newFieldType, setNewFieldType] = useState("text");

  const handleAddField = async () => {
    try {
      const newField = await addField(form.id, {
        label: newFieldLabel,
        fieldType: newFieldType,
      });
      // Actualizamos el form en el padre o refetch aquí si necesario
      setNewFieldLabel("");
      setNewFieldType("text");
    } catch (error) {
      console.error("Error adding field:", error);
    }
  };

  const handleUpdateField = async (fieldId: number) => {
    const updatedLabel = prompt(
      "Nuevo nombre del campo:",
      form.fields.find((f) => f.id === fieldId)?.label
    );
    if (updatedLabel) {
      try {
        await updateField(form.id, fieldId, {
          label: updatedLabel,
          fieldType: newFieldType,
        });
        // Actualizamos el form en el padre o refetch aquí si necesario
      } catch (error) {
        console.error("Error updating field:", error);
      }
    }
  };

  const handleDeleteField = async (fieldId: number) => {
    try {
      await deleteField(form.id, fieldId);
      // Actualizamos el form en el padre o refetch aquí si necesario
    } catch (error) {
      console.error("Error deleting field:", error);
    }
  };

  return (
    <div className="form-display">
      <h2>{form.name}</h2>
      {form.fields.map((field) => (
        <div key={field.id} className="field">
          <label>{field.label}: </label>
          <input type={field.fieldType} />
          <button onClick={() => handleUpdateField(field.id)}>Editar</button>
          <button onClick={() => handleDeleteField(field.id)}>Eliminar</button>
        </div>
      ))}
      <div className="add-field">
        <input
          type="text"
          value={newFieldLabel}
          onChange={(e) => setNewFieldLabel(e.target.value)}
          placeholder="Etiqueta del campo"
        />
        <select
          value={newFieldType}
          onChange={(e) => setNewFieldType(e.target.value)}
        >
          <option value="text">Texto</option>
          <option value="number">Número</option>
          <option value="date">Fecha</option>
        </select>
        <button onClick={handleAddField}>Agregar Campo</button>
      </div>
    </div>
  );
}

export default FormDisplay;
