import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent implements OnInit {

  constructor(
    private projectService: ProjectService
  ) { }
  private readonly PAGE_SIZE = 5;

  queryResult = {};

  query: any = {
    pageSize: this.PAGE_SIZE
  };
  columns = [
    { title: 'Id' },
    { title: 'Name', key: 'name', isSortable: true },
    { title: 'Start Date', key: 'startDate', isSortable: true },
    { title: 'End Date', key: 'endDate', isSortable: true },
    { title: '# of Contacts', key: 'contacts', isSortable: true },
    {}
  ];

  ngOnInit() {
    this.populateProjects();
  }

  private populateProjects() {
    this.projectService.getProjects(this.query)
      .subscribe(
        result => {
          this.queryResult = result;
        }
      );
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateProjects();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy === columnName)
      this.query.isSortAscending = !this.query.isSortAscending;
    else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }
    this.populateProjects();
  }

  onPageChange(page: number) {
    this.query.page = page;
    this.populateProjects();
  }

}
