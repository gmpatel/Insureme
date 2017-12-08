import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientComponent } from './client.component';
import { HomeComponent } from './home/home.component';
import { FormsModule } from '@angular/forms';
import { ClientRoutesModule } from './client.routes';
import { SharedModule } from 'primeng/components/common/shared';
import { CoreModule } from '../_core/core.module';
import { CoolStorageModule } from 'angular2-cool-storage';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		SharedModule,
		CoreModule,
		CoolStorageModule,
		ClientRoutesModule
	],
	declarations: [
		ClientComponent,
		HomeComponent
	]
})
export class ClientModule {
}
