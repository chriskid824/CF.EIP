import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriConfig } from 'src/app/configs/uri-config';

@Injectable({
  providedIn: 'root'
})
export class HrUserService {

  constructor(private httpClient: HttpClient,private uriConstant: UriConfig) { }
  GetCandidatelList(query) {
    return this.httpClient.post(`${this.uriConstant.HrCandidate}/GetCandidatelList`, query);
  }
  GetCandidateDetail(query) {
    return this.httpClient.post(`${this.uriConstant.HrCandidate}/GetCandidateDetail`, query);
  }    
  update(candidate) {
    return this.httpClient.post(`${this.uriConstant.HrCandidate}/Update`, candidate);
  }
  DeleteList(data) {
    return this.httpClient.post(`${this.uriConstant.HrCandidate}/DeleteList`, data);
  }
  add(data) {
    return this.httpClient.post(`${this.uriConstant.HrCandidate}/AddList`, data);
  }
}
