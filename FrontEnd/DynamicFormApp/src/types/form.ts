export interface FormField {
  id?: number;
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
