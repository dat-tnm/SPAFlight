import { Component } from '@angular/core';
import { FormBuilder, Validator, Validators } from '@angular/forms';
import { PassengerService } from './../api/services/passenger.service';
import { AuthService } from '../auth/auth.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register-passenger',
  templateUrl: './register-passenger.component.html',
  styleUrls: ['./register-passenger.component.css']
})
export class RegisterPassengerComponent {
  constructor(private passengerService: PassengerService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute) { }

  returnUrl?: string = undefined;

  form = this.fb.group({
    email: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(100)])],
    password: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(35)])],
    name: ['', Validators.compose([Validators.required, Validators.minLength(3), Validators.maxLength(35)])],
    isFemale: [true, Validators.required]
  })


  ngOnInit(): void {
    this.activatedRoute.params.subscribe(p => this.returnUrl = p['returnUrl']);
  }

  checkPassenger(): void {
    const formEmail = this.form.get('email')?.value ?? "";
    const params = { email: formEmail };

    this.passengerService
      .findPassenger(params)
      .subscribe(this.login,
        e => {
          if (e.status != 404) {
            console.error(e);
          }
        });
  }

  register() {
    if (this.form.invalid)
      return;

    console.log("Form Values: ", this.form.value);

    this.passengerService.registerPassenger({ body: this.form.value })
      .subscribe(this.login, console.error);
  }

  private login = () => {
    this.authService.loginUser({ email: this.form.get('email')?.value ?? "" })
    this.router.navigate([ this.returnUrl ?? '/search-flights'])
  }
}
