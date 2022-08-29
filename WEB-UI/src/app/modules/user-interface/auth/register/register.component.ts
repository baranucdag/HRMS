import { MessageService } from 'primeng/api';
import { LocalStorageService } from './../../../../core/services/local-storage/local-storage.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { AuthService } from 'src/app/core/services/api/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  private readonly onDestroy = new Subject<void>();

  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private localStorageService: LocalStorageService,
    private messageService:MessageService
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  createForm() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  register() {
    if (this.registerForm.valid) {
      const sendForm = Object.assign({}, this.registerForm.value);
      console.log(sendForm);
      this.authService
        .register(sendForm)
        .pipe(takeUntil(this.onDestroy))
        .subscribe(
          (response) => {
            this.localStorageService.set('token', response.data.token);
            this.authService.getUserDetailsFromToken();
            this.router.navigate(['/']);
          },
          (responseError) => {
            console.log(responseError);
          }
        );
    }else this.messageService.add({ severity: "error", detail: "lütfen bilgilerinizi kontrol ediniz" })
  }
}
