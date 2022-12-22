import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Photo } from 'src/app/_Interface/Iphoto';
import { IUserEntity } from 'src/app/_Interface/IuserEntity';
import { IUserDetail } from 'src/app/_Interface/user';
import { AccountService } from 'src/app/_services/account.service';
import { UserentityService } from 'src/app/_services/userentity.service';
import { environment } from 'src/environments/environment';
import { pathToFileURL } from 'url';


@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css'],
  providers:[AccountService]
})
export class PhotoEditorComponent implements OnInit {
  @Input() userEntity: IUserEntity;
  User: IUserDetail;
  baseUrl = environment.apiURl;
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean = false;
  hasAnotherDropZoneOver: boolean;
  response: string;


  constructor(private accountService: AccountService,
    private userServices:UserentityService, private toastServices:ToastrService) {
    // this.accountService.currentUser$.pipe(take(1)).subscribe(resp =>
    //   { 
    //     console.log(resp);
    //     window.alert(resp);
    //     this.User = resp
    // });
    this.User=JSON.parse(localStorage.getItem("users")as string);
  }

  ngOnInit(): void {
    // console.log(this.User);
    // debugger;
    // var resp=localStorage.getItem("users");
    this.Intinilzeuploader();
  }
  SetMainPhoto(photo:Photo){
    debugger
      console.log(this.User);
      this.userServices.SetIsmainPhoto(photo.Id).subscribe(()=>{
      this.User.photourl=photo.Url;
      debugger
      this.accountService.setCurrentUser(this.User);
      this.userEntity.PhotoUrl=photo.Url;
      this.userEntity.photos.forEach(p=>{
        if(p.IsMain)p.IsMain=false;
        if(p.Id==photo.Id) p.IsMain=true;
      })
      },
      error=>{
        console.log(error);
      })
  }
  DeletePhoto(event:any){
      //confirm("Are you sure want to delete??");
      //this.toastServices.
    this.userServices.DeletePhoto(event).subscribe(resp=>{
      console.log(resp);
      this.userEntity.photos=this.userEntity.photos.filter(x=>x.Id!==event);
    })
  }
  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }
  fileOverAnother(e: any) {

  }
  Intinilzeuploader() {
    debugger;
    this.uploader = new FileUploader({
      url: this.baseUrl + 'user/add-Photo',
      authToken: 'Bearer ' + this.User.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onSuccessItem = (item, response, status,header) => {
      debugger;
      if (response) {
        const photo:Photo = JSON.parse(response);
        this.userEntity.photos.push(photo);
        if(photo.IsMain){
          this.User.photourl=photo.Url;
          this.userEntity.PhotoUrl=photo.Url;
          this.accountService.setCurrentUser(this.User);
        }
      }
    }
  
  }
 
}
