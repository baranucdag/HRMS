import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-apply-form-personal',
  templateUrl: './apply-form-personal.component.html',
  styleUrls: ['./apply-form-personal.component.scss']
})
export class ApplyFormPersonalComponent implements OnInit {
  personalInformation: any;
  submitted: boolean = false;
  constructor() { }

  ngOnInit(): void {
  }

}
