import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-file-upload-form',
  templateUrl: './file-upload-form.component.html',
  styleUrls: ['./file-upload-form.component.css'],
})
export class FileUploadFormComponent implements OnInit {
  file: File | null = null;
  fileError: boolean = false;
  emailError: boolean = false;
  form: FormGroup;

  constructor(private http: HttpClient, private fb: FormBuilder) {
    this.form = fb.group({
      file: [null, Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  ngOnInit(): void {
    this.emailError = true;
    this.fileError = true;
  }

  onFileChange(event: any) {
    const fileList: FileList = event.target.files;
    if (fileList.length > 0) {
      this.file = fileList[0];

      if (this.file && this.file.name.split('.').pop() !== 'docx') {
        this.fileError = true;
      } else {
        this.fileError = false;
      }
    }
  }

  onSubmit() {
    if (this.form.invalid || this.fileError || !this.file) {
      return;
    }

    console.log(this.form.controls['email'].value);
  }
}
