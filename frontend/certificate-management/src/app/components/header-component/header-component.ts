import { Component, EventEmitter, Input, Output } from '@angular/core';

export enum HeaderMenuItem{
  listCertificates,
  createCertificate,
}

@Component({
  selector: 'app-header-component',
  imports: [],
  templateUrl: './header-component.html',
  styleUrl: './header-component.scss'
})
export class HeaderComponent {
  @Input() selectedMenu: HeaderMenuItem = HeaderMenuItem.listCertificates;
  protected readonly HeaderMenuItem = HeaderMenuItem;

  @Output() menuChanged = new EventEmitter<HeaderMenuItem>();
  
  changeMenu(item:HeaderMenuItem){
    this.menuChanged.emit(item);
  }


}
