import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from 'rxjs';

@Injectable()
export class ConfigService {

	private configBase = './configs';
	private mapping = {
		'admin-nav': 'admin-nav'
	};

	constructor(private http: Http) {
	}

	public getAdminNav() {
		return this.loadConfig('admin-nav');
	}

	private getConfigFileUrl(config): string {
		return this.configBase + '/' + this.mapping[config] + '.config.json';
	}

	private loadConfig(config) {
		return this.http.request(this.getConfigFileUrl(config))
			.map(res => res.json());
	}
}

export interface NavigationItem {
	displayAs: string;
	url: string;
	icon?: string;
	subNav?: NavigationItem[];
}
