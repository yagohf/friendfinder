import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';
import { UsuarioLogado } from '../_models/usuariologado';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  isCollapsed = true;

  constructor(private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.infosUsuarioLogado$ = this.authenticationService.infosUsuarioLogado;
  }

  logoff() {
    this.authenticationService.logout();
    this.router.navigate(['/acesse']);
  }

  infosUsuarioLogado$: Observable<UsuarioLogado>;
}
