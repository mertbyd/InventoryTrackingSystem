import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'InventoryTrackingSystem',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44398/',
    redirectUri: baseUrl,
    clientId: 'InventoryTrackingSystem_App',
    responseType: 'code',
    scope: 'offline_access InventoryTrackingSystem',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44398',
      rootNamespace: 'InventoryTrackingSystem',
    },
    InventoryTrackingSystem: {
      url: 'https://localhost:44362',
      rootNamespace: 'InventoryTrackingSystem',
    },
  },
} as Environment;
