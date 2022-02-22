import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriConfig } from 'src/app/configs/uri-config';

@Injectable({
  providedIn: 'root'
})
export class HrUserService {

  constructor(private httpClient: HttpClient,private uriConstant: UriConfig) { }
  GetCandidatelList(query) {
    return this.httpClient.post(`${this.uriConstant.EipHR}/GetCandidatelList`, query);
  }
  GetUserDetail(query) {
    return this.httpClient.post(`${this.uriConstant.EipHR}/GetUserDetail`, query);
  }    
  update(material) {
    return this.httpClient.post(`${this.uriConstant.EipHR}/UpdateMaterial`, material);
  }
}
