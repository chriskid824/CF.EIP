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
}
