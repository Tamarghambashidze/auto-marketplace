import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { User } from '../shared/models/user.model';

@Component({
  selector: 'app-header',
  standalone: true,
  imports:[RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {

  public user:User = new User();
  navToggler() {
    const navbar = document.querySelector("[data-navbar]");
    const navToggler = document.querySelector("[data-nav-toggler]");
    navbar?.classList.toggle("active");
    navToggler?.classList.toggle("active");
  }

  IsLoggedIn(): boolean {
    var item = localStorage.getItem('token');
    this.user = JSON.parse(item || '{}');
    return  item ! == null;
  }
}
