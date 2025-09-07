import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { CertificateService, CreateCertificateRequest } from '../../services/certificate-service';

@Component({
  selector: 'app-generate-certificate',
  imports: [FormsModule,InputText, ButtonModule ],
  templateUrl: './generate-certificate.html',
  styleUrl: './generate-certificate.scss'
})
export class GenerateCertificate {
  protected studentNameComplete: string = '';
  protected activities: string[] = [];

  private readonly certificateService = inject(CertificateService);


  addActivity(name:string){
    this.activities.push(name);
  }

  deleteActivity(index:number){
    this.activities.splice(index,1);
  }

  generateCertificate(){
    var request: CreateCertificateRequest = {
      StudentFullName: this.studentNameComplete,
      Activities: this.activities
    }
    this.certificateService.createCertificate(request).subscribe(
      {
        next: (response) => {
          this._clear();
        },
        error: (error) => {console.error('Error generating certificate:', error);}
      }
    );
  }

  private _clear(){
    this.studentNameComplete = '';
    this.activities = [];
  }


}
