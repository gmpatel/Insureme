import { NgModule }     from '@angular/core';
import { RouterModule } from '@angular/router';

import { ClientComponent } from './client.component';
import { HomeComponent } from './home/home.component';
import { ClientGuard } from '../_core/services/client.guard';


export let MainRoutes = [
	{
		path: 'client',
		component: ClientComponent,
		children: [
			{
				path: '',
				canActivate: [ClientGuard],
				children: [
					{ path: '',  component: HomeComponent }
				]
			}
		],
	}
];

@NgModule({
	imports: [
		RouterModule.forChild(MainRoutes)
	],
	exports: [
		RouterModule
	]
})
export class ClientRoutesModule { }
