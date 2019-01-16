import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Amigo } from '../_models/amigo';
import { tap } from 'rxjs/operators';
import { Listagem } from '../_models/infraestrutura/listagem';
import { AmigosProximos } from '../_models/amigos-proximos';

@Injectable({
  providedIn: 'root'
})
export class AmigoService {

  constructor(private http: HttpClient) { }

  listarTodos(): Observable<Listagem<Amigo>> {
    const url = `${environment.enderecoApi}/amigos`;
    return this.http.get<Listagem<Amigo>>(url)
      .pipe(
        tap(_ => console.log(_))
      );
  }

  listarMaisProximos(amigo:Amigo): Observable<AmigosProximos> {
    const url = `${environment.enderecoApi}/amigos/${amigo.id}/proximos`;
    return this.http.get<AmigosProximos>(url)
      .pipe(
        tap(_ => console.log(_))
      );
  }

  excluir(amigo: Amigo) {
    const url = `${environment.enderecoApi}/amigos/${amigo.id}`;
    return this.http.delete(url)
      .pipe(
        tap(_ => console.log(_))
      );
  }

  criar(amigo: Amigo): Observable<Amigo> {
    const url = `${environment.enderecoApi}/amigos`;
    return this.http.post<Amigo>(url, amigo)
      .pipe(
        tap(_ => console.log(_))
      );
  }
}
