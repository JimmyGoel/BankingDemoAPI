import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { IUserEntity } from 'src/app/_modle/IuserEntity';
import { UserentityService } from 'src/app/_services/userentity.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
    members!:IUserEntity;
    galleryOptions: NgxGalleryOptions[];
    galleryImages: NgxGalleryImage[];
  constructor(private userEntry:UserentityService,private router:ActivatedRoute) { }

  ngOnInit(): void {
    this.getUserDetails();
    this.galleryOptions = [
      {
        width: '500px',
        height: '500px',
        imagePercent:100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview:false
      }
    ]
    
  }
  getImages():NgxGalleryImage[]{
    const imageURl=[];
    for(const image of this.members.photos){
      imageURl.push({
        small:image?.Url,
        medium: image?.Url,
        big: image?.Url
      })
    }
    return imageURl;
  }
  getUserDetails(){
    debugger;
    this.userEntry.getUserEntityByID(this.router.snapshot.paramMap.get("id")).subscribe(
      member =>{
        this.members=member;
        this.galleryImages=this.getImages();
      }
    )
  }


}
