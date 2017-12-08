import { NgModule }     from '@angular/core';
import { RouterModule } from '@angular/router';

import { AdminComponent } from './admin.component';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { AdminGuard } from '../_core/services/admin.guard';

export let MainRoutes = [
	{
		path: 'admin',
		component: AdminComponent,
		children: [
			{
				path: '',
				canActivate: [AdminGuard],
				children: [
					{ path: '',  component: HomeComponent },
					{ path: 'users', component: UsersComponent },
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
export class AdminRoutesModule { }
