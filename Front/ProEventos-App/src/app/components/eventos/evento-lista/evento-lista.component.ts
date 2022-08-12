import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public eventoId: number = 0;

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
              private spinner: NgxSpinnerService,
              private router: Router) {}

  ngOnInit() {
    this.spinner.show();
    this.getEventos();
  }

  public getEventos(): void{

    this.eventoService.getEventos().subscribe({
      next: (_eventos : Evento[]) => {
        this.eventos = _eventos,
        this.eventosFiltrados = this.eventos

        console.log(this.eventos);
      },
      error: (error : any) => {
        console.log(error);
        this.spinner.hide();
        this.toastr.error('Falha ao carregar eventos!', 'Falhou...');
      },
      complete: () => this.spinner.hide()
    });

  }

  public alterarImagem(): void {
    this.mostrarImagem = !this.mostrarImagem;
    console.log("mostrarImagem: " + this.mostrarImagem);
  }

  public mostraImagem(imagemUrl: string): string{
    return (imagemUrl != '') ? `${environment.apiUrl}resources/images/${imagemUrl}` : `assets/image/upload-image.png`;
  }

  public filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento: {tema : string}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) != -1
    )
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number) {

    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});

  }

  confirm(): void {

    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvento(this.eventoId).subscribe({
      next: (result: string) => {
          console.log(result);
          this.toastr.success(`Evento deletado com sucesso...`, 'Deletado!');
          this.spinner.hide();
          this.getEventos();
      },
      error: (error : any) => {
        console.log(error);
        this.toastr.error(`Erro ao tentar deletar o evento ${ this.eventoId }`, 'Erro');
        this.spinner.hide();
      },
      complete: () => this.spinner.hide()
    });
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(id: number): void {
    this.router.navigate([`/eventos/detalhe/${id}`])
    console.log(id);
  }

}
