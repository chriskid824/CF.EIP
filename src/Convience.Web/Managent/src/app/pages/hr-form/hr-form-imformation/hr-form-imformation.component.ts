import { Component, OnInit,ViewEncapsulation } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StorageService } from '../../../services/storage.service';
import { FileService } from '../../../business/file.service';
import { FileInfo } from '../../content-manage/model/fileInfo';

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
  public GUID= this.route.snapshot.queryParamMap.get('GUID');
  logonid:string;
  
  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _storageService: StorageService, 
    private _fileService: FileService,) { }

  ngOnInit(): void {
    this.logonid=this._storageService.userName;
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
    this.checkPermission();
    // var query = {
    //   Guid: this.route.snapshot.queryParamMap.get('GUID'),
    //   User: this._storageService.userName,
    // }
    // this._eipHrFormService.CheckFormURL(query).subscribe(result => {
    //   console.log(result);
    //   if(!result)
    //   {
    //     alert("無權限進入此頁面，請聯繫HR");
    //     this._router.navigate(['hr-form/hr-form-error']);
    //   }
    //   else
    //   {
    //     this.getData();
    //   }
    // });   
  }
  checkPermission(){
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
  }
  getData(){
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
    }
    this._eipHrFormService.GetImformationData(query).subscribe(result => {
      this.imformationForm = this._formBuilder.group({
        iform_q1: [result['q1']],
        iform_q2: [result['q2']],
        iform_q3: [result['q3']],
        iform_q4: [result['q4']],
        iform_q5: [result['q5']],
        iform_q6: [result['q6']],
        iform_q7: [result['q7']],
        iform_q8: [result['q8']],
        iform_q9: [result['q9']],
        iform_q10: [result['q10']],
        iform_q11: [result['q11']],
        iform_q12: [result['q12']],
      });
      console.info(result);
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
  send(){
    if(this.imformationForm.valid)
    {
      var imformation ={
        GUID : this.route.snapshot.queryParamMap.get('GUID'),
        q1 : this.imformationForm.value['iform_q1'],
        q2 : this.imformationForm.value['iform_q2'],
        q3 : this.imformationForm.value['iform_q3'],
        q4 : this.imformationForm.value['iform_q4'],
        q5 : this.imformationForm.value['iform_q5'],
        q6 : this.imformationForm.value['iform_q6'],
        q7 : this.imformationForm.value['iform_q7'],
        q8 : this.imformationForm.value['iform_q8'],
        q9 : this.imformationForm.value['iform_q9'],
        q10 : this.imformationForm.value['iform_q10'],
        q11 : this.imformationForm.value['iform_q11'],
        q12 : this.imformationForm.value['iform_q12'],
      }
      this._eipHrFormService.SendImformation(imformation).subscribe(result => {
        alert("儲存成功");
        window.close();
      });
    }
    else
    {
      //alert(222);
      for (const i in this.imformationForm.controls) {
        this.imformationForm.controls[i].markAsDirty();
        this.imformationForm.controls[i].updateValueAndValidity();
      }
    }
  }
  print(){
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
    }
    this._eipHrFormService.PrintImformation(query).subscribe(result => {
      console.info(result);
      var fileInfo = new FileInfo();
      fileInfo.fileName= result["fileName"]+".docx";
      fileInfo.directory = "Report/Output";
      this._fileService.download(fileInfo.fileName, fileInfo.directory).subscribe((result: any) => {
        const a = document.createElement('a');
        const blob = new Blob([result], { 'type': "application/octet-stream" });
        a.href = URL.createObjectURL(blob);
        console.log(blob);
        a.download = fileInfo.fileName;
        a.click();
      });
    });
  }
}
