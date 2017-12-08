import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Alert } from './alert.model';
import { AlertEvent } from './alert.enums';
import { Observable, Subscription } from 'rxjs';

@Injectable()
export class AlertService {

	private alert = new Subject<Alert>();
	public newAlertEvent$ = this.alert.asObservable();
	private alertEventSub = new Subject<AlertEvent>();
	private alertEvent$ = this.alertEventSub.asObservable();

	constructor() {
	}

	public error(msg: string, title?: string): void {
		if(msg) {
			if (title == null) {
				title = 'Error';
			}
			this.alertThis('error', msg, title);
		}
	}

	// here is an example of calling method and returning observable for feedback
	public errorAsync(msg: string, title?: string): Observable<AlertEvent> {
		return Observable.create(observer => {
			if(msg) {
				if (title == null) {
					title = 'Error';
				}
				this.alertThis('error', msg, title);
			}
			let alertEventSubscription = this.alertEvent$.subscribe((event: AlertEvent) => {
				if (event == AlertEvent.Ok_Clicked || event == AlertEvent.Cancel_Clicked) {
					observer.next(event);
					observer.complete();
					alertEventSubscription.unsubscribe();
				}
			})
		});
	}

	private alertThis(alertType: string, msg: string, title: string): void {
		let alert = <Alert>{
			type: alertType,
			title: title,
			content: msg
		};
		this.alertIt(alert);
	}

	private alertIt(alert: Alert) {
		this.alert.next(alert);
	}

	public raiseAlertEvent(event: AlertEvent) {
		this.alertEventSub.next(event);
	}
}
