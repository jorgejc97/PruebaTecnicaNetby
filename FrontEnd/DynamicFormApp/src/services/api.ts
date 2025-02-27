import axios from "axios";
import { Form, FormSummary, FormField } from "../types/form";

const API_URL = "https://localhost:7201";

export const fetchForms = async (): Promise<FormSummary[]> => {
  const response = await axios.get(`${API_URL}/forms`);
  return response.data;
};

export const fetchFormById = async (id: number): Promise<Form> => {
  const response = await axios.get(`${API_URL}/forms/${id}`);
  return response.data;
};

export const createForm = async (form: {
  name: string;
  fields: FormField[];
}): Promise<Form> => {
  const response = await axios.post(`${API_URL}/forms`, form);
  return response.data;
};

export const updateForm = async (
  id: number,
  form: { name: string; fields: FormField[] }
): Promise<void> => {
  await axios.put(`${API_URL}/forms/${id}`, form);
};

export const deleteForm = async (id: number): Promise<void> => {
  await axios.delete(`${API_URL}/forms/${id}`);
};

export const addResponse = async (
  formId: number,
  responseData: string
): Promise<{ id: number; responseData: string }> => {
  const response = await axios.post(`${API_URL}/forms/${formId}/responses`, {
    responseData,
  });
  return response.data;
};

export const getResponses = async (
  formId: number
): Promise<{ id: number; responseData: string }[]> => {
  const response = await axios.get(`${API_URL}/forms/${formId}/responses`);
  return response.data;
};
