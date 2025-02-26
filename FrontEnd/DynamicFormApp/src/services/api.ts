import axios from "axios";
import { Form, FormField } from "../types/form";

const API_URL = "https://localhost:7201";

export const fetchForms = async (): Promise<Form[]> => {
  const response = await axios.get(`${API_URL}/forms`);
  return response.data;
};

export const fetchFormById = async (id: number): Promise<Form> => {
  const response = await axios.get(`${API_URL}/forms/${id}`);
  return response.data;
};

export const addField = async (
  formId: number,
  field: Omit<FormField, "id">
): Promise<FormField> => {
  const response = await axios.post(`${API_URL}/forms/${formId}/fields`, field);
  return response.data;
};

export const updateField = async (
  formId: number,
  fieldId: number,
  field: Omit<FormField, "id">
): Promise<void> => {
  await axios.put(`${API_URL}/forms/${formId}/fields/${fieldId}`, field);
};

export const deleteField = async (
  formId: number,
  fieldId: number
): Promise<void> => {
  await axios.delete(`${API_URL}/forms/${formId}/fields/${fieldId}`);
};
