import {
  Component,
  Inject,
  Input,
  LOCALE_ID,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {DatePipe, formatDate} from '@angular/common';
import {Table} from 'primeng/table';
import {
  ColumnDataType,
  ColumnFilterDataType,
  IColumn,
  IColumnButton,
  ITableOptions,
  SortModeType,
} from './models';
import {environment} from 'src/environments/environment';
import {takeUntil} from 'rxjs/operators';
import {Subject} from 'rxjs';
import {LazyLoadEvent} from 'primeng/api';
import {ICON} from '../../../constants';
import {T} from 'src/app/core/helpers/i18n';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss'],
})
export class TableComponent implements OnInit {
  @Input() options!: ITableOptions;
  @ViewChild('grid') grid!: Table;

  private onDestroy = new Subject<void>();
  filterGlobal: string = '';
  filterValues: any = {};
  dataSource: any[] = [];
  data: any[] = [];
  onExporting = false;
  totalRecords: number = 0;
  private clearingTable = false;
  width = 100;
  selectedRows: any[] = [];
  useLastFilterEvent = false;
  pageSize?: number;
  firstLoading: boolean = true;
  lastTableEvent?: LazyLoadEvent;
  firstTableEvent?: any;

  constructor(
    public datepipe: DatePipe,
    @Inject(LOCALE_ID) private locale: string
  ) {

  }

  ngOnInit() {
    if (!this.options.lazyLoad) {
      this.dataSource = this.options.data;
      this.data = this.dataSource.slice(
        0,
        this.pageOptions?.pageSize || this.defaultPageSize
      );
    } else {
      this.options.loading = true;
      this.useLastFilterEvent = true;
    }

  }

