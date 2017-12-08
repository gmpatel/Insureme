import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MtzInputComponent } from './mtz-input.component';

describe('MtzInputComponent', () => {
  let component: MtzInputComponent;
  let fixture: ComponentFixture<MtzInputComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MtzInputComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MtzInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
