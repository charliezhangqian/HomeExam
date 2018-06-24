import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ContactService } from '../services/contact.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Contact } from '../model/model';

@Component({
  selector: 'app-contact-form',
  templateUrl: './contact-form.component.html',
  styleUrls: ['./contact-form.component.css']
})
export class ContactFormComponent implements OnInit {
  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private contactService: ContactService,
    private modalService: BsModalService
  ) {
    activatedRoute.params.subscribe(
      params => {
        this.contact.id = +params['id'] || 0;
      }
    );
  }
  modalRef: BsModalRef;
  contact: Contact = {
    id: 0,
    name: '',
    phone: '',
    email: ''
  };

  ngOnInit() {
    if (this.contact.id) {
      this.contactService.getContact(this.contact.id).subscribe(
        (c: Contact) => {
          this.setContact(c);
        },
        error => {
          if (error.status == 404)
            this.router.navigate(['/home']);
        });
    }
  }

  setContact(c: Contact) {
    this.contact.id = c.id;
    this.contact.name = c.name;
    this.contact.phone = c.phone;
    this.contact.email = c.email;
  }

  submit() {
    const result$ = (this.contact.id) ? this.contactService.update(this.contact) : this.contactService.create(this.contact);
    result$.subscribe(
      x => {
        //this.toastyService.success({
        //  title: 'Success',
        //  msg: 'The contact was succesfully updated.',
        //  theme: 'bootstrap',
        //  showClose: true,
        //  timeout: 500000
        //});
        this.router.navigate(['/contacts']);
      });
  }

  delete(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  confirm(): void {
    this.contactService.delete(this.contact.id)
      .subscribe(
        x => this.router.navigate(['/contacts'])
      );
    this.modalRef.hide();
  }

  decline(): void {
    this.modalRef.hide();
  }

}
