import { Component, OnInit } from '@angular/core';
import { InventoryTrackingSystemService } from '../services/inventory-tracking-system.service';

@Component({
  selector: 'lib-inventory-tracking-system',
  template: ` <p>inventory-tracking-system works!</p> `,
  styles: [],
})
export class InventoryTrackingSystemComponent implements OnInit {
  constructor(private service: InventoryTrackingSystemService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
