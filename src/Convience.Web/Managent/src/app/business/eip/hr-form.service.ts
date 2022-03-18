import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriConfig } from 'src/app/configs/uri-config';

@Injectable({
  providedIn: 'root'
})
export class HrFormService {

  constructor(private httpClient: HttpClient,private uriConstant: UriConfig) {}
  CheckFormURL(GUID) {
    return this.httpClient.post(`${this.uriConstant.HrForm}/CheckFormURL`, GUID);
  }
  SendImformation(imformation){
    return this.httpClient.post(`${this.uriConstant.HrForm}/SendImformation`, imformation);
  }
  SendWork(work){
    return this.httpClient.post(`${this.uriConstant.HrForm}/SendWork`, work);
  }
  GetWorkData(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/GetWorkData`, GUID);
  }
  GetImformationData(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/GetImformationData`, GUID);
  }
  SendConsent(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/SendConsent`, GUID);
  }
  GetConsentData(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/GetConsentData`, GUID);
  }
  PrintImformation(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/PrintImformation`, GUID);
  }
  PrintWork(GUID){
    return this.httpClient.post(`${this.uriConstant.HrForm}/PrintWork`, GUID);
  }
}
