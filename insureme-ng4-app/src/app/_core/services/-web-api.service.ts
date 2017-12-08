import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers, Response } from '@angular/http';
import { CoolLocalStorage } from 'angular2-cool-storage';
import { Observable, Subject } from 'rxjs';
import { AlertService } from '../../__modules/alert/alert.service';
import { Router } from '@angular/router';
import { AppStoreService } from './app-store.service';

@Injectable()
export class WebApiService {

	private tokenKey = 'x-access-token';
	private baseUrl = 'http://api.tradeone.com.au/api';
	private apiVersion = 'v1';
	private tokenRenewApi = '/renew-token';
	private lastRequest: Request;

	// private progressEvent = new Subject<number>();
	// public progressEvent$ = this.progressEvent.asObservable();

	constructor(private http: Http,
				// private localStorage: CoolLocalStorage,
				private appStore: AppStoreService,
				private alertService: AlertService,
				private router: Router) {
	}

	// public get(api): Observable<any> {
	// 	let timeStr = 'ResponseTime for GET Request ' + api;
	// 	console.time(timeStr);
	// 	return Observable.create(observer => {
	// 		this._get(api).subscribe(
	// 			(res: any) => {
	// 				console.timeEnd(timeStr);
	// 				// to handle response with error object in it
	// 				if (res.error) {
	// 					console.log('creating alert for error', res.error);
	// 					this.alertService.error(res.error.message, 'Error');
	// 					this.redirectToLogin();
	// 					observer.error(res.error);
	// 				} else {
	// 					observer.next(res.result);
	// 				}
	// 				observer.complete();
	// 			},
	// 			(error) => {
	// 				console.log('error on GET request');
	// 				console.error(error);
	// 				console.timeEnd(error.error.message);
	// 				this.alertService.error(error.error.message, 'Error');
	// 				this.redirectToLogin();
	// 				observer.error(error);
	// 				observer.complete();
	// 			}
	// 		);
	// 	});
	// }

	public get(api): Observable<any> {
		let timeStr = 'ResponseTime for GET Request ' + api;
		console.time(timeStr);

		this.lastRequest = <Request>{
			type: 'get',
			api: api
		};

		return Observable.create(observer => {
			this._get(api).subscribe(
				(res: any) => {
					console.timeEnd(timeStr);
					if (res.error) {
						console.log('creating alert for error', res.error);
						this.alertService.error(res.error.message, 'Error');
						this.redirectToLogin();
						observer.error(res.error);
					} else {
						observer.next(res.result);
					}
					observer.complete();
				}
			);
		});
	}

	public post(api, body): Observable<any> {
		let timeStr = 'ResponseTime for POST Request ' + api;
		console.time(timeStr);
		return Observable.create(observer => {
			this._post(api, body).subscribe(
				(res: any) => {
					console.timeEnd(timeStr);
					if (res.error) {
						this.alertService.error(res.error, 'Error');
						this.redirectToLogin();
						observer.error(res.error);
					} else {
						observer.next(res.result);
					}
					observer.complete();
				}
			);
		});
	}

	public update(api, item): Observable<any> {
		let timeStr = 'ResponseTime for Delete Request ' + api;
		console.time(timeStr);
		return Observable.create(observer => {
			this._update(api, item['_id'], item).subscribe(
				(res: any) => {
					console.timeEnd(timeStr);
					if (res.error) {
						this.alertService.error(res.error, 'Error');
						this.redirectToLogin();
						observer.error(res.error);
					} else {
						observer.next(res.result);
					}
					observer.complete();
				}
			);
		});
	}

	public delete(api, item): Observable<any> {
		let timeStr = 'ResponseTime for Delete Request ' + api;
		console.time(timeStr);
		return Observable.create(observer => {
			this._delete(api, item['_id']).subscribe(
				(res: any) => {
					console.timeEnd(timeStr);
					if (res.error) {
						this.alertService.error(res.error, 'Error');
						this.redirectToLogin();
						observer.error(res.error);
					} else {
						observer.next(res.result);
					}
					observer.complete();
				}
			);
		});
	}

