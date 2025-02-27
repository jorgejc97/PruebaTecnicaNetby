import { FormField } from "../../types/form";
import "./form-input-list.component.css";

interface FormInputListProps {
  fields: FormField[];
}

function FormInputListComponent({ fields }: FormInputListProps) {
  return (
    <div className="form-input-list">
      {fields.map((field) => (
        <div key={field.id} className="field">
          <label>{field.label}: </label>
          <input type={field.fieldType} />
        </div>
      ))}
    </div>
  );
}

export default FormInputListComponent;
