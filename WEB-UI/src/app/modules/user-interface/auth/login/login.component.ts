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
    private formBuilder: FormBuilder
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
    console.log(response);
   },(responseError)=>{
    console.log(responseError.data);
    
   })
  }
}

