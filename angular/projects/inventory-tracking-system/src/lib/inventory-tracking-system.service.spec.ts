import { TestBed } from '@angular/core/testing';
import { InventoryTrackingSystemService } from './services/inventory-tracking-system.service';
import { RestService } from '@abp/ng.core';

describe('InventoryTrackingSystemService', () => {
  let service: InventoryTrackingSystemService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(InventoryTrackingSystemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
