import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery-9';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {

  @ViewChild('memberTabs', { static: true }) memberTabs: TabsetComponent|any
 user! : User ; 
  galleryOptions!: NgxGalleryOptions[];
  galleryImages!: NgxGalleryImage[];
  constructor(private userService : UserService,private alertify:AlertifyService,private rout:ActivatedRoute) { }

  ngOnInit() {
    //this.loadUser();
    this.rout.data.subscribe(data=>{
      this.user=data['user']
    });
    this.rout.queryParams.subscribe(
      params=>{
        debugger
        const selectedTab= params['tab'];
        this.memberTabs.tabs[selectedTab].active=true;
      }
    )
    this.galleryOptions = [
      {
          width: '500px',
          height: '500px',
          imagePercent:100,
          thumbnailsColumns: 4,
          imageAnimation: NgxGalleryAnimation.Slide,
          preview:false
      },
    ]
    this.galleryImages =this.getImage();
  }
  selectTab(tabsId:number){
   
    this.memberTabs.tabs[3].active = true;
  }
  
  getImage()
  {
    const imageUrls=[];
    for(var photo of this.user!.photos!){
     imageUrls.push({
       small:photo.url,
       medium:photo.url,
       big:photo.url
     })
    }
return imageUrls;
  }
 // getImag9e()
  //{const imageUrls=[];
   // for(let i=0;i<this.user!.photos!.length;i++)
    //{
    //  imageUrls.push({
     //   small:this.user!.photos![i].url
     // })
   // }
 // }

  //loadUser()
  //{
   // this.userService.getUserById(this.rout.snapshot.params['id']).subscribe(
//(user:User)=>{this.user=user},
     // error=>{this.alertify.error(error)}
      
   // )

  //}

}
