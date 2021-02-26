import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PackSimulatorComponent } from './pack-simulator.component';

describe('PackSimulatorComponent', () => {
  let component: PackSimulatorComponent;
  let fixture: ComponentFixture<PackSimulatorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PackSimulatorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PackSimulatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
