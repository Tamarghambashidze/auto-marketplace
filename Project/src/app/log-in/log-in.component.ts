import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { UserLogIn } from '../shared/models/user-log-in.model';
import { UserService } from '../shared/services/user.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse, HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css'
})
export class LogInComponent {
  public loginData:UserLogIn = new UserLogIn();

  constructor(private userService:UserService, private toastrService:ToastrService, private router:Router) {}

  onLogin(loginForm: NgForm) {
    if (loginForm.valid) {
      console.log('Login data:', this.loginData);
      this.userService.logIn(this.loginData).subscribe({
        next: (res) => {
          this.toastrService.success("User logged in successfully", "Success");
          localStorage.setItem('token', JSON.stringify(res.body?.user));
          this.router.navigate(['/']);
          console.log(res);
        },
        error: (err : HttpErrorResponse) => {
          this.toastrService.error(err.error.message, "Error");
          console.error(err);
        }
      });
      loginForm.resetForm();
    } else {
      this.toastrService.error("Please fill out all required fields correctly.", "Form Error");
      console.log('Form is not valid');
    }
  }  
  
}