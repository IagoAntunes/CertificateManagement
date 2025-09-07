import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent, HeaderMenuItem } from './components/header-component/header-component';
import { ListCertificates } from "./components/list-certificates/list-certificates";
import { GenerateCertificate } from "./components/generate-certificate/generate-certificate";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, ListCertificates, GenerateCertificate],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('certificate-management');
  protected readonly HeaderMenuItem = HeaderMenuItem;

  selectedMenu: HeaderMenuItem = HeaderMenuItem.listCertificates;

  onMenuChanged(newMenu: HeaderMenuItem) {
    this.selectedMenu = newMenu;
  }

  changeMenuToCreateCertificate(){
    this.selectedMenu = HeaderMenuItem.createCertificate;
  }

}
