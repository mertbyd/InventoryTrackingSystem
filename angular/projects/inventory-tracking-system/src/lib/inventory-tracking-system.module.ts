import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { InventoryTrackingSystemComponent } from './components/inventory-tracking-system.component';
import { InventoryTrackingSystemRoutingModule } from './inventory-tracking-system-routing.module';

@NgModule({
  declarations: [InventoryTrackingSystemComponent],
  imports: [CoreModule, ThemeSharedModule, InventoryTrackingSystemRoutingModule],
  exports: [InventoryTrackingSystemComponent],
})
export class InventoryTrackingSystemModule {
  static forChild(): ModuleWithProviders<InventoryTrackingSystemModule> {
    return {
      ngModule: InventoryTrackingSystemModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<InventoryTrackingSystemModule> {
    return new LazyModuleFactory(InventoryTrackingSystemModule.forChild());
  }
}
