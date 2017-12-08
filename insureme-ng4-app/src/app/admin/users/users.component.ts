import { Component, OnInit } from '@angular/core';
import { UserService } from '../../_core/services/user.service';
import { NotificationsService } from '../../__modules/notifications';

@Component({
	selector: 'admin-users',
	templateUrl: './users.component.html',
	styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

	private users: any[];
	private counter = 0;

	constructor(private userService: UserService,
				private notifiationsService: NotificationsService) {
	}

	ngOnInit() {
		this.getLsitAllUsers();
	}

	protected notify() {
		this.counter++;

		if (this.counter % 4 == 0)
			this.notifiationsService.error('testing error ... test... test... test...notification');
		else if (this.counter % 4 == 1)
			this.notifiationsService.info('testing info notification');
		else if (this.counter % 4 == 2)
			this.notifiationsService.success('testing success notification');
		else if (this.counter % 4 == 3)
			this.notifiationsService.warning('testing warning notification');
	}

	private getLsitAllUsers(): void {
		this.userService.getAllUsers().subscribe((users) => {
			console.log('all available users', users);
			this.users = users;
		});
	}

}
