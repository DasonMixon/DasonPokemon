import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkPtcgoAccountComponent } from './link-ptcgo-account.component';

describe('LinkPtcgoAccountComponent', () => {
  let component: LinkPtcgoAccountComponent;
  let fixture: ComponentFixture<LinkPtcgoAccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LinkPtcgoAccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LinkPtcgoAccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
