import { Component, OnInit } from '@angular/core';
import { AppStoreService } from '../../_core/services/app-store.service';
import { UserService } from '../../_core/services/user.service';

@Component({
	selector: 'app-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

	constructor(private appStore: AppStoreService, private userService: UserService) {
	}

	ngOnInit() {
	}
}
