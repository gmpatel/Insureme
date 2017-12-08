import { Component, OnInit } from '@angular/core';
import { LoginUser, UserAuthService } from '../_core/services/user-auth.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

	private user = <LoginUser>{};
	private errors = {};

	constructor(private appAuthService: UserAuthService) {
	}

	ngOnInit() {
	}

	private noErrors(): boolean {
		return Object.keys(this.errors).length === 0 && this.errors.constructor === Object;
	}

	protected enableSubmit(): boolean {
		if (this.user.email && this.user.password) {
			if (this.noErrors()) {
				return true;
			}
		}
		return false;
	}

	protected loginUser() {
		console.log(this.user);
		this.appAuthService.login(this.user);
	}

}
