import { Component, Input, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AccountService } from 'src/app/_services/account.service';


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

  constructor(private authService:AccountService) {
    
    
  }
   

  ngOnInit() {
    this.initializeUploader();
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
  
}
