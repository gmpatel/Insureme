import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MainRoutesModule } from './main.routes';

import { MainComponent } from './main.component';
import { HomeComponent } from './home/home.component';
import { CoreModule } from '../_core/core.module';
import { SharedModule } from '../_shared/shared.module';

@NgModule({
	imports: [
		CommonModule,
		CoreModule,
		SharedModule,
		MainRoutesModule,
		FormsModule
	],
	declarations: [
		MainComponent,
		HomeComponent
	]
})
export class MainModule {
}
