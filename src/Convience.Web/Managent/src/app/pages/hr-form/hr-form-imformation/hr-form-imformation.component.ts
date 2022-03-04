import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { HrFormService } from '../../../business/eip/hr-form.service';
@Component({
  //以本地css為主
  encapsulation: ViewEncapsulation.None,
  selector: 'app-hr-form-imformation',
  templateUrl: './hr-form-imformation.component.html',
  styleUrls: ['./hr-form-imformation.component.less']
})
export class HrFormImformationComponent implements OnInit {
  imformationForm: FormGroup = new FormGroup({});

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

    this.imformationForm = this._formBuilder.group({
      iform_q1: [null,[Validators.required]],
      iform_q2: [null,[Validators.required]],
      iform_q3: [null,[Validators.required]],
      iform_q4: [null,[Validators.required]],
      iform_q5: [null,[Validators.required]],
      iform_q6: [null,[Validators.required]],
      iform_q7: [null,[Validators.required]],
      iform_q8: [null,[Validators.required]],
      iform_q9: [null,[Validators.required]],
      iform_q10: [null,[Validators.required]],
      iform_q11: [null,[Validators.required]],
      iform_q12: [null,[Validators.required]],
    });
  }

}
