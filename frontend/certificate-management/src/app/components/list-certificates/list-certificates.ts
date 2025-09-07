import { Component, EventEmitter, inject, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CertificateModel, CertificateService } from '../../services/certificate-service';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { Toast } from "primeng/toast";



@Component({
  selector: 'app-list-certificates',
  imports: [ButtonModule, CommonModule, Toast],
  providers: [MessageService],
  templateUrl: './list-certificates.html',
  styleUrl: './list-certificates.scss'
})
export class ListCertificates {
  @Output() changeMenu = new EventEmitter<void>();
  private readonly _certificateService = inject(CertificateService);
  private readonly _messageService = inject(MessageService);

  certificates: CertificateModel[] = [];
  protected selectedCertificate?: CertificateModel;
  isMenuSeeImage: boolean = false;
  http = inject(HttpClient);


  ngOnInit(){
    this._loadCertificates();
  }

  private _loadCertificates(){
    this._certificateService.getAllCertificates().subscribe(
      {
        next: (certificates: CertificateModel[]) => {
          this.certificates = certificates;
        },
        error: (err) => {
          console.error(err);
        },
      }
    )
  }

  seeImage(certificate: CertificateModel){
    this.isMenuSeeImage = true;
    this.selectedCertificate = certificate;
  }

  onBackSeeImage(){
    this.isMenuSeeImage = false;
  }

downloadImage():void {
  const image = this.selectedCertificate?.image;
  if (!image || !image.path) {
    console.error('Imagem ou URL da imagem não encontrada.');
    return;
  }
  const imageUrl = image.path; 

  this.http.get(imageUrl, { responseType: 'blob' }).subscribe({
    next: (blob) => {
      const objectUrl = URL.createObjectURL(blob);

      const link = document.createElement('a');
      link.href = objectUrl;
      link.download = image.fileName || 'certificado.png';

      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);

      URL.revokeObjectURL(objectUrl);
      this._messageService.add({severity:'success', summary: 'Success', detail: 'Imagem baixada com sucesso!'});
    },
    error: (err) => {
      console.error('Falha ao baixar a imagem:', err);
    }
  });
}


  changeMenuToCreateCertificate(){
    this.changeMenu.emit();
  }

}
