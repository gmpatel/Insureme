import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Notification } from './notification.model';
import { trigger, state, transition, style, animate } from '@angular/animations';


@Component({
	selector: 'notification',
	templateUrl: './notification.component.html',
	styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

	@Input() data: Notification;
	@Output() timeUp = new EventEmitter();

	constructor() {
	}

	ngOnInit() {
		let self = this;
		setTimeout(function() {
			console.log('time up..');
			self.timeUp.next(self.data);
		}, 10000);
	}
}
