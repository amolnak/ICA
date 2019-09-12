import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ClientComponent } from './client/client.component';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from 'selenium-webdriver/http';
import  { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    ClientComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers:  [FormBuilder,HttpClient],
  bootstrap: [AppComponent]
})
export class AppModule { }
