import { Component, EventEmitter, inject, Output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CertificateModel, CertificateService } from '../../services/certificate-service';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-list-certificates',
  imports: [ButtonModule,CommonModule],
  templateUrl: './list-certificates.html',
  styleUrl: './list-certificates.scss'
})
export class ListCertificates {
  @Output() changeMenu = new EventEmitter<void>();
  private readonly _certificateService = inject(CertificateService);
  
  certificates: CertificateModel[] = [];
  protected selectedCertificate?: CertificateModel;
  isMenuSeeImage: boolean = false;

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


  changeMenuToCreateCertificate(){
    this.changeMenu.emit();
  }

}
