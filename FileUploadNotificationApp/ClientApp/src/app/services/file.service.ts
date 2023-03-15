import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  uploadFile(formData: FormData) {
    return this.http.post(`${this.apiUrl}/api/upload-file`, formData);
  }
}
