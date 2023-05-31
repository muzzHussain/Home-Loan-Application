import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { UserModule } from './user/user.module';
import { AdvisorModule } from './advisor/advisor.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { UserService } from './services/userService';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: [AppComponent],
    imports:[
      BrowserModule,
      BrowserAnimationsModule,
      AppRoutingModule,
      CoreModule,
      SharedModule,
      AdvisorModule,
      HttpClientModule
      
    ],
    providers:[UserService],
    bootstrap:[AppComponent]
})
export class AppModule {}
