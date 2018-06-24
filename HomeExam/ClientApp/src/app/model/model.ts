export interface SaveProject {
  id: number;
  name: string;
  startDate: string;
  endDate: string;
  contacts: number[];
}

export interface Project {
    id: number;
  name: string;
  startDate: Date;
  endDate: Date;
  contacts: Contact[];
}

export interface Contact {
  id: number;
  name: string;
  phone: string;
  email: string;
}

export interface QueryResult {
  totalCount: number;
  items: any[];
}
