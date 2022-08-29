import { AuthService } from './../../../services/api/auth.service';
import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'src/app/core/services/local-storage/local-storage.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
})
export class NavigationComponent implements OnInit {
  constructor(
    private localStorageService: LocalStorageService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {}

  logOut() {
    this.localStorageService.Remove('token');
  }
  
  isAuthenticated(){
    if(this.localStorageService.get('token')){
      return true;
    }else{
      return false;
    }
  }
}
