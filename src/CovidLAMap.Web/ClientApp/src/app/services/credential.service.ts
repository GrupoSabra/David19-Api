import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

export enum Sex {
  Male = 0,
  Female = 1,
  Unspecified = 2,
  Other = 3
}

export enum CredentialType {
  Confinement = 0,
  Interruption = 1,
  Symptoms = 2,
  Infection = 3,
  Recovery = 4
}

export enum InterruptionReason {
  None = 0,
  Purchase = 1,
  AttendanceHealthCenter = 2,
  CommutingWork = 3,
  ReturnResidence = 4,
  AssistPeople = 5,
  CommutingFinancial = 6,
  ForceMajeure = 7
}

export interface Credential {
  id: number;
  hashId: string;
  citizenAddress: string;
  subjectHashId: string;
  startDate: Date;
  credentialCreation: Date;
  sex: Sex;
  location: any;
  credintialType: CredentialType;
  reason: InterruptionReason;
  lat: number;
  lon: number;
  countryName: string;
}


@Injectable({
  providedIn: 'root'
})
export class CredentialService {

  constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  public async getAll(): Promise<Credential[]>
  {
    return await this.httpClient.get<Credential[]>(this.baseUrl + "api/credential/all").toPromise();
  }
}
