import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { DatePipe } from '@angular/common';
import { ModalModule } from 'ngx-bootstrap/modal';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { ProjectListComponent } from './project-list/project-list.component';
import { PaginationComponent } from './shared/pagination.component';
import { ProjectService } from './services/project.service';
import { UtilitiesService } from './services/utilities.service';
import { ProjectFormComponent } from './project-form/project-form.component';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactService } from './services/contact.service';
import { ContactFormComponent } from './contact-form/contact-form.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    ProjectListComponent,
    PaginationComponent,
    ProjectFormComponent,
    ContactListComponent,
    ContactFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'projects', pathMatch: 'full' },
      { path: 'projects', component: ProjectListComponent },
      { path: 'projects/new', component: ProjectFormComponent },
      { path: 'projects/:id', component: ProjectFormComponent },
      { path: 'contacts', component: ContactListComponent },
      { path: 'contacts/new', component: ContactFormComponent },
      { path: 'contacts/:id', component: ContactFormComponent }
    ]),
    ToastyModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [
    ProjectService,
    ContactService,
    UtilitiesService,
    DatePipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
