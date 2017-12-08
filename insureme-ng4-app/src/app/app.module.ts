import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MaterializeModule } from 'angular2-materialize';
import { MainModule } from './main/main.module';
import { AdminModule } from './admin/admin.module';

import { AppRoutesModule } from './app.routes';

import { AppComponent } from './app.component';
import { ClientModule } from './client/client.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MaterialModule } from '@angular/material';
import { MtzExtModule } from './__modules/mtz-ext/mtz-ext.module';
import { UserModule } from './user/user.module';
import { CoreModule } from './_core/core.module';
import { SharedModule } from './_shared/shared.module';
import { NotificationsModule } from './__modules/notifications/notifications.module';
import { AlertModule } from './__modules/alert/alert.module';



@NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		MaterialModule,
		MaterializeModule,
		AlertModule,
		NotificationsModule,
		MtzExtModule,
		CoreModule,
		SharedModule,
		MainModule,
		AdminModule,
		ClientModule,
		UserModule,
		AppRoutesModule
	],
	declarations: [
		AppComponent,
		LoginComponent,
		RegisterComponent
	],
	providers: [

	],
	bootstrap: [
		AppComponent
	]
})
export class AppModule {
}
