// src/app/IContact.model.ts
export interface IContact {
    id?: number;
    name: string;
    phoneNumber: string;
    email: string;
  }
  
  export interface Metadata {
    currentPage: number;
    pageSize: number;
    totalItems: number;
  }
  
  export interface IPagedResponse<T> {
    data: T[];
    metadata: Metadata;
  }
  