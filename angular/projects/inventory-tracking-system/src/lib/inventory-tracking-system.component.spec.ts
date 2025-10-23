import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { InventoryTrackingSystemComponent } from './components/inventory-tracking-system.component';
import { InventoryTrackingSystemService } from '@inventory-tracking-system';
import { of } from 'rxjs';

describe('InventoryTrackingSystemComponent', () => {
  let component: InventoryTrackingSystemComponent;
  let fixture: ComponentFixture<InventoryTrackingSystemComponent>;
  const mockInventoryTrackingSystemService = jasmine.createSpyObj('InventoryTrackingSystemService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [InventoryTrackingSystemComponent],
      providers: [
        {
          provide: InventoryTrackingSystemService,
          useValue: mockInventoryTrackingSystemService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InventoryTrackingSystemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
