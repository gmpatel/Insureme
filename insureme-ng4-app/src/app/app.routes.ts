import { NgModule }     from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

export let AppRoutes = [
	{
		path: '',
		component: AppComponent,
		children: [
			{
				path: 'login',
				component: LoginComponent
			},
			{
				path: 'sign-up',
				component: RegisterComponent
			}
		]
	}
];

@NgModule({
	imports: [
		RouterModule.forRoot(AppRoutes)
	],
	exports: [
		RouterModule
	]
})
export class AppRoutesModule { }
