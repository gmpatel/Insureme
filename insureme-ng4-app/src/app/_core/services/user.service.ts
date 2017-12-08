import { Injectable } from '@angular/core';
import { WebApiService } from './-web-api.service';
import { Observable } from 'rxjs';

@Injectable()
export class UserService {

	private serviceBaseUrl = '/users';

	constructor(private webApiService: WebApiService) {
	}

	public getUsers(): Observable<User[]> {
		return this.webApiService.get(this.getServiceUrl());
	}

	public addUser(user: User): Observable<any> {
		return this.webApiService.post(this.getServiceUrl(), user);
	}

	public deleteUser(user: User): Observable<boolean> {
		console.log('request item to delete nav item');
		console.log(user);
		return this.webApiService.delete(this.getServiceUrl(), user)
	}

	public getAllUsers() {
		return this.webApiService.get('/users/level/1');
	}

	private getServiceUrl(ext?: string): string {
		let retVal = this.serviceBaseUrl;
		if (ext) {
			retVal += '/' + ext;
		}
		return retVal;
	}
}

export interface User {
	first_name: string;
	last_name: string;
	email: string;
	username: string;
	password: string;
	created_on: Date;
}

