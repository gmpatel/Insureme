import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolSetComponent } from './tool-set.component';

describe('ToolSetComponent', () => {
  let component: ToolSetComponent;
  let fixture: ComponentFixture<ToolSetComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ToolSetComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolSetComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
