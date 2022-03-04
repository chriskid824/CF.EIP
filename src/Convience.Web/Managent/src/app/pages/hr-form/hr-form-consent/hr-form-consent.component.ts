import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { HrFormService } from '../../../business/eip/hr-form.service';
@Component({
  encapsulation: ViewEncapsulation.None,
  selector: 'app-hr-form-consent',
  templateUrl: './hr-form-consent.component.html',
  styleUrls: ['./hr-form-consent.component.less']
})
export class HrFormConsentComponent implements OnInit {
  consentForm: FormGroup = new FormGroup({});

  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,
    private _formBuilder: FormBuilder,) { }

  ngOnInit(): void {
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
    }
    this._eipHrFormService.CheckFormURL(query).subscribe(result => {
      console.log(result);
      if(!result)
      {
        alert("無權限進入此頁面，請聯繫HR");
        this._router.navigate(['account/login']);
      }
    });
    this.consentForm = this._formBuilder.group({
      name: [null,[Validators.required]],
      id: [null,[Validators.required]],
    });
  }

}
