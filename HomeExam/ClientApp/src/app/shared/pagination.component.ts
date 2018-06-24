import {
  Component,
  Input,
  Output,
  EventEmitter
  } from '@angular/core';
import { OnChanges } from '@angular/core';

@Component({
  selector: 'pagination',
  template: `
    <nav *ngIf="totalItems > pageSize">
        <ul class="pagination">
            <li [class.disabled]="currentPage == 1">
                <a (click)="previous()" aria-label="Previous">
                <span aria-hidden="true">&laquo;</span>
                </a>
            </li>
            <li [class.active]="currentPage == page" *ngFor="let page of pages" (click)="changePage(page)">
                <a class="page-link">{{ page }}</a>
            </li>
            <li [class.disabled]="currentPage == pages.length">
                <a (click)="next()" aria-label="Next">
                <span aria-hidden="true">&raquo;</span>
                </a>
            </li>
        </ul>
    </nav>  
`
})
export class PaginationComponent implements OnChanges {
  @Input('total-items') totalItems: number = 0;
  @Input('page-size') pageSize = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages: number[] = [];
  currentPage = 1;

  ngOnChanges() {
    this.currentPage = 1;

    const pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];
    for (var i = 1; i <= pagesCount; i++)
      this.pages.push(i);
  }

  //changePage(page: number) {
  //  this.currentPage = page;
  //  this.pageChanged.emit(page);
  //}

  //previous() {
  //  if (this.currentPage == 1)
  //    return;

  //  this.currentPage--;
  //  this.pageChanged.emit(this.currentPage);
  //}

  //next() {
  //  if (this.currentPage == this.pages.length)
  //    return;

  //  this.currentPage++;
  //  this.pageChanged.emit(this.currentPage);
  //}
}
