import { Evento } from "./Evento";

export interface Lote {
  id : number;
  nome : string;
  valor : number;
  dataInicio? : Date;
  dataFim? : Date;
  quantidade : number;
  eventoId: number;
  evento : Evento;
}
