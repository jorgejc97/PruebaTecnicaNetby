import { useState } from "react";
import { FormField } from "../../types/form";
import "./form-input-list.component.css";

interface FormInputListProps {
  fields: FormField[];
  onSubmit?: (responseData: Record<string, string>) => void;
}

function FormInputListComponent({ fields, onSubmit }: FormInputListProps) {
  const initialValues = fields.reduce(
    (acc, field) => ({
      ...acc,
      [field.label]: field.fieldType === "checkbox" ? "false" : "",
    }),
    {} as Record<string, string>
  );

  const [values, setValues] = useState<Record<string, string>>(initialValues);

  const handleChange = (label: string, value: string) => {
    setValues((prev) => ({ ...prev, [label]: value }));
  };

  const isValid = () => {
    return fields
      .filter((field) => field.fieldType !== "checkbox")
      .every((field) => {
        const value = values[field.label];
        return value !== undefined && value.trim() !== "";
      });
  };

  const handleSubmit = () => {
    if (!isValid()) {
      alert("Debe llenar todos los campos de texto");
      return;
    }
    if (onSubmit) {
      const responseData = Object.fromEntries(
        Object.entries(values).map(([label, value]) => {
          const field = fields.find((f) => f.label === label);
          return [
            label,
            field?.fieldType === "checkbox"
              ? value === "true"
                ? "Sí"
                : "No"
              : value,
          ];
        })
      );
      onSubmit(responseData);
      setValues(initialValues);
    }
  };

  return (
    <div className="form-input-list">
      {fields.map((field) => (
        <div key={field.id} className="field">
          <label>{field.label}: </label>
          <input
            type={field.fieldType === "checkbox" ? "checkbox" : field.fieldType}
            value={
              field.fieldType !== "checkbox"
                ? values[field.label] || ""
                : undefined
            }
            checked={
              field.fieldType === "checkbox"
                ? values[field.label] === "true"
                : undefined
            }
            onChange={(e) =>
              handleChange(
                field.label,
                field.fieldType === "checkbox"
                  ? e.target.checked.toString()
                  : e.target.value
              )
            }
            className={
              field.fieldType !== "checkbox" &&
              values[field.label]?.trim() === ""
                ? "input-error"
                : ""
            }
          />
        </div>
      ))}
      {onSubmit && (
        <button onClick={handleSubmit} disabled={!isValid()}>
          Ingresar
        </button>
      )}
    </div>
  );
}

export default FormInputListComponent;
