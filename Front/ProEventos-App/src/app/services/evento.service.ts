import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Evento } from '../models/Evento';

@Injectable(
  // {providedIn: 'root'}
)

export class EventoService {

  // baseURL = 'https://localhost:44378/api/Evento';
  baseURL = environment.apiUrl + 'api/Evento';


  constructor(private http:HttpClient) { }

  public getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`);
  }

  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  public post(evento: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL, evento);
  }

  public put(evento: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento);
  }

  public deleteEvento(id: number): Observable<any> {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  public postUpload(idEvento: number, file: File | any): Observable<Evento> {
    const fileTOUpload = file[0] as File;
    const formData = new FormData();
    formData.append('file', fileTOUpload);

    return this.http.post<Evento>(`${this.baseURL}/uploadImage/${idEvento}`, formData);
  }
}
