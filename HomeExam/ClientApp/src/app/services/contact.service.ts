import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { UtilitiesService } from './utilities.service';
import { Contact } from '../model/model';

@Injectable()
export class ContactService {

  private readonly endPoint = '/api/contacts';

  constructor(
    private readonly httpClient: HttpClient,
    private readonly utils: UtilitiesService
  ) { }

  getContacts(filter: any) {
    return this.httpClient.get(this.endPoint + '?' + this.utils.toQueryString(filter));
  }

  getContact(id: number) {
    return this.httpClient.get(this.endPoint + '/' + id);
  }

  delete(id: number) {
    return this.httpClient.delete(this.endPoint + '/' + id);
  }

  create(contact: Contact) {
    return this.httpClient.post(this.endPoint, contact);
  }

  update(contact: Contact) {
    return this.httpClient.put(this.endPoint + '/' + contact.id, contact);
  }
}
