import { Component, OnInit } from '@angular/core';
import { RegisterUser, UserAuthService } from '../_core/services/user-auth.service';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

	private user: RegisterUser;
	private errors = {};
	private password2: string;


	constructor(private appAuthService: UserAuthService) {
	}

	ngOnInit() {
		this.user = <RegisterUser> {};
	}

	protected registerUser(): void {
		console.log('please send request to register this user : ', this.user);
		this.appAuthService.register(this.user);
	}

	private noErrors(): boolean {
		return Object.keys(this.errors).length === 0 && this.errors.constructor === Object;
	}



	protected enableSubmit(): boolean {
		if (this.user.firstName && this.user.lastName && this.user.email && this.user.password) {
			if (this.user.password == this.password2) {
				if (this.noErrors()) {
					return true;
				}
			}
		}
		return false;
	}

	private setError(field, msg) {
		let self = this;
		setTimeout(function() {
			self.errors[field] = msg;
		}, 0);
	}

	private deleteError(field) {
		delete this.errors[field];
	}

	protected validate(field, val) {
		if (field == 'firstName') {
			this.deleteError(field);
			if (val == 'chirag') {
				this.setError(field, 'Chirag is not allowed as first name ...');
			}
		}

		if (field == 'password2') {
			this.deleteError(field);
			if (this.user.password != val) {
				this.setError(field, 'Passwords are not matching ...');
			}
		}
	}
}
