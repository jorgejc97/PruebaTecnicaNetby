export interface FormField {
  id?: number; // Opcional para creación
  label: string;
  fieldType: string;
}

export interface Form {
  id: number;
  name: string;
  fields: FormField[];
}

export interface FormSummary {
  id: number;
  name: string;
}
