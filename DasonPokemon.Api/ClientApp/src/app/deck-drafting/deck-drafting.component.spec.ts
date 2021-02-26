import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckDraftingComponent } from './deck-drafting.component';

describe('DeckDraftingComponent', () => {
  let component: DeckDraftingComponent;
  let fixture: ComponentFixture<DeckDraftingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeckDraftingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DeckDraftingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
