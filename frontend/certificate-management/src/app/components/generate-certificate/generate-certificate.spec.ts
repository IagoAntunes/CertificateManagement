import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateCertificate } from './generate-certificate';

describe('GenerateCertificate', () => {
  let component: GenerateCertificate;
  let fixture: ComponentFixture<GenerateCertificate>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenerateCertificate]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GenerateCertificate);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
