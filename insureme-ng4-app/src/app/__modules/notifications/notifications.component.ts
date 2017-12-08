import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { NotificationsService } from './notifications.service';
import { Notification } from './notification/notification.model';
import { trigger, state, transition, style, animate } from '@angular/animations';

@Component({
	selector: 'notifications',
	templateUrl: './notifications.component.html',
	styleUrls: ['./notifications.component.scss'],
	animations: [
		trigger('flyInOut', [
			state('in', style({
				opacity: 1,
				height: '*',
				transform: 'translateY(0)'
			})),
			transition('void => *', [
				style({
					opacity: 0,
					height: 0,
					transform: 'translateY(100%)'
				}),
				animate('300ms ease-in')
			]),
			transition('* => void', [
				animate('300ms ease-out', style({
					opacity: 0,
					height: 0,
					transform: 'translateY(100%)'
				}))
			])
		])
	]
})
export class NotificationsComponent implements OnInit, OnDestroy {

	private notificationSubscription: Subscription;
	private notificationsStack = [];
	private uid: number;

	constructor(private notificationsService: NotificationsService) {
		this.notificationSubscription = this.notificationsService.notificationEvent$.subscribe((notification: Notification) => {
			this.uid ++;
			notification['id'] = this.uid;
			this.notificationsStack.push(notification);
			console.log(this.notificationsStack);
		});
	}

	ngOnInit() {
		this.uid = 0;
	}

	ngOnDestroy() {
		this.notificationSubscription.unsubscribe();
	}

	protected deteleNotification(notification: Notification) {
		let foundIndex = -1
		this.notificationsStack.forEach((n, i) => {
			if(n.id == notification.id) {
				foundIndex = i;
			}
		});
		this.notificationsStack.splice(foundIndex, 1);
	}
}
