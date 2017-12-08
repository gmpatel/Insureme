import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../_core/services/config.service';

@Component({
	selector: 'app-admin',
	templateUrl: './admin.component.html',
	styleUrls: [
		'./admin.component.scss'
	]
})
export class AdminComponent implements OnInit {

	private navigation: any[];

	constructor(private configService: ConfigService) {
	}

	ngOnInit() {
		this.loadAdminConfigs();
	}

	private loadAdminConfigs() {
		this.loadNav();
	}

	private loadNav() {
		console.log('loading navigation');
		this.configService.getAdminNav().subscribe((result) => {
			this.navigation = result;
			console.log('navigation loaded');
			console.log(this.navigation);
		});
	}
}

