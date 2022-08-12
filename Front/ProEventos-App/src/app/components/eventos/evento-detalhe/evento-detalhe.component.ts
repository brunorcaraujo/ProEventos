import { Component, OnInit, TemplateRef } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { Lote } from '@app/models/Lote';
import { LoteService } from '@app/services/lote.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  evento = {} as Evento;
  form!: FormGroup;
  estadoSalvar = 'post';
  eventoId: number = 0;
  modalRef?: BsModalRef;
  loteAtual = {id: 0, nome: '', indice: 0};
  imagemURL = 'assets/image/upload-image.png';
  file: File | any;

  get modoEditar(): boolean {
    return this.estadoSalvar == 'put';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  get bsConfigLote(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }

  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activateRouter: ActivatedRoute,
    private eventoService: EventoService,
    private loteService: LoteService,
    private spinner: NgxSpinnerService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private toaster: ToastrService,
    private router: Router
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit() {
    this.carregarEvento();
    this.validacao();
  }

  public validacao(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(10)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: [''],
      lotes: this.fb.array([]),
    });
  }

  addLote(): void {
    this.lotes.push(this.criarLote({ id: 0 } as Lote));
  }

  criarLote(lote: Lote): FormGroup {
    return this.fb.group({
      id: [lote.id],
      nome: [lote.nome, Validators.required],
      quantidade: [lote.quantidade, Validators.required],
      valor: [lote.valor, Validators.required],
      dataInicio: [lote.dataInicio],
      dataFim: [lote.dataFim],
    });
  }

  public retornaTituloLote(nome: string) : string {
    return nome == null || nome == '' ? 'Nome do Lote' : nome
  }

  public limparForm(): void {
    this.form.reset();
  }

  public cssValidador(campoForm: FormControl | AbstractControl): any {
    return {
      'is-invalid':
        campoForm?.errors && (campoForm?.dirty || campoForm?.touched),
    };
  }

  public carregarEvento(): void {
    const id = this.activateRouter.snapshot.paramMap.get('id');

    if (id != null) this.eventoId = +id;

    if (this.eventoId != null && this.eventoId != 0) {
      this.estadoSalvar = 'put';
      this.spinner.show();

      this.eventoService.getEventoById(this.eventoId).subscribe({
        next: (evento: Evento) => {
          // this.evento = Object.assign({}, evento);
          this.evento = { ...evento };
          this.form.patchValue(this.evento);

          if (this.evento.imagemURL !== '') {
            this.imagemURL = environment.apiUrl + 'resources/images/' + this.evento.imagemURL;
          }

          this.evento.lotes.forEach((lote) => {
            this.lotes.push(this.criarLote(lote));
          });
          // this.carregarLotes();
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toaster.error('Erro ao carregar evento...', 'Erro!');
          console.error(error);
        },
        complete: () => {
          this.spinner.hide();
        },
      });
    }
  }

  public carregarLotes(): void {
    this.loteService
      .getLotesByEventoId(this.eventoId)
      .subscribe({
        next: (lotesRetorno: Lote[]) => {
          lotesRetorno.forEach((lote) => {
            this.lotes.push(this.criarLote(lote));
          });
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toaster.error('Erro ao carregar evento...', 'Erro!');
          console.error(error);
        },
      })
      .add(() => this.spinner.hide());
  }

  public salvarEvento(): void {
    this.spinner.show();

    if (this.form.valid) {
      this.evento =
        this.estadoSalvar == 'post'
          ? { ...this.form.value }
          : { id: this.evento.id, ...this.form.value };

      if (this.estadoSalvar == 'post') {
        this.eventoService.post(this.evento).subscribe({
          next: (eventoRetorno: Evento) => {
            this.toaster.success('Evento cadastrado!', 'Sucesso');
            this.spinner.hide();
            this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
          },
          error: (error: any) => {
            this.toaster.error('Erro ao cadastrar evento...', 'Erro');
            this.spinner.hide();
            console.error(error);
          },
          complete: () => {
            this.spinner.hide();
            // this.router.navigate([`/eventos/lista`]);
          },
        });
      } else {
        this.eventoService.put(this.evento).subscribe({
          next: (eventoRetorno: Evento) => {
            this.toaster.success('Evento atualizado!', 'Sucesso');
            this.spinner.hide();
            this.router.navigate([`eventos/detalhe/${eventoRetorno.id}`]);
          },
          error: (error: any) => {
            this.toaster.error('Erro ao atualizar evento...', 'Erro');
            this.spinner.hide();
            console.error(error);
          },
          complete: () => {
            this.spinner.hide();
            // this.router.navigate([`/eventos/lista`]);
          },
        });
      }
    }
  }

  public salvarLote(): void {

    if (this.lotes.valid) {
      this.spinner.show();

      this.loteService
        .saveLote(this.eventoId, this.form.value.lotes)
        .subscribe({
          next: () => {
            this.toaster.success('Lotes salvos!', 'Sucesso');
            this.lotes.reset();
            // this.carregarLotes();
            this.spinner.hide();
          },
          error: (error: any) => {
            this.toaster.error('Erro ao salvar lotes...', 'Erro');
            console.error(error);
            this.spinner.hide();
          },
          complete: () => {
            this.spinner.hide();
          },
        });
    }
  }

  public removerLote(template: TemplateRef<any>, indice: number): void {

    this.loteAtual.id = this.lotes.get(indice + '.id')?.value;
    this.loteAtual.nome = this.lotes.get(indice + '.nome')?.value;
    this.loteAtual.indice = indice;

    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirmDeleteLote(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.loteService
      .deleteLote(this.eventoId, this.loteAtual.id)
      .subscribe({
        next: (result: string) => {
          console.log(result);
          this.toastr.success(`Lote deletado com sucesso...`, 'Deletado!');
          this.lotes.removeAt(this.loteAtual.indice);
          this.spinner.hide();
          // this.getEventos();
        },
        error: (error: any) => {
          console.log(error);
          this.toastr.error(`Erro ao tentar deletar o evento ${this.loteAtual.nome}`,'Erro');
          this.spinner.hide();
        },
      })
      .add(() => this.spinner.hide());
  }

  declineDeleteLote(): void {
    this.modalRef?.hide();
  }

  onFileChange(ev: any): void {
    const reader = new FileReader();

    reader.onload = (event: any) => this.imagemURL = event.target.result;

    this.file = ev.target.files;

    reader.readAsDataURL(this.file[0]);

    this.uploadImagem();
  }

  uploadImagem(): void {
    this.spinner.show();
    this.eventoService.postUpload(this.eventoId, this.file).subscribe(
      () => {
        this.carregarEvento();
        this.toastr.success('Imagem atualizada com Sucesso', 'Sucesso!');
      },
      (error: any) => {
        this.toastr.error('Erro ao fazer upload de imagem', 'Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

}
