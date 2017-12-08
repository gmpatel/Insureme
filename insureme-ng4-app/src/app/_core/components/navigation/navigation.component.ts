import { Component, OnInit, Input } from '@angular/core';
import { NavigationItem } from '../../services/config.service';

@Component({
	selector: 'navigation',
	templateUrl: './navigation.component.html',
	styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent implements OnInit {

	@Input() data: NavigationItem[];

	constructor() {
	}

	ngOnInit() {
	}

}
