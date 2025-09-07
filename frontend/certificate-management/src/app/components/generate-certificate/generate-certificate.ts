import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputText } from 'primeng/inputtext';
import { CertificateService, CreateCertificateRequest } from '../../services/certificate-service';
import { SessionService } from '../../services/session-service';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-generate-certificate',
  imports: [FormsModule,InputText, ButtonModule, ToastModule ],
  providers: [MessageService],
  templateUrl: './generate-certificate.html',
  styleUrl: './generate-certificate.scss'
})
export class GenerateCertificate {
  protected studentNameComplete: string = '';
  protected activities: string[] = [];

  private readonly _certificateService = inject(CertificateService);
  private readonly _sessionService = inject(SessionService);
  private readonly _messageService = inject(MessageService);

  protected isLoading: boolean = false;

  addActivity(name:string) : void{
    this.activities.push(name);
  }

  deleteActivity(index:number) : void{
    this.activities.splice(index,1);
  }

  generateCertificate() : void{
    if (this.isLoading) return;

    this.isLoading = true;

    var sessionId = this._sessionService.getSessionId();
    var request: CreateCertificateRequest = {
      StudentFullName: this.studentNameComplete,
      Activities: this.activities,
      sessionId: sessionId
    }
    this._certificateService.createCertificate(request).subscribe(
      {
        next: (response) => {
          this._messageService.add({severity:'success', summary: 'Success', detail: 'Certificate generated successfully!'});
          this._clear();
          this.isLoading = false;
        },
        error: (error) => {
          this._messageService.add({ severity: 'error', summary: 'Erro', detail: 'Falha ao gerar certificado.' });
          this.isLoading = false;
        }
      }
    );
  }

  private _clear() : void{
    this.studentNameComplete = '';
    this.activities = [];
  }


}
