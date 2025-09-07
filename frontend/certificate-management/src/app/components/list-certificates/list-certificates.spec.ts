import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCertificates } from './list-certificates';

describe('ListCertificates', () => {
  let component: ListCertificates;
  let fixture: ComponentFixture<ListCertificates>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListCertificates]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ListCertificates);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
