import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

import { ProjectService } from '../services/project.service';
import { SaveProject, Contact, Project } from '../model/model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.css']
})
export class ProjectFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
    private datePipe: DatePipe,
    private modalService: BsModalService
  ) {
    activatedRoute.params.subscribe(
      params => {
        this.project.id = +params['id'] || 0;
      }
    );
  }
  modalRef: BsModalRef;
  project: SaveProject = {
    id: 0,
    name: '',
    startDate: '',
    endDate: '',
    contacts: []
  };

  contacts: Contact[] = [];

  selectedContacts: Contact[] = [];

  ngOnInit() {
    if (this.project.id) {
      this.projectService.getProject(this.project.id).subscribe(
        (p: Project) => {
          this.setProject(p);
          this.contacts = p.contacts.sort((a, b) => a.id > b.id ? 1 : a.id < b.id ? -1 : 0);
          this.resetSelection();
        },
        error => {
          if (error.status == 404)
            this.router.navigate(['/home']);
        });
    }
  }

  setProject(p: Project) {
    this.project.id = p.id;
    this.project.name = p.name;
    this.project.startDate = this.datePipe.transform(p.startDate, 'yyyy-MM-dd');
    this.project.endDate = this.datePipe.transform(p.endDate, 'yyyy-MM-dd');
    this.project.contacts = p.contacts.map(c => c.id);
  }

  deleteContact(contact: Contact) {
    this.contacts.splice(this.contacts.indexOf(contact));
    this.project.contacts.splice(this.project.contacts.indexOf(contact.id));
  }

  submit() {
    const result$ = (this.project.id) ? this.projectService.update(this.project) : this.projectService.create(this.project);
    result$.subscribe(
      x => {
        this.router.navigate(['/projects']);
      });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);

    this.modalService.onHide.subscribe(
      () => this.resetSelection()
    );
  }

  delete(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    this.projectService.delete(this.project.id)
      .subscribe(
        x => this.router.navigate(['/projects'])
      );
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

  resetSelection() {
    this.selectedContacts = [...this.contacts];
  }

  onConfirmSelection(selectedContacts: Contact[]) {
    this.contacts = selectedContacts;
    this.contacts.sort((a, b) =>  a.id > b.id ? 1 : a.id < b.id ? -1 : 0);
    this.project.contacts = selectedContacts.map(c => c.id);
    this.modalRef.hide();
  }
}
