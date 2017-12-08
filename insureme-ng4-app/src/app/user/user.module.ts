import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserComponent } from './user.component';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../_core/core.module';
import { SharedModule } from '../_shared/shared.module';
import { UserRoutesModule } from './user.routes';
import { MtzExtModule } from '../__modules/mtz-ext/mtz-ext.module';
import { MaterialModule } from '@angular/material';
import { MaterializeModule } from 'angular2-materialize';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		CoreModule,
		SharedModule,
		MaterialModule,
		MaterializeModule,
		MtzExtModule,
		UserRoutesModule
	],
	declarations: [
		UserComponent,
		HomeComponent
	]
})
export class UserModule {
}
