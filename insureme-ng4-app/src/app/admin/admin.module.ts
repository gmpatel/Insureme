import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '@angular/material';
import { MaterializeModule } from 'angular2-materialize';
import { MtzExtModule } from '../__modules/mtz-ext/mtz-ext.module';
import { SharedModule } from '../_shared/shared.module';
import { CoreModule } from '../_core/core.module';
import { CoolStorageModule } from 'angular2-cool-storage';
import { AdminRoutesModule } from './admin.routes';

import { AdminComponent } from './admin.component';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import 'hammerjs';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		BrowserAnimationsModule,
		MaterialModule,
		MaterializeModule,
		MtzExtModule,
		SharedModule,
		CoreModule,
		CoolStorageModule,
		AdminRoutesModule
	],
	declarations: [
		AdminComponent,
		HomeComponent,
		UsersComponent
	]
})
export class AdminModule {
}
