import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { HrFormService } from '../../../business/eip/hr-form.service';

@Component({
  selector: 'app-hr-form-work',
  templateUrl: './hr-form-work.component.html',
  styleUrls: ['./hr-form-work.component.less']
})
export class HrFormWorkComponent implements OnInit {

  public GUID: string;

  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,) { }

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
  }

}
