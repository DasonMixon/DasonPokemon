import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  register : boolean = false;

  loginForm : FormGroup;
  registerForm : FormGroup;
  formBuilder : FormBuilder = new FormBuilder();

  constructor() {
    this.loginForm = this.formBuilder.group({
      email: new FormControl('', { validators: Validators.compose([
        Validators.required,
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')
      ]), updateOn: 'blur' }),
      password: new FormControl('', { validators: [Validators.required], updateOn: 'blur' })
    });

    this.registerForm = this.formBuilder.group({
      firstName: new FormControl('', { validators: [Validators.required], updateOn: 'blur' }),
      lastName: new FormControl('', { validators: [Validators.required], updateOn: 'blur' }),
      email: new FormControl('', { validators: Validators.compose([
        Validators.required,
        Validators.pattern('^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$')
      ]), updateOn: 'blur' }),
      password: new FormControl('', { validators: [Validators.required], updateOn: 'blur' })
    });
  }

  ngOnInit(): void {
  }

  public ToggleLoginOrRegister() {
    this.loginForm.reset();
    this.registerForm.reset();

    this.register = !this.register;
  }

  public onSubmitRegister() {
    if (this.registerForm.valid) {
      console.log('form submitted');
    } else {
      this.validateAllFormFields(this.registerForm);
    }
  }

  public onSubmitLogin() {
    if (this.loginForm.valid) {
      console.log('form submitted');
    } else {
      this.validateAllFormFields(this.loginForm);
    }
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  get loginEmail() { return this.loginForm.get('email'); }
  get loginEmailInvalid() { return this.loginEmail!.invalid && (this.loginEmail!.dirty || this.loginEmail!.touched) }

  get loginPassword() { return this.loginForm.get('password'); }
  get loginPasswordInvalid() { return this.loginPassword!.invalid && (this.loginPassword!.dirty || this.loginPassword!.touched) }

  get registerFirstName() { return this.registerForm.get('firstName'); }
  get registerFirstNameInvalid() { return this.registerFirstName!.invalid && (this.registerFirstName!.dirty || this.registerFirstName!.touched) }

  get registerLastName() { return this.registerForm.get('lastName'); }
  get registerLastNameInvalid() { return this.registerLastName!.invalid && (this.registerLastName!.dirty || this.registerLastName!.touched) }

  get registerEmail() { return this.registerForm.get('email'); }
  get registerEmailInvalid() { return this.registerEmail!.invalid && (this.registerEmail!.dirty || this.registerEmail!.touched) }

  get registerPassword() { return this.registerForm.get('password'); }
  get registerPasswordInvalid() { return this.registerPassword!.invalid && (this.registerPassword!.dirty || this.registerPassword!.touched) }
}
