import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ContactService } from '../services/contact.service';
import { Contact, QueryResult } from '../model/model';


@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit {
  @Input() mode = 'list';
  @Input('selected-contacts') selected: Contact[] = [];
  @Output('confirm') confirm = new EventEmitter();

  constructor(
    private contactService: ContactService
  ) { }
  private readonly PAGE_SIZE = 5;

  queryResult: QueryResult = {
    totalCount: 0,
    items: []
  };
  selectedIds = [];
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
    this.selectedIds = this.selected.map(c => c.id);
  }

  private populateContacts() {
    this.contactService.getContacts(this.query)
      .subscribe(
        (result: QueryResult)=> {
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

  toggleSelection(c: Contact) {
    const ind = this.selected.indexOf(c);

    if (ind !== -1) {
      this.selected.splice(ind);
      this.selectedIds.splice(ind);
    } else {
      this.selected.push(c);
      this.selectedIds.push(c.id);
    }
  }

  onConfirm() {
    const selectedContacts = this.selected;
    this.confirm.emit(selectedContacts);
  }
}
