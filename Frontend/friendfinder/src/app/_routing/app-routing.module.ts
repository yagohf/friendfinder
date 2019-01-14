import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from '../home/home.component';
import { AcesseComponent } from '../acesse/acesse.component';
import { AuthGuard } from '../_guards/auth.guard';
import { AmigosComponent } from '../amigos/amigos.component';

const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard]},
  { path: 'acesse', component: AcesseComponent },
  { path: 'amigos', component: AmigosComponent , canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {

}