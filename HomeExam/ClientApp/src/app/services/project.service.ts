import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from './utilities.service';
import { SaveProject } from '../model/model';

@Injectable()
export class ProjectService {
  private readonly endPoint = '/api/projects';

  constructor(
    private readonly httpClient: HttpClient,
    private readonly utils: UtilitiesService
  ) { }

  getProjects(filter: any) {
    return this.httpClient.get(this.endPoint + '?' + this.utils.toQueryString(filter));
  }

  getProject(id: number) {
    return this.httpClient.get(this.endPoint + '/' + id);
  }

  delete(id: number) {
    return this.httpClient.delete(this.endPoint + '/' + id);
  }

  create(project: SaveProject) {
    return this.httpClient.post(this.endPoint, project);
  }

  update(project: SaveProject) {
    return this.httpClient.put(this.endPoint + '/' + project.id, project);
  }
}
