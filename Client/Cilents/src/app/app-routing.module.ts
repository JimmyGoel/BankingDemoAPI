import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MembersEditComponent } from './members/members-edit/members-edit.component';
import { MessagesComponent } from './messages/messages.component';
import { NotFoundComponent } from './_errors/not-found/not-found.component';
import { ServerErrorComponent } from './_errors/server-error/server-error.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsaveChangesGuard } from './_guards/prevent-unsave-changes.guard';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path:'',
    runGuardsAndResolvers:'always',
    canActivate:[AuthGuard],
    children:[
      { path: 'members', component: MemberListComponent},
      { path: 'members/:id', component: MemberDetailComponent },
      { path: 'member/edit', component: MembersEditComponent,canDeactivate:[PreventUnsaveChangesGuard] },
      { path: 'list', component: ListsComponent },
      { path: 'messages', component: MessagesComponent },
    ]
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: '**', component: NotFoundComponent, pathMatch: 'full' },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
