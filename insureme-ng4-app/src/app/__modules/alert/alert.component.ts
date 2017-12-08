import { Component, OnInit, OnDestroy } from '@angular/core';
import { AlertService } from './alert.service';
import { Subscription } from 'rxjs/Subscription';
import { Alert } from './alert.model';
import { AlertEvent } from './alert.enums';

@Component({
	selector: 'alert',
	templateUrl: 'alert.component.html',
	styleUrls: ['alert.component.scss']
})
export class AlertComponent implements OnInit, OnDestroy {

	private alertSubscription: Subscription;

	private show = false;
	private msg: string;
	private title: string;

	constructor(private alertService: AlertService) {
		this.alertSubscription = this.alertService.newAlertEvent$.subscribe((alert: Alert) => {
			this.title = alert.title;
			this.msg = alert.content;
			let self = this;
			setTimeout(function() {
				self.show = true;
				self.alertService.raiseAlertEvent(AlertEvent.Visible);
			}, 50);
		});
	}

	ngOnInit() {
	}

	ngOnDestroy() {
		this.alertSubscription.unsubscribe();
	}

	protected cancelClicked() {
		this.show = false;
		this.alertService.raiseAlertEvent(AlertEvent.Cancel_Clicked);
	}

	protected okClicked() {
		this.show = false;
		this.alertService.raiseAlertEvent(AlertEvent.Ok_Clicked);
	}

}
