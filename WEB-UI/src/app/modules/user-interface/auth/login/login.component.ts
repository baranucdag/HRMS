import { Route, Router } from '@angular/router';
import { LocalStorageService } from './../../../../core/services/local-storage/local-storage.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from './../../../../core/services/api/auth.service';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit,OnDestroy {

  loginForm!: FormGroup;
  private readonly onDestroy = new Subject<void>();
  
  constructor(
    private authService: AuthService,
    private localStorageService:LocalStorageService,
    private formBuilder: FormBuilder,
    private router:Router
  ) {}

  ngOnInit(): void {
    this.createForm();
  }

 
  ngOnDestroy() {
    this.onDestroy.next();
    this.onDestroy.complete();
  }

  createForm(){
    this.loginForm = this.formBuilder.group({
      email:['',Validators.required],
      password:['',Validators.required]
    })
  }

  login(){
   const sendForm =  Object.assign({},this.loginForm.value)
   this.authService.login(sendForm).pipe(takeUntil(this.onDestroy)).subscribe((response)=>{
    this.localStorageService.set('token',response.data.token)
    this.authService.getUserDetailsFromToken();
    this.router.navigate(['/'])
   },(responseError)=>{
    console.log(responseError);
    
   })
  }
}

