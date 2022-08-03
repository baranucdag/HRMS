import { CandidateService } from './../../../../core/services/api/candidate.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TableComponent } from 'src/app/core/components/tables';
import { ITableOptions } from 'src/app/core/components/tables/table/models';
import { DialogService } from 'src/app/core/services/dialog';

@Component({
  selector: 'app-candidate-list',
  templateUrl: './candidate-list.component.html',
  styleUrls: ['./candidate-list.component.scss'],
})
export class CandidateListComponent implements OnInit {
  @ViewChild('table') table?: TableComponent;

  // component içerisinde kullanılan değişkenler
  tableOptions!: ITableOptions;

  constructor(
    private candidateService: CandidateService,
    private dialogService: DialogService
  ) {}

  ngOnInit(): void {
    this.createTable();
  }

  createTable() {
    this.tableOptions = {
      data: [],
      columns: [
        {
          field: 'candidateFullName',
          title: 'Candidate Full Name',
          type: 'text',
        },
        {
          field: 'eMail',
          title: 'Email',
          type: 'text',
        },
        { field: 'profession', title: 'Profession', type: 'text' },
        { field: 'phoneNumber', title: 'Phone Number', type: 'text' },
        { field: 'adress', title: 'Adress', type: 'text' },
        { field: 'gender', title: 'Gender', type: 'text' ,},
      ],
      filterable: true,
      sortable: true,
      sortMode: 'multiple',
      scrollable: false,
      selectable: true,
      pageOptions: {
        pageSize: 25,
        pageSizes: [25, 50, 100, 250, 500],
      },
      globalFilterOptions: {
        clearButtonOptions: {
          label: 'Clear',
        },
        searchOptions: {
          placeholder: 'Search',
        },
      },
      lazyLoad: true,
      dataService: this.candidateService,
    };
  }

}
