import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  fetchFormById,
  updateForm,
  deleteForm,
  createForm,
} from "../../services/api";
import { Form } from "../../types/form";
import "./form-edit.component.css";
import FormInputListComponent from "../form-input-list/form-input-list.component";
import FormInputEditComponent from "../form-input-edit/form-input-edit.component";

function FormEditComponent() {
  const { formId } = useParams<{ formId: string }>();
  const [form, setForm] = useState<Form | null>(null);
  const [isNew, setIsNew] = useState(formId === "new");
  const [showEditModal, setShowEditModal] = useState(formId === "new");
  const navigate = useNavigate();

  useEffect(() => {
    if (!isNew) {
      const loadForm = async () => {
        try {
          const data = await fetchFormById(Number(formId));
          setForm(data);
        } catch (error) {
          console.error("Error fetching form:", error);
        }
      };
      loadForm();
    } else {
      setForm({ id: 0, name: "", fields: [] });
    }
  }, [formId, isNew]);

  const handleDelete = async () => {
    if (window.confirm("¿Seguro que quieres eliminar este formulario?")) {
      try {
        await deleteForm(Number(formId));
        navigate("/");
      } catch (error) {
        console.error("Error deleting form:", error);
      }
    }
  };

  if (!form) return <div>Cargando...</div>;

  return (
    <div className="form-edit">
      {!isNew && (
        <>
          <h1>{form.name}</h1>
          <FormInputListComponent fields={form.fields} />
          <div className="actions">
            <button onClick={() => setShowEditModal(true)}>Modificar</button>
            <button onClick={handleDelete}>Eliminar</button>
            <button onClick={() => navigate("/")}>Volver</button>
          </div>
        </>
      )}
      {showEditModal && (
        <FormInputEditComponent
          form={form}
          onClose={() => {
            setShowEditModal(false);
            if (isNew) navigate("/");
          }}
          onSave={async (updatedForm) => {
            if (isNew) {
              const newForm = await createForm(updatedForm);
              setForm(newForm);
              navigate(`/edit/${newForm.id}`);
            } else {
              await updateForm(form.id, updatedForm);
              setForm(updatedForm);
            }
            setShowEditModal(false);
            setIsNew(false);
          }}
        />
      )}
    </div>
  );
}

export default FormEditComponent;
