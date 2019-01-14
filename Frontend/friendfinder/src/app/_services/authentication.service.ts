import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { BehaviorSubject, throwError } from 'rxjs';
import { UsuarioLogado } from '../_models/usuariologado';
import { Autenticacao } from '../_models/autenticacao';

@Injectable()
export class AuthenticationService {
  constructor(private http: HttpClient) { }

  static USUARIO_DESLOGADO: UsuarioLogado = { autenticado: false, login: null };

  //Observable para expor o status do usuário como logado ou não.
  private usuarioLogado = new BehaviorSubject<UsuarioLogado>(this.obterUsuarioLogado());
  get infosUsuarioLogado() {
    return this.usuarioLogado.asObservable();
  }

  login(autenticacao: Autenticacao) {
    return this.http.post<any>(`${environment.enderecoApi}/usuarios/token`, autenticacao)
      .pipe(map(resultado => {
        //Login com sucesso se o retorno contiver um token.
        if (resultado && resultado.status == 1 && resultado.token) {
          //Guardar o token em localstorage para poder manter o usuário logado entre refreshs.
          localStorage.setItem('usuarioLogado', JSON.stringify(resultado.token));
        }

        //Enviar mensagem de usuário logado (ou não) para quem quer que esteja observando.
        this.usuarioLogado.next(this.obterUsuarioLogado());
        return resultado;
      }));
  }

  logout() {
    //Remover o usuário da localstorage.
    localStorage.removeItem('usuarioLogado');

    //Enviar mensagem de usuário deslogado para quem quer que esteja observando.
    this.usuarioLogado.next(AuthenticationService.USUARIO_DESLOGADO);
  }

  obterUsuarioLogado(): UsuarioLogado {
    let u = new UsuarioLogado();
    u.autenticado = false;
    u.login = null;

    if (localStorage.getItem('usuarioLogado')) {
      u.autenticado = true;
      u.login = this.obterLoginUsuario();
    }

    return u;
  }

  obterLoginUsuario(): string {
    var token = localStorage.getItem('usuarioLogado');
    if (!token) {
      return null;
    }
    else {
      var objetoCorpoJWT = this.obterTokenDecodificado(token);
      return objetoCorpoJWT['cognito:username'] || null;
    }
  }

  private obterTokenDecodificado(token: string): any {
    var corpoJWT = token.split('.')[1];
    var objetoCorpoJWT = JSON.parse(window.atob(corpoJWT));
    return objetoCorpoJWT;
  }
}