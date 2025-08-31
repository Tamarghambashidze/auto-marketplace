import { Component } from '@angular/core';
import { CreateUser, UserDetails } from '../shared/models/user.model';
import { FormsModule, NgForm } from '@angular/forms';
import { UserService } from '../shared/services/user.service';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-registration',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css'
})
export class RegistrationComponent {
  public user:CreateUser = new CreateUser();
  public details:UserDetails = new UserDetails();

  constructor(private userService:UserService, private toastr:ToastrService, private router:Router){
    this.user.userDetails = this.details;
  }

  onRegister(form: NgForm) {
    if (form.valid) {
      console.log("User Registered:", this.user);
      this.userService.registerUser(this.user).subscribe({
        next: res => {
          console.log(res);
          this.toastr.success("New user created!", "Registration Successful");
          this.router.navigate(['/log-in'])
        },
        error: (err: HttpErrorResponse) => {
          console.error("Registration Error:", err);
  
          if (err.status === 400 && err.error?.errors) {
            const validationErrors = Object.values(err.error.errors).flat();
            validationErrors.forEach(errorMsg => this.toastr.error(errorMsg as string, "Validation Error"));
          } else {
            this.toastr.error("Something went wrong. Please try again.", "Error");
          }
        }
      });
      form.resetForm();
    } else {
      this.toastr.error("Please fill in all required fields correctly.", "Form Error");
      console.log("Form is not valid");
    }
  }  
}
