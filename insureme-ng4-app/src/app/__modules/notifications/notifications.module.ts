import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsComponent } from './notifications.component';
import { NotificationsService } from './notifications.service';
import { NotificationComponent } from './notification/notification.component';

@NgModule({
	imports: [
		CommonModule
	],
	declarations: [
		NotificationsComponent,
		NotificationComponent
	],
	providers: [
		NotificationsService
	],
	exports: [
		NotificationsComponent
	]
})
export class NotificationsModule {
}
