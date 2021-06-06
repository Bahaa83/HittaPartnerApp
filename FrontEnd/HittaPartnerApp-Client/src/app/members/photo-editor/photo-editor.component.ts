import { Component, Input, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/_services/account.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { User } from 'src/app/_models/user';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
@Input() photos:Photo[] | undefined;

    uploader!:FileUploader;
    hasBaseDropZoneOver:false | undefined;
    URL= environment.apiUrl;
    currentMain!:Photo;
    user!:User;

  constructor(private route:ActivatedRoute, private authService:AccountService,private userService:UserService,private alertify:AlertifyService) {
    
    
  }
   

  ngOnInit() {
    this.initializeUploader();
    this.route.data.subscribe(data=>{
      this.user= data['user'];
    })
  }


  public fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }
 
  initializeUploader()
  {
    this.uploader= new FileUploader({
      url:this.URL+'Photos/AddPhotoForUser?userId='+this.authService.decodedToken.nameid,
      authToken: 'Bearer '+ localStorage.getItem('token'),
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize:10*1024*1024
    });
    this.uploader.onAfterAddingFile=(file)=>{file.withCredentials=false;};
    this.uploader.onSuccessItem=(item,Response,status,header)=>{
      if(Response){
        const res:Photo=JSON.parse(Response);
        const photo={
          id:res.id,
          url:res.url,
          dateAdded:res.dateAdded,
          isMain:res.isMain,
          description:res.description
        };
        this.photos?.push(photo)
      }
    }
  }
       setMainPhoto(photo:Photo)
       {
          this.userService.setMainPhoto(this.authService.decodedToken.nameid,photo.id).subscribe(
            next=>{this.currentMain=this.photos!.filter(p=>p.isMain===true)[0];
            this.currentMain.isMain=false;
            photo.isMain=true;
           this.authService.changeMemberPhoto(photo.url);
           this.authService.currentUser.photoUrl=photo.url;
           localStorage.setItem('user',JSON.stringify(this.authService.currentUser));
            },
            error=>{this.alertify.error(error)}
            
          )
       }
       deletePhoto(id:string){
          this.alertify.confirm("Vill du ta bort den här fotot?",()=>{
            this.userService.deletePhoto(this.authService.decodedToken.nameid,id).subscribe(
              next=>{this.photos?.splice(this.photos.findIndex(p=>p.id===id),1);
              this.alertify.success("Fotoet har tagits bort")
            },
              error=>{this.alertify.error(error)}
            )
          })
       }
  
}
