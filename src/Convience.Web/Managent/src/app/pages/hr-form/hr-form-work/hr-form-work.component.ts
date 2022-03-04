import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { HrFormService } from '../../../business/eip/hr-form.service';

@Component({
  //以本地css為主
  encapsulation: ViewEncapsulation.None,
  selector: 'app-hr-form-work',
  templateUrl: './hr-form-work.component.html',
  styleUrls: ['./hr-form-work.component.less']
})
export class HrFormWorkComponent implements OnInit {

  workForm: FormGroup = new FormGroup({});
  checked = true;

  public GUID: string;

  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,
    private _formBuilder: FormBuilder,) { }

  ngOnInit(): void {
    // this.GUID = this.route.snapshot.queryParamMap.get('GUID');
    // console.log(this.route.snapshot.queryParamMap.get('GUID'));
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
    this.workForm = this._formBuilder.group({
      name_ch: [null,[Validators.required]],
      name_en: [null,[Validators.required]],
      birthdate: [null,[Validators.required]],
      gender: [null,[Validators.required]],
      birthplace: [null,[Validators.required]],
      personid: [null,[Validators.required]],
      address: [null,[Validators.required]],
      institute: [null,[Validators.required]],
      department: [null,[Validators.required]],
      bachelorofscience: [null,[Validators.required]],
      year: [null,[Validators.required]],
      graduate: [null,[Validators.required]],
      graduate_y: [null,[Validators.required]],
      time: [null,[Validators.required]],
      phone: [null,[Validators.required]],
      ck: [false],
    });
  }

}
