import { ICandidate } from './../../../../core/models/views/candidate.model';
import { AuthService } from './../../../../core/services/api/auth.service';
import { CandidateService } from './../../../../core/services/api/candidate.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-apply-form',
  templateUrl: './apply-form.component.html',
  styleUrls: ['./apply-form.component.scss'],
})
export class ApplyFormComponent implements OnInit {
  candidateForm!: FormGroup;
  items: MenuItem[] = [];

  get f(): any {
    return this.candidateForm.controls;
  }

  constructor(
    private candidateService: CandidateService,
    private formBuilder: FormBuilder,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.items = [
      {
        label: 'Personal Informations',
        routerLink: 'personal',
      },
      {
        label: 'Education Information',
        routerLink: 'education',
      },
      {
        label: 'Work Experience Information',
        routerLink: 'workexperience',
      },
      {
        label: 'Confirmation',
        routerLink: 'confirmation',
      },
    ];
    this.createForm();
  }

  createForm() {
    this.candidateForm = this.formBuilder.group({
      dateOfBirth: ['', Validators.required],
      placeOfBirth: ['', Validators.required],
      gender: ['', Validators.required],
      profession: ['', Validators.required],
      isMarried: ['', Validators.required],
    });
  }

  updateCandidate() {
    if (this.authService.isAuthenticated() && this.candidateForm.valid) {
      let candidateToUpdate: any = {
        dateOfBirth: this.f.dateOfBirth.value,
        placeOfBirth: this.f.placeOfBirth.value,
        gender: this.f.placeOfBirth.value,
        profession: this.f.profession.value,
        isMarried: this.f.isMarried.value,
      };
      console.log(candidateToUpdate);
    }
  }
}
