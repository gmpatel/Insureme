import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebApiService } from './services/-web-api.service';
import { AdminGuard } from './services/admin.guard';
import { UserService } from './services/user.service';
import { ClientGuard } from './services/client.guard';
import { UserGuard } from './services/user.guard';
import { UserAuthService } from './services/user-auth.service';
import { AdminPageLayoutComponent } from './components/admin-page-layout/admin-page-layout.component';
import { AppStoreService } from './services/app-store.service';
import { ConfigService } from './services/config.service';
import { NavigationComponent } from './components/navigation/navigation.component';
import { NavigationItemComponent } from './components/navigation-item/navigation-item.component';
import { SharedModule } from '../_shared/shared.module';

@NgModule({
	imports: [
		CommonModule,
		SharedModule
	],
	declarations: [
		AdminPageLayoutComponent,
		NavigationComponent,
		NavigationItemComponent
	],
	providers: [
		WebApiService,
		UserGuard,
		UserAuthService,
		AdminGuard,
		ClientGuard,
		UserService,
		AppStoreService,
		ConfigService
	],
	exports: [
		AdminPageLayoutComponent
	]
})
export class CoreModule {
}
