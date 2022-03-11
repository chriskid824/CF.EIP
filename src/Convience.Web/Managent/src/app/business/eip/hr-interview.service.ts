import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UriConfig } from 'src/app/configs/uri-config';

@Injectable({
  providedIn: 'root'
})
export class HrInterviewService {

  constructor(private httpClient: HttpClient,private uriConstant: UriConfig) { }
  GetInterviewList(query) {
    return this.httpClient.post(`${this.uriConstant.HrInterview}/GetInterviewList`, query);
  }
  GetInterviewDetail(query) {
    return this.httpClient.post(`${this.uriConstant.HrInterview}/GetInterviewDetail`, query);
  }    
  update(interview) {
    return this.httpClient.post(`${this.uriConstant.HrInterview}/Update`, interview);
  }
  DeleteList(data) {
    return this.httpClient.post(`${this.uriConstant.HrInterview}/DeleteList`, data);
  }
  add(data) {
    return this.httpClient.post(`${this.uriConstant.HrInterview}/AddList`, data);
  }
  GetCandidateList(query){
    return this.httpClient.post(`${this.uriConstant.HrInterview}/GetCandidateList`, query);
  }
  SendMail(query){
    return this.httpClient.post(`${this.uriConstant.HrInterview}/SendMail`, query);
  }
}
