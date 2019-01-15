import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Amigo } from '../_models/amigo';
import { tap } from 'rxjs/operators';
import { Listagem } from '../_models/infraestrutura/listagem';

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
}
