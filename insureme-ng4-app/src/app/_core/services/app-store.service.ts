import { Injectable } from '@angular/core';
import { CoolLocalStorage } from 'angular2-cool-storage';

@Injectable()
export class AppStoreService {

	private tokenLocalStorageKey = 'x-access-token';
	private userLocalStorageKey = 'x-user';

	public token: any;
	private user: any;

	constructor(private localStorage: CoolLocalStorage) {
	}

	public saveToken(token: any) {
		this.localStorage.setItem(this.tokenLocalStorageKey, token);
		this.restoreToken();
	}

	private restoreToken() {
		this.token = this.localStorage.getItem(this.tokenLocalStorageKey);
	}

	public getToken(): any {
		if (!this.token) {
			this.restoreToken();
		}
		return this.token;
	}

	public saveUser(user: any) {
		this.user = user;
		this.localStorage.setItem(this.userLocalStorageKey, JSON.stringify(this.user));
	}

	private restoreUser() {
		if (this.getToken()) {
			this.user = JSON.parse(this.localStorage.getItem(this.userLocalStorageKey));
		}
	}

	public getUser(): any {
		if (!this.user) {
			this.restoreUser();
		}
		return this.user;
	}

	public clear() {
		this.localStorage.clear();
		this.user = null;
		this.token = null;
	}

	public set() {
		this.restoreToken();
		this.restoreUser();
	}
}
