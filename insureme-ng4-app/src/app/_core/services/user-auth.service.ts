import { Injectable } from '@angular/core';
import { WebApiService } from './-web-api.service';
import { Router } from '@angular/router';
import { AppStoreService } from './app-store.service';
import { AlertService } from '../../__modules/alert/alert.service';

@Injectable()
export class UserAuthService {

	public redirectUrl: string = '/';
	private singUpApi = '/users/sign-up';
	private singInApi = '/users/sign-in';

	constructor(private webApiService: WebApiService,
				private router: Router,
				private appStore: AppStoreService) {
	}

	public isLoggedIn(): boolean {
		if (this.appStore.getUser()) {
			return true;
		}
		return false;
	}

	public isLoggedInAsAdmin(): boolean {
		if (this.isLoggedIn()) {
			return this.appStore.getUser().role == 'Admin';
		}
		return false;
	}

	public isLoggedInAsClient(): boolean {
		if (this.isLoggedIn()) {
			return this.appStore.getUser().role == 'Client' || this.appStore.getUser().role == 'Admin';
		}
	}

	public login(user: LoginUser) {
		this.webApiService.post(this.singInApi, user).subscribe((result) => {
			if (result.token) {
				this.appStore.saveToken(result.token);
				this.appStore.saveUser(result.user);
				this.router.navigate([this.generatingSafeRoute()])
			}
		});
	}

	private generatingSafeRoute() {
		let admin = /^\/admin/;
		let client = /^\/client/;
		let userRole = this.appStore.getUser().role;

		if (userRole == 'Client') {
			if(admin.test(this.redirectUrl)) {
				return '/client';
			}
		} else if (userRole == 'User') {
			// console.log('route : ', this.redirectUrl);
			// console.log('checking for admin route : ', admin.test(this.redirectUrl));
			// console.log('checking for client route : ', client.test(this.redirectUrl));
			if (admin.test(this.redirectUrl) || client.test(this.redirectUrl)) {
				return '/user';
			}
		}

		return this.redirectUrl;
	}

	public logout(): void {
		this.appStore.clear();
		this.router.navigate(['/']);
	}

	public register(user: RegisterUser) {
		this.webApiService.post(this.singUpApi, user).subscribe((result) => {
			console.log('sign up result : ', result);
		});
	}
}

export interface AppUser {
	firstName: string;
	lastName: string;
	age: number;
	familyType: string;
	state: string;
	postcode: string;
	mobile: string;
	email: string;
	role: string;
	enabled: boolean;
	verified: boolean;
}

export interface RegisterUser {
	firstName: string;
	lastName: string;
	email: string;
	password: string;
}

export interface LoginUser {
	email: string;
	password: string;
}