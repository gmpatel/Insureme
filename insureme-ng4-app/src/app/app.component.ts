import { Component, OnInit } from '@angular/core';
import { AppStoreService } from './_core/services/app-store.service';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
	title = 'InsureMe!!!';

	constructor(private appStore: AppStoreService) {
		console.log('initializing app...');
	}

	ngOnInit() {
	}
}
