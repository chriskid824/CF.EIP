import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StorageService } from '../../../services/storage.service';

import { HrFormService } from '../../../business/eip/hr-form.service';
@Component({
  encapsulation: ViewEncapsulation.None,
  selector: 'app-hr-form-consent',
  templateUrl: './hr-form-consent.component.html',
  styleUrls: ['./hr-form-consent.component.less']
})
export class HrFormConsentComponent implements OnInit {
  consentForm: FormGroup = new FormGroup({});
  public GUID= this.route.snapshot.queryParamMap.get('GUID');
  logonid:string;

  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _storageService: StorageService, ) { }

  ngOnInit(): void {
    this.logonid=this._storageService.userName;
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
      User: this._storageService.userName,
    }
    this._eipHrFormService.CheckFormURL(query).subscribe(result => {
      console.log(result);
      if(!result)
      {
        alert("無權限進入此頁面，請聯繫HR");
        this._router.navigate(['hr-form/hr-form-error']);
      }
      else
      {
        if (this.logonid!=null)
        {
          this.getData();
        }
      }
    });
    this.consentForm = this._formBuilder.group({
      name: [null,[Validators.required]],
      id: [null,[Validators.required]],
    });
  }
  work(){
    console.log("hr-form/hr-form-work?GUID="+this.GUID);
    this._router.navigate(["hr-form/hr-form-work"],{queryParams:{GUID:this.GUID}});
  }
  imformation(){
    this._router.navigate(['hr-form/hr-form-imformation'],{queryParams:{GUID:this.GUID}});
  }
  consent(){
    this._router.navigate(['hr-form/hr-form-consent'],{queryParams:{GUID:this.GUID}});
  }
  getData(){
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
    }
    this._eipHrFormService.GetConsentData(query).subscribe(result => {
      this.consentForm = this._formBuilder.group({
        name: [result['username']],
        id: [result['identity']],
      });
      console.info(result);
    });
  }
  send(){
    if(this.consentForm.valid)
    {
      var consent ={
        GUID : this.route.snapshot.queryParamMap.get('GUID'),
        username : this.consentForm.value['name'],
        identity : this.consentForm.value['id'],
      }
      this._eipHrFormService.SendConsent(consent).subscribe(result => {
        alert("儲存成功");
      });
    }
    else
    {
      for (const i in this.consentForm.controls) {
        this.consentForm.controls[i].markAsDirty();
        this.consentForm.controls[i].updateValueAndValidity();
      }
    }
  }

}
