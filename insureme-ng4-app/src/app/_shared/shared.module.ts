import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValidationService } from './services/validation.service';
import { BreadcrumbComponent } from './components/breadcrumb/breadcrumb.component';
import { ToolSetComponent } from './components/tool-set/tool-set.component';

@NgModule({
	imports: [
		CommonModule
	],
	providers: [
		ValidationService
	],
	declarations: [
		BreadcrumbComponent,
		ToolSetComponent
	],
	exports: [
		BreadcrumbComponent,
		ToolSetComponent
	]
})
export class SharedModule {
}
