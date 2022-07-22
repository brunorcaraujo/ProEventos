import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})

export class EventosComponent implements OnInit {

  public eventos: any;
  mostrarImagem: boolean = true;
  private _filtroLista: string = '';
  public eventosFiltrados: any;

  public get filtroLista(): string{
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }


  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  public getEventos(): void{

    this.http.get('https://localhost:5001/api/Evento').subscribe(
      response => {
        this.eventos = response,
        this.eventosFiltrados = response
      },
      error => console.log(error)
    );
  }

  public alterarImagem(){
    console.log(this.mostrarImagem);

    this.mostrarImagem = !this.mostrarImagem;
  }

  filtrarEventos(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: {tema : string}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) != -1
    )
  }

}
