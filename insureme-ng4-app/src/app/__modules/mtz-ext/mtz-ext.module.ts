import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MtzInputComponent } from './mtz-input/mtz-input.component';
import { MaterializeModule } from 'angular2-materialize';
import { FormsModule } from '@angular/forms';

@NgModule({
	imports: [
		CommonModule,
		FormsModule,
		MaterializeModule
	],
	declarations: [
		MtzInputComponent
	],
	exports: [
		MtzInputComponent
	]
})
export class MtzExtModule {
}