  ngOnDestroy() {
    this.onDestroy.next();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['options']) {
      this.options = changes['options'].currentValue;
      this.ngOnInit();
    }
  }

  get icon() {
    return ICON;
  }

  get isLoading() {
    return this.options.loading || false;
  }

  get columns() {
    return this.options.columns.filter((m) => !m.hidden);
  }

  get columnsHasWidth() {
    return this.columns.filter((column) => !!column.width);
  }

  get columnsHasNotWidth() {
    return this.columns.filter((column) => !column.width);
  }

  get filterableColumns() {
    return this.columns.filter(
      (column) => column.filterable !== false && column.field
    );
  }

  get globalFilterableFields(): string[] {
    return this.filterableColumns.map((column) => column.field!) || [];
  }

  get defaultDateFormat() {
    return environment.defaults.dateFormat;
  }

  get defaultPageSize() {
    return environment.defaults.table.pageSize;
  }

  get defaultPageSizes() {
    return environment.defaults.table.pageSizes;
  }

  get defaultCurrentPageTemplate() {
    return environment.defaults.table.currentPageTemplate;
  }

  get defaultColumnButtonclass() {
    return environment.defaults.table.column.buttonClass;
  }

  get lazyLoad(): boolean {
    return !!this.options.lazyLoad;
  }

  get sortMode(): SortModeType {
    return this.options.sortMode || 'single';
  }

  get scrollable(): boolean {
    return !!this.options.scrollable;
  }

  get resizable(): boolean {
    return !!this.options.resizable;
  }

  get reorderable(): boolean {
    return !!this.options.reorderable;
  }

  get selectable(): boolean {
    return !!this.options.selectable;
  }

  get searchOptions() {
    return this.options.globalFilterOptions?.searchOptions;
  }

  get pageOptions() {
    return this.options.pageOptions;
  }

  get clearButtonOptions() {
    return this.options.globalFilterOptions?.clearButtonOptions;
  }

  get searchProperties() {
    return {
      enabled: !!this.searchOptions,
      placeholder: this.searchOptions?.placeholder ?? 'Search',
      iconPos: this.searchOptions?.iconPosition ?? 'left',
    };
  }

  get clearButtonProperties() {
    return {
      enabled: !!this.clearButtonOptions,
      label: this.clearButtonOptions?.label ?? 'Clear',
      class: this.clearButtonOptions?.class ?? '',
    };
  }

  get toolbarOptions() {
    return this.options.toolbarOptions;
  }

  get exportColumns() {
    return this.columns
      .filter((m) => m.field)
      .map((m) => `${m.field}:${T(m.title)}`)
      .join(',');
  }

  get showIndexColumn() {
    return this.options.showIndexColumn || true;
  }

  defaultFilterMatch(type: ColumnDataType) {
    switch (type) {
      case 'numeric':
      case 'enum':
      case 'date':
        return 'equals';
    }

    return environment.defaults.table.filter.matchMode;
  }

  getColumnFilterType(column: IColumn): ColumnFilterDataType {
    if (column.filter?.type) return column.filter.type;

    switch (column.type) {
      case 'numeric':
        return 'numeric';
      case 'enum':
        return 'dropdown';
      case 'date':
        return 'date';
    }

    return 'text';
  }

  clear(table: Table) {
    //this.firstLoading = true;
    this.clearingTable = true;
    this.filterGlobal = '';
    this.filterValues = {};
    table.sortOrder = 0;
    table.sortField = '';
    table.clear();
    table.reset();
    this.selectedRows = [];
    this.onSelectionChange([]);
    if (this.options.lazyLoad) {
      this.options.columns.forEach((column) => {
        Object.keys(column).forEach((prop) => {
          if (prop == 'filter') {
            if (column.filter?.type == 'dropdown') {
              this.filterValues[column.field ?? ''] =
                column.filter?.defaultValue;
              this.firstTableEvent.filters[column.field ?? ''] = {
                value: column.filter?.defaultValue
                  ? column.filter?.defaultValue.toString()
                  : null,
                matchMode: 'equals',
              };
            }
          }else if (column.filter?.type == 'text') {
            this.filterValues[column.field ?? ''] =
              column.filter?.defaultValue?.value;
            this.firstTableEvent.filters[column.field ?? ''] = {
              value: column.filter?.defaultValue?.value
                ? column.filter?.defaultValue.value.toString()
                : null,
              matchMode: 'contains',
            };
          }
        });
      });
      this.getData(this.firstTableEvent);
    } else {
      this.clearingTable = false;
      this.filter(null, table);
    }
  }

  onChangeFilterGlobal(event: any, table: Table) {
    let filterValue: any = this.filterGlobal;
    this.filter(filterValue, table);
  }

  onChangeFilter(value: any, filterCallback: any, type?: any) {
    value = value || value === 0 ? (value = value.toString()) : null;
    if (type == 'date') {
      value = formatDate(value, 'dd-MM-yyyy', this.locale);
    }

    filterCallback(value);
  }

  clearFilter(filterCallback: any, field: string) {
    this.filterValues[field] = null;
    this.filterGlobal = '';

    filterCallback(null);
  }

  filter(value: any, table: Table) {
    table.filterGlobal(value, 'contains');
  }

  loadData(event: any) {
    if (this.firstLoading) {
      this.firstTableEvent = event;
      this.options.columns.forEach((column) => {
        Object.keys(column).forEach((prop) => {
          if (prop == 'filter') {
            if (column.filter?.type == 'dropdown') {
              this.filterValues[column.field ?? ''] =
                column.filter?.defaultValue;
              event.filters[column.field ?? ''] = {
                value: column.filter?.defaultValue
                  ? column.filter?.defaultValue.toString()
                  : null,
                matchMode: 'equals',
              };
            }else if (column.filter?.type == 'numeric') {
              console.log("a");
              
              this.filterValues[column.field ?? ''] =
                column.filter?.defaultValue?.value;
              event.filters[column.field ?? ''] = {
                value: column.filter?.defaultValue?.value
                  ? column.filter?.defaultValue.value
                  : null,
                matchMode: 'contains',
              };
            }
          }
        });
      });
      this.getData(event);
    }

    if (!this.clearingTable && this.firstLoading != true) {
      this.getData(event);
    }
    this.firstLoading = false;
  }

  refreshData() {
    this.selectedRows = [];
    this.getData();
  }


  getData(event?: LazyLoadEvent) {
    if (event) {
      this.lastTableEvent = event;
    }

    if (this.lastTableEvent) this.pageSize = this.lastTableEvent.rows;
    this.options.loading = true;
    const dataFilter: any = {
      curPage:
        this.lastTableEvent?.first != undefined && this.lastTableEvent.rows != undefined
          ? this.lastTableEvent.first / this.lastTableEvent.rows
          : 0,
      pageSize: this.pageSize,
      filters: this.lastTableEvent?.filters,
      multiSortMeta: this.lastTableEvent?.multiSortMeta ? this.lastTableEvent.multiSortMeta : [],
    };
    dataFilter.curPage++; //back-end ile uyumlu olması için +1 eklendi

    if (!this.lastTableEvent) dataFilter.filters = null;

    this.options.dataService.getPeginationData(dataFilter,this.options.url)
      .pipe(takeUntil(this.onDestroy))
      .subscribe((result:any)=>{
        // this.clearingTable = false;
        // this.data = result.body.data.rows;
        // this.options.loading = false;


        this.totalRecords = (result.body.data.totalRowCount as number) ?? 0;
        this.options.loading = false;
        this.dataSource = result.body.data.rows;
        this.data = this.dataSource;
        this.clearingTable = false;
    })
  }

  onSelectionChange(event: any) {
    if (this.options.onSelectionChange) this.options.onSelectionChange(event);
  }


  getEnumData(row: any, column: IColumn) {
    if (this.lazyLoad || !column.filter?.data) return row[column.field!];

    const value = column.filter.data.find(
      (m) => m.value === row[column.field!]
    )?.label;
    return value;
  }

  getTemplateValue(column: IColumn, row: any, rowIndex: number) {
    switch (typeof column.template) {
      case 'function':
        return column.template(row, rowIndex);
      default:
        return column.template;
    }
  }

  numberLengthControl(value: number, field: string) {
    if (value && value.toString().length > 10) {
      this.filterValues[field] = parseInt(value.toString().slice(0, 10));
    }
  }

  hidden(row: any, rowIndex: number, button: IColumnButton): boolean {
    if (button.hidden) return button.hidden(row, rowIndex);
    if (row.isDeleted === 0 && button.options.tooltip === 'Undelete') return true;
    if (row.isDeleted === 1 && button.options.tooltip === 'Delete') return true;

    return false;
  }

  getIsDeleteStyle(row:any){
    let color = "#da3e56";
    if(row.isDeleted==true) color="#da3e56";
    else color="white"
    return color
  }
}
