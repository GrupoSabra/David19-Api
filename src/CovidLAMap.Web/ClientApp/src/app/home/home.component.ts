import { Component, OnInit } from '@angular/core';
import { CredentialService, Credential, CredentialType } from '../services/credential.service';

import * as L from 'leaflet';
import { Map, tileLayer, latLng, marker, icon, markerClusterGroup } from 'leaflet';
import 'leaflet.markercluster';
import { stringify } from 'querystring';
import { Title } from '@angular/platform-browser';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  constructor(private credentialService: CredentialService) {
    this.credentialsPromise =  this.credentialService.getAll();
  }

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
  credsByCountryCount: Dictionary = new Dictionary([]);


  onMapReady(map: Map) {
    this.myMap = map;
  }

  async markerClusterReady(group: L.MarkerClusterGroup) {

    const credentials = await this.credentialsPromise;
    const credentialsByCountry: Dictionary = new Dictionary([]);
    const typesByCountryCount: Dictionary = new Dictionary([]);

    for (const cred of credentials) {
      const mark = marker([cred.location.coordinates[1], cred.location.coordinates[0]],
        {
          icon: icon(this.getIcon(cred.credintialType)),
          title: this.getText(cred.credintialType)
        });

       if (!credentialsByCountry.containsKey(cred.countryName)) {
         credentialsByCountry.add(cred.countryName, new Array<any>());
         typesByCountryCount.add(cred.countryName, { Confinement : 0,
          Interruption : 0,
          Symptoms : 0,
          Infection : 0,
          Recovery : 0, total: 0});
       }
       credentialsByCountry[cred.countryName].push({credential: cred, mark: mark});
       this.addCredential(typesByCountryCount, cred);

    }

   for (const country of credentialsByCountry.keys()) {
     const marks = credentialsByCountry[country];
     const markers = markerClusterGroup({spiderLegPolylineOptions : { weight: 1.5, color: '#5D5E60', opacity: 0.5 }});
     marks.forEach(element => {
      markers.addLayer(element.mark);
     });
     this.myMap.addLayer(markers);
   }

   this.credsByCountryCount = typesByCountryCount;
  }


  private addCredential(typesByCountryCount: Dictionary, cred: Credential) {
    typesByCountryCount[cred.countryName].total++;
    if(cred.credintialType == CredentialType.Confinement)
      typesByCountryCount[cred.countryName].Confinement++;
    else if(cred.credintialType == CredentialType.Infection)
      typesByCountryCount[cred.countryName].Infection++;
    else if(cred.credintialType == CredentialType.Interruption)
      typesByCountryCount[cred.countryName].Interruption++;
    else if(cred.credintialType == CredentialType.Recovery)
      typesByCountryCount[cred.countryName].Recovery++;
    else if(cred.credintialType == CredentialType.Symptoms)
      typesByCountryCount[cred.countryName].Symptoms++;
  }

  getIcon(credType: CredentialType): L.IconOptions {
    const green: L.IconOptions = {
      iconSize: [25, 41],
      iconAnchor: [13, 41],
      iconUrl: 'assets/marker-icon-green.png',
      shadowUrl: 'assets/marker-shadow.png'
    };

    const purple = { ...green} ;
    purple.iconUrl = 'assets/marker-icon-violet.png';
    const red = { ...green} ;
    red.iconUrl = 'assets/marker-icon-red.png';
    const yellow = { ...green} ;
    yellow.iconUrl = 'assets/marker-icon-gold.png';
    const orange = { ...green} ;
    orange.iconUrl = 'assets/marker-icon-orange.png';

    if(credType == CredentialType.Confinement) return green;
    if(credType == CredentialType.Infection) return red;
    if(credType == CredentialType.Interruption) return yellow;
    if(credType == CredentialType.Recovery) return purple;
    if(credType == CredentialType.Symptoms) return orange;
  }

  getText(credType: CredentialType) {
    if(credType == CredentialType.Confinement) return "Confinamiento";
    if(credType == CredentialType.Infection) return "Infectado";
    if(credType == CredentialType.Interruption) return "Interrupción";
    if(credType == CredentialType.Recovery) return "Recupero";
    if(credType == CredentialType.Symptoms) return "Con Sintomas";
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

interface IDictionary {
  add(key: string, value: any): void;
  remove(key: string): void;
  containsKey(key: string): boolean;
  keys(): string[];
  values(): any[];
}

class Dictionary {

  _keys: string[] = new Array<string>();
  _values: any[] = new Array<any>();

  constructor(init: { key: string; value: any; }[]) {

      for (let x = 0; x < init.length; x++) {
          this[init[x].key] = init[x].value;
          this._keys.push(init[x].key);
          this._values.push(init[x].value);
      }
  }

  add(key: string, value: any) {
      this[key] = value;
      this._keys.push(key);
      this._values.push(value);
  }

  remove(key: string) {
      const index = this._keys.indexOf(key, 0);
      this._keys.splice(index, 1);
      this._values.splice(index, 1);

      delete this[key];
  }

  keys(): string[] {
      return this._keys;
  }

  values(): any[] {
      return this._values;
  }

  containsKey(key: string) {
      if (typeof this[key] === 'undefined') {
          return false;
      }

      return true;
  }

  toLookup(): IDictionary {
      return this;
  }
}
