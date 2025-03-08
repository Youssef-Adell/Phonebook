import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IContact, IPagedResponse } from './contact.model';
import { environment } from 'src/environments/environment.development';
=

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private apiUrl = environment.apiUrl + '/contact';

  constructor(private http: HttpClient) {
  }

  getContacts(search: string = '', page: number = 1, pageSize: number = 5): Observable<IPagedResponse<IContact>> {
    let params = new HttpParams()
      .set('search', search)
      .set('page', page)
      .set('pageSize', pageSize);
      
    return this.http.get<IPagedResponse<IContact>>(this.apiUrl, { params });
  }

  createContact(contact: IContact): Observable<IContact> {
    return this.http.post<IContact>(this.apiUrl, contact);
  }

  deleteContact(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
