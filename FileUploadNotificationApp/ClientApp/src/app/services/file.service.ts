import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(private http: HttpClient) {}

  uploadFile(formData: FormData) {
    return this.http.post('https://localhost:7271/api/upload-file', formData);
  }
}
