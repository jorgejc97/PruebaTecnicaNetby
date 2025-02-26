export interface FormField {
  id: number;
  label: string;
  fieldType: string;
}

export interface Form {
  id: number;
  name: string;
  fields: FormField[];
}
