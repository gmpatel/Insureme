import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { UserAuthService } from './user-auth.service';
import { AlertService } from '../../__modules/alert/alert.service';
import { NotificationsService } from '../../__modules/notifications/notifications.service';

@Injectable()
export class UserGuard implements CanActivate {

	constructor(private userAuthService: UserAuthService,
				private router: Router,
				private alertService: AlertService,
				private notificationsService: NotificationsService) {
	}

	canActivate(next: ActivatedRouteSnapshot,
				state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {

		let url: string = state.url;
		return this.checkLogin(url);
	}

	private checkLogin(url: string): boolean {
		if (this.userAuthService.isLoggedIn()) {
			return true;
		}

		this.userAuthService.redirectUrl = url;

		let self = this;

		let msg = "Your authorisation doesn't permit to access 'User Area'. Please login with correct authorisation to access 'User Area'";

		setTimeout(() => {
			self.alertService.error(msg, 'Not Authorised');
			self.notificationsService.info(msg);
			self.router.navigate(['/login']);
			return false;
		}, 50);
	}
}
