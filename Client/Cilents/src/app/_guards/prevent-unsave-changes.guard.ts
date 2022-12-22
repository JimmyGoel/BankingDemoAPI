import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ignoreElements, Observable } from 'rxjs';
import { MembersEditComponent } from '../members/members-edit/members-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsaveChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: MembersEditComponent):boolean {
      if(component.EditForm.dirty){
        return confirm("Are you sure want to continue? Any unsaved changes will be lost");
      }
    return true;
  }
  
}
