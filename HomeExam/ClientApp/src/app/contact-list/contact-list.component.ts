import { Component, OnInit } from '@angular/core';
import { ContactService } from '../services/contact.service';


@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {

  constructor(
    private contactService: ContactService
  ) { }
  private readonly PAGE_SIZE = 5;

  queryResult = {};

  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Phone', key: 'phone', isSortable: true },
    { title: 'Email', key: 'email', isSortable: true },
    {}
  ];

  ngOnInit() {
    this.populateContacts();
  }

  private populateContacts() {
    this.contactService.getContacts(this.query)
      .subscribe(
        result => {
          this.queryResult = result;
        }
      );
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateContacts();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateContacts();
  }

  onPageChange(page: number) {
    this.query.page = page;
    this.populateContacts();
  }
}
