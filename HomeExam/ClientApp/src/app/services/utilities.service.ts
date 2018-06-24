import { Injectable } from '@angular/core';

@Injectable()
export class UtilitiesService {

  constructor() { }

  toQueryString(obj: any) {
    const part: any[] = [];
    for (let property in obj) {
      if (obj.hasOwnProperty(property)) {
        const value = obj[property];
        if (value != null) {
          part.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
        }
      }
    }
    return part.join('&');
  }
}
