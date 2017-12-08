import { NgModule }     from '@angular/core';
import { RouterModule } from '@angular/router';

import { MainComponent } from './main.component';
import { HomeComponent } from './home/home.component';

export let MainRoutes = [
	{
		path: '',
		component: MainComponent,
		children: [
			{ path: '',  component: HomeComponent },
		]
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
export class MainRoutesModule { }
