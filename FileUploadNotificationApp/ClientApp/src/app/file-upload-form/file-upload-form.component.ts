import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileService } from '../services/file.service';

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

  constructor(
    private http: HttpClient,
    private fb: FormBuilder,
    private fileService: FileService
  ) {
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

    const formData = new FormData();
    formData.append('email', this.form.controls['email'].value);
    formData.append('file', this.file);

    this.fileService.uploadFile(formData).subscribe(
      () => {
        alert('File added successfully');
        this.form.controls['file'].reset();
        this.form.controls['email'].reset();
      },
      (error) => {
        alert(error);
        this.form.controls['file'].reset();
        this.form.controls['email'].reset();
      }
    );
  }
}
