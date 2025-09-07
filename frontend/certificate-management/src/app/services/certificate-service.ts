import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SessionService } from './session-service';

export interface CreateCertificateRequest{
  StudentFullName: string;
  Activities: string[];
  sessionId: string;
}

export interface CertificateModel{
  id: string;
  studentFullName: string;
  activities: string[];
  image?: ImageModel;
  createdAt: string;
}

export interface ImageModel{
  fileName:string,
  path:string,
  contentType:string,
}


@Injectable({
  providedIn: 'root'
})
export class CertificateService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'https://localhost:7015/api'; 
  private readonly _sessionService = inject(SessionService);

  createCertificate(request: CreateCertificateRequest) : Observable<any>{
    return this.http.post(`${this.apiUrl}/Certificate`, request);
  }

  getAllCertificates(): Observable<any>{
    var sessionId = this._sessionService.getSessionId();
    return this.http.get(`${this.apiUrl}/Certificate/${sessionId}`);
  }

}