	private redirectToLogin(): void {
		this.router.navigate(['/login']);
	}

	// public uploadFile(api: string, file: File, fileName: string): Observable<any> {
	// 	let formData = new FormData();
	// 	formData.append('files', file, fileName);
	// 	return this.http.post(this.getApiUrl(api), formData);
	// }
	//
	// public uploadFileWithProgressEvent(api: string, file: File, fileName: string): Observable<any> {
	// 	return Observable.create(observer => {
	// 		let formData: FormData = new FormData(),
	// 			xhr: XMLHttpRequest = new XMLHttpRequest();
	//
	// 		formData.append("files", file, fileName);
	//
	// 		xhr.onreadystatechange = () => {
	// 			if (xhr.readyState === 4) {
	// 				if (xhr.status === 200) {
	// 					observer.next(JSON.parse(xhr.response));
	// 					observer.complete();
	// 				} else {
	// 					observer.error(xhr.response);
	// 				}
	// 			}
	// 		};
	//
	// 		xhr.upload.onprogress = (event) => {
	// 			let progress = Math.round(event.loaded / event.total * 100);
	// 			this.progressEvent.next(progress);
	// 		};
	//
	// 		xhr.open('POST', this.getApiUrl(api), true);
	// 		xhr.send(formData);
	// 	});
	// }

	private errorResponseHandler(err): Observable<any> {
		let error = err.json().error;
		return Observable.create(observer => {
			if (error.responseCode == 401 && error.code == 10103) {
				this._get(this.tokenRenewApi).subscribe((res) => {
					if (!res.error) {
						if (res.result) {
							this.appStore.saveToken(res.result);
							this._get(this.lastRequest.api).subscribe((res) => {
								observer.next(res);
							});
						}
					}
				});
			} else {
				observer.error(err.json());
			}
		});
	}

	// // old implementation
	// private _get(api, body) {
	// 	return this.http.get(this.getApiUrl(api), this.options(true))
	// 					.map((res: Response) => res.json())
	// 					.catch((error: any) => {
	// 						return Observable.create(observer => {
	// 							observer.error(JSON.parse(JSON.stringify(error.json())));
	// 						});
	// 					});
	// }

	private _get(api): Observable<any> {
		return this.http.get(this.getApiUrl(api), this.options())
						.map((res: Response) => res.json())
						.catch((error: any) => this.errorResponseHandler(error));
	}

	private _post(api, body) {
		return this.http.post(this.getApiUrl(api), body, this.options(true))
			.map((res: Response) => res.json())
			.catch((error: any) => this.errorResponseHandler(error));
	}

	private _update(api, _id, body) {
		let url = this.getApiUrl(api) + '/' + _id;
		return this.http.put(url, body, this.options(true))
						.map((res: Response) => res.json())
						.catch((error: any) => this.errorResponseHandler(error));
	}

	private _delete(api, _id) {
		let url = this.getApiUrl(api) + '/' + _id;
		return this.http.delete(url, this.options())
						.map((res: Response) => res.json())
						.catch((error: any) => this.errorResponseHandler(error));
	}


	// private functions

	private getApiUrl(api): string {
		return this.baseUrl + '/' + this.apiVersion + api;
	}

	private options(isPost: boolean = false): any {
		let headersObj = {};

		if (isPost) {
			headersObj = {
				'Content-Type': 'application/json',
			};
		}

		headersObj['x-access-key'] = 'ac30c82d-25a0-48f4-af24-31dec9688956';

		// let token = this.localStorage.getItem(this.tokenKey);
		let token = this.appStore.getToken();

		if (token) {
			headersObj[this.tokenKey] = token;
		}

		let headers = new Headers(headersObj);
		return new RequestOptions({
			headers: headers
		});
	}
}

export interface Request {
	type: string;
	api: string;
	body?: any;
}

