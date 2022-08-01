// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  keys:{
    languageStorage: 'boiler_plate_lang'
  },
  apiUrl:'https://localhost:44371/api/arklicense/',
  defaults: {
    dateFormat: 'dd.MM.yyyy',
    table: {
      filter: {
        matchMode: 'contains',
      },
      pageSize: 10,
      pageSizes: [10, 25, 50, 100],
      currentPageTemplate:
        'Showing {first} to {last} of {totalRecords} entries',
      column: {
        buttonClass: 'p-button-raised p-button-rounded',
      },
    }
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
