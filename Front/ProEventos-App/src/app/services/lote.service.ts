import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Lote } from '@app/models/Lote';
import { Observable } from 'rxjs/internal/Observable';

@Injectable(

  // {providedIn: 'root'}
)
export class LoteService {

  baseURL = 'https://localhost:44378/api/lote';

  constructor(private http : HttpClient) { }

  public getLotesByEventoId(idEvento : number): Observable<Lote[]> {
    return this.http.get<Lote[]>(`${this.baseURL}/${idEvento}`);
  }

  public saveLote(idEvento : number, lotes: Lote[]): Observable<Lote> {
    return this.http.put<Lote>(`${this.baseURL}/${idEvento}`, lotes);
  }

  public deleteLote(idEvento : number, idLote : number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${idEvento}/${idLote}`);
  }
}
