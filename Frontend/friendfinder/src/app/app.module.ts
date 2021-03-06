import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS  } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { AcesseComponent } from './acesse/acesse.component';
import { HomeComponent } from './home/home.component';
import { MensagensComponent } from './mensagens/mensagens.component';
import { LoaderComponent } from './loader/loader.component';
import { MenuComponent } from './menu/menu.component';

import { AuthenticationService } from './_services/authentication.service';
import { AuthGuard } from './_guards/auth.guard';
import { AuthInterceptor } from './_interceptors/auth.interceptor';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { LoaderInterceptor } from './_interceptors/loader.interceptor';

import { AppRoutingModule } from './_routing/app-routing.module';
import { AppNgxbootsbundleModule } from './_ngxbundle/ngxbootstrapbundle.module';

@NgModule({
  declarations: [
    AppComponent,
    AcesseComponent,
    HomeComponent,
    MenuComponent,
    MensagensComponent,
    LoaderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AppNgxbootsbundleModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [AuthGuard, AuthenticationService,  
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
