import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Notification } from './notification/notification.model';

@Injectable()
export class NotificationsService {

	private notification = new Subject<Notification>();
	public notificationEvent$ = this.notification.asObservable();

	constructor() {
	}

	public error(msg: string, title?: string) {
		this.notifyThis('error', msg, title);
	}

	public info(msg: string, title?: string) {
		this.notifyThis('info', msg, title)
	}

	public success(msg: string, title?: string) {
		this.notifyThis('success', msg, title);
	}

	public warning(msg: string, title?: string) {
		this.notifyThis('warning', msg, title);
	}

	private notifyThis(notificationType: string, msg: string, title?: string): void {
		if(msg) {
			let notification = <Notification>{
				type: notificationType,
				title: title,
				message: msg
			};
			this.notifyIt(notification);
		}
	}

	private notifyIt(notification: Notification) {
		this.notification.next(notification);
	}
}
