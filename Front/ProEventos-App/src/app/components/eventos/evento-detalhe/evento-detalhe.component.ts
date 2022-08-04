import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})

export class EventoDetalheComponent implements OnInit {

  evento = {} as Evento;
  form: FormGroup | any;
  estadoSalvar: string = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
              isAnimated: true,
              adaptivePosition: true,
              dateInputFormat: 'DD/MM/YYYY hh:mm a',
              containerClass: 'theme-default',
              showWeekNumbers: false
            }
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventoServie: EventoService,
              private spinner: NgxSpinnerService,
              private toaster: ToastrService,
              private routerNav: Router) {
    this.localeService.use('pt-br');
  }

  ngOnInit() {
    this.carregarEvento();
    this.validacao();
  }

  public validacao(): void {
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)] ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.min(1), Validators.max(10)] ],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email] ],
      imagemURL: ['', Validators.required]
    });
  }

  public limparForm(): void {
    this.form.reset();
  }

  public cssValidador(campoForm: FormControl): any {
    return {'is-invalid': campoForm.errors && (campoForm.dirty || campoForm.touched)}
  }

  public carregarEvento(): void {

    const eventIdParam = this.router.snapshot.paramMap.get('id');

    if (eventIdParam != null){

      this.estadoSalvar = 'put';
      this.spinner.show();

      this.eventoServie.getEventoById(+eventIdParam).subscribe({
        next: (evento: Evento) => {
          // this.evento = Object.assign({}, evento);
          this.evento = {...evento};
          this.form.patchValue(this.evento);
        },
        error: (error: any) => {
          this.spinner.hide();
          this.toaster.error('Erro ao carregar evento...', 'Erro!')
          console.error(error);
        },
        complete: () => {
          this.spinner.hide();
        }
      });
    }
  }

  public salvarAlteracao():void {

    this.spinner.show();

    if (this.form.valid) {

      if (this.estadoSalvar == 'post') {
        this.evento = {...this.form.value};
        this.eventoServie.postEvento(this.evento).subscribe({
          next: () => {
            this.toaster.success('Evento cadastrado!', 'Sucesso')
            this.spinner.hide();
          },
          error: (error: any) => {
            this.toaster.error('Erro ao cadastrar evento...', 'Erro')
            this.spinner.hide();
            console.error(error);
          },
          complete: () => {
            this.spinner.hide();
            this.routerNav.navigate([`/eventos/lista`]);
          }
        });
      } else {
        this.evento = {id: this.evento.id , ...this.form.value};
        this.eventoServie.putEvento(this.evento.id, this.evento).subscribe({
          next: () => {
            this.toaster.success('Evento atualizado!', 'Sucesso')
            this.spinner.hide();
          },
          error: (error: any) => {
            this.toaster.error('Erro ao atualizar evento...', 'Erro')
            this.spinner.hide();
            console.error(error);
          },
          complete: () => {
            this.spinner.hide();
            this.routerNav.navigate([`/eventos/lista`]);
          }
        });
      }
    }
  }
}
