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
  addForm: FormGroup = new FormGroup({});

  isNewUser: boolean;
  tplModal: NzModalRef;
  page: number = 1;
  size: number = 6;
  total: number;
  data;
  fileList: any[] = [];

  editedUser: user;


  constructor(
    private _formBuilder: FormBuilder,
    //private _srmSrmService: SrmSupplierService,
    private _storageService: StorageService, 
    //private _layout: LayoutComponent, 
    private _router: Router,
    private _modalService: NzModalService,
    private _messageService: NzMessageService,
    private _eipHrUserService: HrUserService,) { }

  ngOnInit(): void {
    this.searchForm = this._formBuilder.group({
      //candidateId:[null],
      name:[null]
    });
  }
  submitSearch() {
    this.refresh();
  }
  pageChange() {
    this.refresh();
  }
  addmaterial() {
    //this._layout.navigateTo('material-c');
    this._router.navigate(['srm/material-c']);
    //window.open('../srm/rfq');
  }
  edit(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user) {    
    var query = {
      candidateId:user.candidateId,
    }
    console.log(query);
    this._eipHrUserService.GetCandidateDetail(query).subscribe(result => {      

      console.log(result);

      this.isNewUser = false;
      this.editedUser = result;
      this.editForm = this._formBuilder.group({
        candidateId: [result['candidateId']],
        username: [result['username']],
        cellphone: [result['cellphone']],
        email: [result['email']],  
        status: [result['status']],        
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
  initaddForm() {
    this.addForm = this._formBuilder.group({
      candidateId: [null],
      username: [null, [Validators.required]],
      cellphone: [null, [Validators.required]],
      email: [null, [Validators.required]],
    });
    this.fileList = [];
  }
  add(title: TemplateRef<{}>, content: TemplateRef<{}>) {   
    this.initaddForm(); 
    this.tplModal = this._modalService.create({
      nzTitle: title,
      nzContent: content,
      nzFooter: null,
      nzClosable: true,
      nzMaskClosable: false
    });
  }
  delete(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user){
    var data = {
      candidateId:user.candidateId,
      username:user.username
    }

    this.tplModal = this._modalService.confirm({
      nzTitle: '你確定要刪除應聘人員：'+data.username+' 嗎？',
      //nzContent: '<b style="color: red;">Some descriptions</b>',
      nzOkText: '確認',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzOnOk: () => this.submitDelete(data),
      nzCancelText: '取消',
      //nzOnCancel: () => alert("取消")
    });

  }
  submitDelete(data){
    this._eipHrUserService.DeleteList(data).subscribe((result) => {
      alert("刪除成功");
      this.refresh();
      this.tplModal.close();
    });
  }

  submitEdit(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user) {
    for (const i in this.editForm.controls) {
      this.editForm.controls[i].markAsDirty();
      this.editForm.controls[i].updateValueAndValidity();
    }
    if (this.editForm.valid) {
      let candidate: any = {};
      candidate.candidateid = this.editForm.value['candidateId'];     
      candidate.username = this.editForm.value['username'];
      candidate.cellphone = this.editForm.value['cellphone'];
      candidate.email = this.editForm.value['email'];
      candidate.status = this.editForm.value['status'];    
      candidate.user = this._storageService.userName;

      this._eipHrUserService.update(candidate).subscribe(result => {
        this._messageService.success("更新成功！");
        this.tplModal.close();
        this.refresh();
      });
    }
  } 
  submitadd(title: TemplateRef<{}>, content: TemplateRef<{}>,user:user) {
    for (const i in this.editForm.controls) {
      this.editForm.controls[i].markAsDirty();
      this.editForm.controls[i].updateValueAndValidity();
    }
    if (this.editForm.valid) {
      let candidate: any = {};  
      candidate.username = this.addForm.value['username'];
      candidate.cellphone = this.addForm.value['cellphone'];
      candidate.email = this.addForm.value['email'];
      candidate.user = this._storageService.userName;

      this._eipHrUserService.add(candidate).subscribe(result => {
        this._messageService.success("新增成功");
        this.tplModal.close();
        this.refresh();
      });
    }
  }
  canceladd() {
    this.tplModal.close();
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
