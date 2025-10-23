import { ModuleWithProviders, NgModule } from '@angular/core';
import { INVENTORY_TRACKİNG_SYSTEM_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class InventoryTrackingSystemConfigModule {
  static forRoot(): ModuleWithProviders<InventoryTrackingSystemConfigModule> {
    return {
      ngModule: InventoryTrackingSystemConfigModule,
      providers: [INVENTORY_TRACKİNG_SYSTEM_ROUTE_PROVIDERS],
    };
  }
}
