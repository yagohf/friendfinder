import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../_services/authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authenticationService: AuthenticationService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //Adicionar o token de autenticação a cada request.
        let usuarioLogado = this.authenticationService.obterUsuarioLogado();
        if (usuarioLogado) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${usuarioLogado.token}`
                }
            });
        }

        return next.handle(request);
    }
}