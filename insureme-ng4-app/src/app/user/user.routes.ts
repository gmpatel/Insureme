import { NgModule }     from '@angular/core';
import { RouterModule } from '@angular/router';

import { UserComponent } from './user.component';
import { HomeComponent } from './home/home.component';
import { UserGuard } from '../_core/services/user.guard';

export let MainRoutes = [
	{
		path: 'user',
		component: UserComponent,
		children: [
			{
				path: '',
				canActivate: [UserGuard],
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
export class UserRoutesModule { }
