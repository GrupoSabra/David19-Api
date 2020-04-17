import { Component, OnInit } from '@angular/core';
import { CredentialService, Credential } from '../services/credential.service';

import * as L from 'leaflet';
import { Map, tileLayer, latLng, marker, icon, markerClusterGroup } from 'leaflet';
import 'leaflet.markercluster';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  options = {
    layers: [
      tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { maxZoom: 18, attribution: '&copy; <a href = "https://www.openstreetmap.org/copyright">OpenStreetMap</a>' })
    ],
    zoom: 3,
    center: latLng(-17.05686960, -64.99122860)
  };

  layers = [];
  myMap: Map;
  credentialsPromise: Promise<Credential[]>;
  markerClusterData: any[] = [];
  markerClusterOptions: L.MarkerClusterGroupOptions;


  constructor(private credentialService: CredentialService) {
    this.credentialsPromise =  this.credentialService.getAll();
  }

  onMapReady(map: Map) {
    this.myMap = map;
  }

  async markerClusterReady(group: L.MarkerClusterGroup) {

    const credentials = await this.credentialsPromise;
    console.log('h');
    const markers = markerClusterGroup();

    for (const cred of credentials) {
      const mark = marker([cred.location.coordinates[1], cred.location.coordinates[0]],
        {
          icon: icon({
            iconSize: [25, 41],
            iconAnchor: [13, 41],
            iconUrl: 'assets/marker-icon.png',
            shadowUrl: 'assets/marker-shadow.png'
          })
        });
      markers.addLayer(mark);
    }

    this.myMap.addLayer(markers);
  }

  async ngOnInit() {
  //  let credentials = await this.credentialService.getAll();
  //  if (!credentials || credentials.length <= 0) return;

    //for (var cred of credentials) {
    //  //let point = new Point(cred.location.coordinates[0], cred.location.coordinates[1]);
    //  //let latlng = 
    //  const mark = marker([cred.location.coordinates[1], cred.location.coordinates[0]],
    //    {
    //      icon: icon({
    //        iconSize: [25, 41],
    //        iconAnchor: [13, 41],
    //        iconUrl: 'assets/marker-icon.png',
    //        shadowUrl: 'assets/marker-shadow.png'
    //      })
    //    });
  //    this.layers.push(mark);
  //  }
  }
}
