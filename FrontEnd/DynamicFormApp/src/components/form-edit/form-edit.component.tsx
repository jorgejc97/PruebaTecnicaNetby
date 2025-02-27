import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  fetchFormById,
  updateForm,
  deleteForm,
  createForm,
  addResponse,
  getResponses,
} from "../../services/api";
import FormInputListComponent from "../form-input-list/form-input-list.component";
import FormInputEditComponent from "../form-input-edit/form-input-edit.component";
import { Form } from "../../types/form";
import "./form-edit.component.css";

function FormEditComponent() {
  const { formId } = useParams<{ formId: string }>();
  const [form, setForm] = useState<Form | null>(null);
  const [isNew, setIsNew] = useState(formId === "new");
  const [showEditModal, setShowEditModal] = useState(formId === "new");
  const [showResponsesModal, setShowResponsesModal] = useState(false);
  const [responses, setResponses] = useState<
    { id: number; responseData: string }[]
  >([]);
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
        alert("Error al eliminar el formulario");
      }
    }
  };

  const handleAddResponse = async (responseData: Record<string, string>) => {
    try {
      await addResponse(Number(formId), JSON.stringify(responseData));
      alert("Respuesta guardada con éxito");
    } catch (error) {
      console.error("Error adding response:", error);
      alert("Error al guardar la respuesta");
    }
  };

  const handleShowResponses = async () => {
    try {
      const data = await getResponses(Number(formId));
      setResponses(data);
      setShowResponsesModal(true);
    } catch (error) {
      console.error("Error fetching responses:", error);
      alert("Error al consultar respuestas");
    }
  };

  if (!form) return <div>Cargando...</div>;

  return (
    <div className="form-edit">
      {!isNew && (
        <>
          <h1>{form.name}</h1>
          <FormInputListComponent
            fields={form.fields}
            onSubmit={handleAddResponse}
          />
          <div className="actions">
            <button onClick={() => setShowEditModal(true)}>Modificar</button>
            <button onClick={handleDelete}>Eliminar</button>
            <button onClick={handleShowResponses}>Consultar</button>
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
      {showResponsesModal && (
        <div className="modal">
          <div className="modal-content">
            <h2>Respuestas de {form.name}</h2>
            <div className="modal-body">
              <ul className="response-list">
                {responses.map((response) => {
                  const data = JSON.parse(response.responseData);
                  return (
                    <li key={response.id}>
                      {Object.entries(data).map(([key, value]) => (
                        <div key={key}>{`${key}: ${value}`}</div>
                      ))}
                    </li>
                  );
                })}
              </ul>
            </div>
            <div className="modal-actions">
              <button onClick={() => setShowResponsesModal(false)}>
                Cerrar
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default FormEditComponent;
