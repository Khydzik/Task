import { Component,Input,  OnInit,  ViewChild, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { CreatePost } from '../../models/createPost.interface';
import { FormGroup,FormBuilder, Validators } from '@angular/forms';
import { ShowToastrService } from './../../services/show-toastr.service';
import { post } from 'selenium-webdriver/http';
 
@Component({
  selector: 'app-register',
  templateUrl: './createPost.component.html',
  styleUrls: ['../../styles/shared-form.style.css'],
  encapsulation: ViewEncapsulation.None
})

export class CreatePostComponent implements OnInit{
  @Input() create: CreatePost;
  @ViewChild('uploader') uploader: any;
  files:any;

  postForm: FormGroup;

  constructor(
    private apiService: AuthService,
    private toastr: ShowToastrService,
    private router:Router,
    private formBuilder: FormBuilder
  ) {
  }

ngOnInit(): void {
      this.postForm = this.formBuilder.group(
        {
          title: [null, Validators.required],
          shortDescription: [null,Validators.required]
        },
        { updateOn: 'submit' }
      ); 
    }  

    addPhoto(event) {
      let target = event.target || event.srcElement;
      this.files = target.files;
  }
  utoa(str:string) {
    return window.btoa(unescape(encodeURIComponent(str)));
}
    onSubmit() {
      let finalData;
      let formModel = this.postForm.value;  

      if (this.files) {
        let files: FileList = this.files;
        const formData = new FormData();
        for (let i = 0; i < files.length; i++) {
            formData.append('image', files[0]);             
        }
        formData.append('title',formModel.title);
        formData.append('shortDescription',formModel.shortDescription);
        finalData = formData;
        
      this.apiService
            .upload(finalData);                
          } 
        }
      }
    