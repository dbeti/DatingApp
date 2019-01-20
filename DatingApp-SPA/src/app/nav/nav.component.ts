import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  title: String = 'Dating App';
  model: any = {};

  constructor(private authService: AuthService,
    private alertifyService: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(() => {
      this.alertifyService.success('Logged in successfuly!');
      this.router.navigate(['/members']);
    }, error => {
      this.alertifyService.error(error);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/home']);
  }

  getUserName() {
    const userName = this.authService.getUserName();
    return userName;
  }

}
