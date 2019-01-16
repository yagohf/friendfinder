import { Component, OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import { } from 'googlemaps';
import { AmigoService } from '../_services/amigo.service';
import { MensagensService } from '../_services/mensagens.service';
import { Amigo } from '../_models/amigo';
import { EnumTipoMarcador } from '../_models/enums/enum.tipomarcador';
import { AmigoMarcador } from '../_models/amigo-marcador';
import { EnumMensagem } from '../_models/enums/enum.mensagem';
import { positionElements } from 'ngx-bootstrap';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('gmap') gmapElement: any;
  map: google.maps.Map;
  markers: AmigoMarcador[] = new Array<AmigoMarcador>();
  infoWindow: google.maps.InfoWindow = new google.maps.InfoWindow();
  posicaoAtual: Amigo = new Amigo();
  modoInclusao: boolean = false;

  constructor(private amigoService: AmigoService, private mensagemService: MensagensService) { }

  ngOnInit() {
    var mapProp = {
      center: new google.maps.LatLng(-16.227646, -48.018715),
      zoom: 5,
      mapTypeId: google.maps.MapTypeId.ROADMAP,
      mapTypeControl: false,
      streetViewControl: false,
      zoomControl: false,
      rotateControl: false,
      scaleControl: false,
      fullscreenControl: false
    };

    this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);

    this.obterTodosAmigos();
    this.mensagemService.enviarMensagem('Dica: Selecione um de seus amigos para informar sua localização e poder encontrar os amigos mais próximos.', EnumMensagem.SUCESSO);
  }

  reiniciarMapa() {
    this.limparMarcadores();
    this.posicaoAtual = new Amigo();
    this.obterTodosAmigos();
  }

  obterTodosAmigos() {
    this.limparMarcadores();
    this.amigoService.listarTodos().subscribe(retorno => {
      retorno.lista.forEach(x => this.criarMarcador(x));
    });
  }

  limparMarcadores() {
    this.infoWindow.close();
    this.markers.forEach(x => x.marcador.setMap(null));
    this.markers = new Array<AmigoMarcador>();
  }

  criarMarcador(amigo: Amigo) {
    let marcador = new google.maps.Marker({
      position: {
        lat: amigo.latitude,
        lng: amigo.longitude,
      },
      map: this.map,
      title: amigo.nome,
      icon: this.obterDesenhoMarcador(amigo.id == this.posicaoAtual.id ? EnumTipoMarcador.POSICAO_ATUAL : EnumTipoMarcador.COMUM),
      label: {
        text: amigo.nome,
        color: '#eb3a44',
        fontSize: '16px',
        fontWeight: 'bold',
      }
    });

    marcador.addListener('click', () => {
      this.exibirInformacoesMarcador(marcador, amigo);
    });

    this.markers.push(new AmigoMarcador(amigo, marcador));
  }

  exibirInformacoesMarcador(marcador: google.maps.Marker, amigo: Amigo): any {
    let htmlInfoWindow = '<div class="div-info-window">';
    htmlInfoWindow += '<div class="div-info-window-titulo text-center">';
    htmlInfoWindow += '<h3> <i class="fa fa-user"></i> ' + marcador.getTitle() + '</h3>';
    htmlInfoWindow += '</div>';
    htmlInfoWindow += '<div class="div-info-window-acoes">';
    htmlInfoWindow += '<button id="btnSetarPosicaoUsuario" type="button" class="btn btn-primary btn-rounded" style="display:' + (amigo.id == this.posicaoAtual.id ? 'none' : 'block') + ';"><i class="fa fa-map-marker"></i> Eu estou aqui !</button>';
    htmlInfoWindow += '<button id="btnRemoverPosicaoUsuario" type="button" class="btn btn-warning btn-rounded" style="display:' + (amigo.id == this.posicaoAtual.id ? 'block' : 'none') + ';"><i class="fa fa-map-marker"></i> Não estou mais aqui !</button>';
    htmlInfoWindow += '<button id="btnExcluirAmigo" type="button" class="btn btn-danger btn-rounded" style="display:' + (amigo.id == this.posicaoAtual.id ? 'none' : 'block') + ';"><i class="fa fa-trash-o"></i> Excluir</button>';
    htmlInfoWindow += '</div>';
    htmlInfoWindow += '</div>';

    this.infoWindow.close();

    this.infoWindow = new google.maps.InfoWindow({
      content: htmlInfoWindow
    });

    this.infoWindow.addListener('domready', () => {
      document.getElementById('btnSetarPosicaoUsuario').addEventListener('click', () => {
        this.setarPosicaoUsuario(marcador, amigo);
      });

      document.getElementById('btnRemoverPosicaoUsuario').addEventListener('click', () => {
        this.posicaoAtual = new Amigo();
        this.obterTodosAmigos();
      });

      document.getElementById('btnExcluirAmigo').addEventListener('click', () => {
        this.excluirAmigo(marcador, amigo);
      });
    });

    this.infoWindow.open(this.map, marcador);
  }

  criarMarcadorNovoAmigo(amigo: Amigo) {
    let marcador = new google.maps.Marker({
      position: {
        lat: amigo.latitude,
        lng: amigo.longitude,
      },
      map: this.map,
      title: amigo.nome,
      icon: this.obterDesenhoMarcador(EnumTipoMarcador.COMUM),
      label: {
        text: amigo.nome,
        color: '#eb3a44',
        fontSize: '16px',
        fontWeight: 'bold',
      }
    });

    marcador.addListener('click', () => {
      this.exibirJanelaNomearAmigo(marcador, amigo);
    });

    this.markers.push(new AmigoMarcador(amigo, marcador));
    this.exibirJanelaNomearAmigo(marcador, amigo);
    google.maps.event.clearListeners(this.map, 'click');
    this.map.panTo(marcador.getPosition());
  }

  exibirJanelaNomearAmigo(marcador: google.maps.Marker, amigo: Amigo) {
    let htmlInfoWindow = '<div class="div-info-window">';
    htmlInfoWindow += '<div class="div-info-window-titulo text-center">';
    htmlInfoWindow += '<h4> <i class="fa fa-user"></i> Informe o nome para o novo amigo: </h4>';
    htmlInfoWindow += '<input id="txtNomeNovoAmigo" type="text" maxlength="100" class="form-control" />';
    htmlInfoWindow += '</div>';
    htmlInfoWindow += '<div class="div-info-window-acoes">';
    htmlInfoWindow += '<button id="btnSalvarNovoAmigo" type="button" class="btn btn-primary btn-rounded"><i class="fa fa-save"></i> Salvar</button>';
    htmlInfoWindow += '</div>';
    htmlInfoWindow += '</div>';

    this.infoWindow.close();

    this.infoWindow = new google.maps.InfoWindow({
      content: htmlInfoWindow
    });

    this.infoWindow.addListener('domready', () => {
      document.getElementById('btnSalvarNovoAmigo').addEventListener('click', () => {
        this.salvarNovoAmigo(marcador, amigo);
      });
    });

    this.infoWindow.open(this.map, marcador);
    setTimeout(function () {
      document.getElementById('txtNomeNovoAmigo').focus();
    }, 500);
  }

  setarPosicaoUsuario(marcador: google.maps.Marker, amigo: Amigo) {
    this.infoWindow.close();
    this.posicaoAtual = amigo;
    this.map.panTo(new google.maps.LatLng(amigo.latitude, amigo.longitude));
    this.limparMarcadores();
    this.amigoService.listarMaisProximos(amigo).subscribe(retorno => {
      retorno.proximos.forEach(x => this.criarMarcador(x));
      this.criarMarcador(retorno.atual);
    });
  }

  obterDesenhoMarcador(tipo: EnumTipoMarcador): google.maps.Symbol {
    if (tipo == EnumTipoMarcador.POSICAO_ATUAL) {
      return {
        path: 'M 0,0 -1,-2 V -43 H 1 V -2 z M 1,-40 H 30 V -20 H 1 z',
        fillColor: '#f45942',
        fillOpacity: 1,
        strokeColor: '#000',
        strokeWeight: 2,
        scale: 1,
      };
    }
    else {
      return {
        path: 'M 0,0 C -2,-20 -10,-22 -10,-30 A 10,10 0 1,1 10,-30 C 10,-22 2,-20 0,0 z M -2,-30 a 2,2 0 1,1 4,0 2,2 0 1,1 -4,0',
        fillColor: '#2976f2',
        fillOpacity: 1,
        strokeColor: '#000',
        strokeWeight: 2,
        scale: 1,
      };
    }
  }

  excluirAmigo(marcador: google.maps.Marker, amigo: Amigo) {
    this.amigoService.excluir(amigo).subscribe(retorno => {
      this.obterTodosAmigos();
      this.mensagemService.enviarMensagem('Amigo excluído com sucesso', EnumMensagem.SUCESSO);
    });
  }

  salvarNovoAmigo(marcador: google.maps.Marker, amigo: Amigo) {
    let nomeNovoAmigo = (document.getElementById('txtNomeNovoAmigo') as HTMLInputElement).value;
    if (nomeNovoAmigo && nomeNovoAmigo.trim()) {
      amigo.nome = nomeNovoAmigo;
      this.amigoService.criar(amigo).subscribe(retorno => {
        this.cancelarModoInclusaoAmigo();
        this.obterTodosAmigos();
      });
    }
    else {
      this.mensagemService.enviarMensagem('Informe um nome para o novo amigo.', EnumMensagem.ERRO);
    }
  }

  abrirModoInclusaoAmigo() {
    this.limparMarcadores();
    this.modoInclusao = true;
    this.mensagemService.enviarMensagem('Você está no modo de inclusão de amigos. Clique sobre o mapa para adicionar um amigo no ponto desejado.', EnumMensagem.SUCESSO);

    this.map.addListener('click', (event) => {
      let amigo = new Amigo();
      amigo.latitude = event.latLng.lat();
      amigo.longitude = event.latLng.lng();
      amigo.nome = "Novo amigo";

      this.criarMarcadorNovoAmigo(amigo);
    });
  }

  cancelarModoInclusaoAmigo() {
    this.obterTodosAmigos();
    this.modoInclusao = false;
  }
}
