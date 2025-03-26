import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Router, RouterLink} from '@angular/router';
import {AuthService} from '../../data/auth.service';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  router = inject(Router);
  authService = inject(AuthService);

  form: FormGroup = new FormGroup({
    email: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    password: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    firstName: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    lastName: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
  });


  onSubmit() {
    this.authService.register(this.form.value).subscribe(() => {
      this.router.navigate(['/login']);
    });
  }
}
