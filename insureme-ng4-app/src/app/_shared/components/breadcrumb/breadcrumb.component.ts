import { Component, OnInit, Input } from '@angular/core';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';

@Component({
	selector: 'breadcrumb',
	templateUrl: './breadcrumb.component.html',
	styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent implements OnInit {

	@Input() data: string[];

	constructor(router: Router) {
		router.events.subscribe((val) => {
			let url = val['url'].split('/');
			url.shift();
			url.map((w, i) => {
				url[i] = this.toTitleCase(w)
			});
			this.data = url;
		});
	}

	private toTitleCase(str)
	{
		return str.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
	}

	ngOnInit() {
	}

}
