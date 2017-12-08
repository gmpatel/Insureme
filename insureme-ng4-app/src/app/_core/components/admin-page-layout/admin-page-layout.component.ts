import { Component, OnInit, Input } from '@angular/core';
import { UserAuthService } from '../../services/user-auth.service';

@Component({
	selector: 'admin-page-layout',
	templateUrl: './admin-page-layout.component.html',
	styleUrls: ['./admin-page-layout.component.scss']
})
export class AdminPageLayoutComponent implements OnInit {

	@Input() title: string;
	@Input() navigation: string;

	private toolSet1Tools = ['menu', 'fullscreen'];
	private toolSet2Tools = ['menu', 'fullscreen', 'exit_to_app'];

	constructor(private userAuthService: UserAuthService) {
	}

	ngOnInit() {
	}

	protected handleToolClick(tool) {
		if (tool == 'exit_to_app') {
			this.userAuthService.logout();
		}
	}
}
