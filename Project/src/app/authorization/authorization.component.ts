import { Component } from '@angular/core';
import { RegistrationComponent } from "../registration/registration.component";

@Component({
  selector: 'app-authorization',
  standalone: true,
  imports: [RegistrationComponent],
  templateUrl: './authorization.component.html',
  styleUrl: './authorization.component.css'
})
export class AuthorizationComponent {

}
