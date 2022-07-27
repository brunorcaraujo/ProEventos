import { Component, OnInit, TemplateRef } from '@angular/core';

import { ToastrService } from 'ngx-toastr';
import { EventoService } from '../../services/evento.service';
import { Evento } from '../../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
  // providers: [EventoService]
})

export class EventosComponent implements OnInit {

  modalRef?: BsModalRef;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public mostrarImagem = true;

  private _filtroLista: string = '';

  public get filtroLista(): string{
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {}

  ngOnInit() {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void{

    this.eventoService.getEventos().subscribe({
      next: (_eventos : Evento[]) => {
        this.eventos = _eventos,
        this.eventosFiltrados = this.eventos
      },
      error: (error : any) => {
        this.spinner.hide(),
        this.toastr.error('Falha ao carregar eventos!', 'Falhou...');
      },
      complete: () => this.spinner.hide()
    });

  }

  public alterarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
    console.log("mostrarImagem: " + this.mostrarImagem);
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: {tema : string}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) != -1
    )
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastr.success('Evento deletado com sucesso...', 'Deletado!');
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
