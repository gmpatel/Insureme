import { Component, OnInit, Input } from '@angular/core';
import { NavigationItem } from '../../services/config.service';
import { Router } from '@angular/router';

@Component({
	selector: 'navigation-item',
	templateUrl: './navigation-item.component.html',
	styleUrls: ['./navigation-item.component.scss']
})
export class NavigationItemComponent implements OnInit {

	@Input() data: NavigationItem;
	private open = false;

	constructor(private router: Router) {
	}

	ngOnInit() {
	}

	protected navItemClicked() {
		if(this.data.subNav) {
			this.toggleNav();
		} else {
			this.navigate();
		}
	}

	private navigate() {
		console.log('navigation me to : ', this.data.url);
		this.router.navigate([this.data.url]);
	}

	private toggleNav() {
		this.open = !this.open;
	}
}
