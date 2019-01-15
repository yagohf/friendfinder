import { Component, OnInit } from '@angular/core';
import { ViewChild } from '@angular/core';
import { } from 'googlemaps';
import { AmigoService } from '../_services/amigo.service';
import { Amigo } from '../_models/amigo';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @ViewChild('gmap') gmapElement: any;
  map: google.maps.Map;
  markers: google.maps.Marker[] = new Array<google.maps.Marker>();
  infoWindow: google.maps.InfoWindow = new google.maps.InfoWindow();

  constructor(private amigoService: AmigoService) { }

  ngOnInit() {
    var mapProp = {
      center: new google.maps.LatLng(-16.227646, -48.018715),
      zoom: 5,
      mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);

    this.obterTodosAmigos();
  }

  obterTodosAmigos() {
    this.limparMapa();
    this.amigoService.listarTodos().subscribe(retorno => {
      retorno.lista.forEach(x => this.criarMarcador(x));
    });
  }

  limparMapa() {
    this.infoWindow.close();
    this.markers.forEach(x => x.setMap(null));
    this.markers = new Array<google.maps.Marker>();
  }

  criarMarcador(amigo: Amigo) {
    let marcador = new google.maps.Marker({
      position: {
        lat: amigo.latitude,
        lng: amigo.longitude,
      },
      map: this.map,
      title: amigo.nome
    });

    marcador.addListener('click', () => {
      this.exibirInformacoesMarcador(marcador);
    });

    this.markers.push(marcador);
  }

  exibirInformacoesMarcador(marcador: google.maps.Marker): any {
    this.infoWindow.close();

    this.infoWindow = new google.maps.InfoWindow({
      content: '<i class="fa fa-user"></i> ' + marcador.getTitle()
    });

    this.infoWindow.open(this.map, marcador);
  }
}
