import { Component, OnInit,ViewEncapsulation,ViewChild,TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StorageService } from '../../../services/storage.service';
import { LayoutComponent } from '../../layout/layout/layout.component';
import { Router } from '@angular/router';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { EipModule } from '../eip.module';
import { NzMessageService } from 'ng-zorro-antd/message';
import { user } from '../../system-manage/model/hr-user';

import { HrUserService } from '../../../business/eip/hr-user.service';

@Component({
  selector: 'app-hr-user',
  templateUrl: './hr-user.component.html',
  styleUrls: ['./hr-user.component.less']
})
export class HrUserComponent implements OnInit {
  searchForm: FormGroup = new FormGroup({});
  editForm: FormGroup = new FormGroup({});

  isNewUser: boolean;
  tplModal: NzModalRef;
  page: number = 1;
  size: number = 6;
  total: number;
  data;

  editedUser: user;


  constructor(
    private _formBuilder: FormBuilder,
    //private _srmSrmService: SrmSupplierService,
    private _storageService: StorageService, 
    private _layout: LayoutComponent, 
    private _router: Router,
    private _modalService: NzModalService,
    private _messageService: NzMessageService,
    private _eipHrUserService: HrUserService,) { }

  ngOnInit(): void {
    this.searchForm = this._formBuilder.group({
      name:[null]
    });
  }
  submitSearch() {
    //this.refresh();
  }
  pageChange() {
    //this.refresh();
  }
  addmaterial() {
    //this._layout.navigateTo('material-c');
    this._router.navigate(['srm/material-c']);
    //window.open('../srm/rfq');
  }
  edit(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user) {

    //console.log(material.srmMatnr1)
    var query = {
      material:user.userid
    }
    this._eipHrUserService.GetUserDetail(query).subscribe(result => {      

      console.log(result);

      this.isNewUser = false;
      this.editedUser = result;
      this.editForm = this._formBuilder.group({
        srmMatnr1: [result['srmMatnr1']],
        sapMatnr: [result['sapMatnr']],
        matnrGroup: [result['matnrGroup']],
        description: [result['description']],
        version: [result['version']],
        material: [result['material']],
        length: [result['length']],
        width: [result['width']],
        height: [result['height']],
        density: [result['density']],
        weight: [result['weight']],
        gewei: [result['gewei']],
        unitDesc: [result['unitDesc']],
        ekgrp: [result['ekgrp']],
        bnnum: [result['bn_num']],
        major_diameter: [result['major_diameter']],
        minor_diameter: [result['minor_diameter']],
        statusDesc: [result['statusDesc']],
        note: [result['note']],
        otherDesc: [result['otherDesc']],
      });
      this.tplModal = this._modalService.create({
        nzTitle: title,
        nzContent: content,
        nzFooter: null,
        nzClosable: true,
        nzMaskClosable: false
      });
    });
  }
  delete(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user){
    var data = {
      material:user.userid
    }

    // this.tplModal = this._modalService.confirm({
    //   nzTitle: '你確定要刪除料號：'+data.srmMatnr1+' 嗎？',
    //   //nzContent: '<b style="color: red;">Some descriptions</b>',
    //   nzOkText: '確認',
    //   nzOkType: 'primary',
    //   nzOkDanger: true,
    //   nzOnOk: () => this.submitDelete(data),
    //   nzCancelText: '取消',
    //   //nzOnCancel: () => alert("取消")
    // });

  }
  submitDelete(data){
    // this._eipHrUserService.DeleteList(data).subscribe((result) => {
    //   alert("刪除成功");
    //   this.refresh();
    //   this.tplModal.close();
    // });
  }

  submitEdit() {
    for (const i in this.editForm.controls) {
      this.editForm.controls[i].markAsDirty();
      this.editForm.controls[i].updateValueAndValidity();
    }
    if (this.editForm.valid) {
      let material: any = {};
      material.srmMatnr1 = this.editForm.value['srmMatnr1'];
      material.sapMatnr = this.editForm.value['sapMatnr'];
      material.matnrGroup = this.editForm.value['matnrGroup'];
      material.description = this.editForm.value['description'];
      material.version = this.editForm.value['version'];
      material.material = this.editForm.value['material'];
      material.length=this.editForm.value['length'];
      material.width = this.editForm.value['width'];
      material.height = this.editForm.value['height'];
      material.density = this.editForm.value['density'];
      material.weight = this.editForm.value['weight'];
      material.statusDesc = this.editForm.value['statusDesc'];
      material.note = this.editForm.value['note'];
      material.user = this._storageService.userName;
      material.gewei = this.editForm.value['gewei'];
      material.unitDesc = this.editForm.value['unitDesc'];
      material.ekgrp = this.editForm.value['ekgrp'];
      material.bn_num = this.editForm.value['bnnum'];
      material.minor_diameter = this.editForm.value['minor_diameter'];
      material.major_diameter = this.editForm.value['major_diameter'];
      material.otherDesc = this.editForm.value['otherDesc'];
      console.log(this._storageService.userName);

      this._eipHrUserService.update(material).subscribe(result => {
        this._messageService.success("更新成功！");
        this.tplModal.close();
        this.refresh();
      });
    }
  } 


  cancelEdit() {
    this.tplModal.close();
  }

  refresh() {
    var query = {
      name: this.searchForm.value["name"] == null ? "" : this.searchForm.value["name"],
      page: this.page,
      size: this.size
    }
    //console.log(query);
    this._eipHrUserService.GetCandidatelList(query).subscribe(result => {
      console.log(this.data);
      this.data = result["data"];
      this.total = result["count"];
    });
  }

}
