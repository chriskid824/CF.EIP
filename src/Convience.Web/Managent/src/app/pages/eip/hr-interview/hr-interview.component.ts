import { Component, OnInit,ViewEncapsulation,ViewChild,TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StorageService } from '../../../services/storage.service';
import { LayoutComponent } from '../../layout/layout/layout.component';
import { Router } from '@angular/router';
import { NzModalRef, NzModalService } from 'ng-zorro-antd/modal';
import { EipModule } from '../eip.module';
import { NzMessageService } from 'ng-zorro-antd/message';
import { hr } from '../../system-manage/model/hr';

import { HrInterviewService } from '../../../business/eip/hr-interview.service';

@Component({
  selector: 'app-hr-interview',
  templateUrl: './hr-interview.component.html',
  styleUrls: ['./hr-interview.component.less']
})
export class HrInterviewComponent implements OnInit {
  searchForm: FormGroup = new FormGroup({});
  editForm: FormGroup = new FormGroup({});
  addForm: FormGroup = new FormGroup({});

  isNewUser: boolean;
  tplModal: NzModalRef;
  page: number = 1;
  size: number = 6;
  total: number;
  data;
  candidateList = [];

  editedInterview: hr;
  interviewDate:any;
  noticeDate:any;
  validDate:any;

  constructor(
    private _formBuilder: FormBuilder,
    //private _srmSrmService: SrmSupplierService,
    private _storageService: StorageService, 
    //private _layout: LayoutComponent, 
    private _router: Router,
    private _modalService: NzModalService,
    private _messageService: NzMessageService,
    private _eipHrInterviewService: HrInterviewService,) { }

  ngOnInit(): void {
    this.searchForm = this._formBuilder.group({
      //candidateId:[null],
      date:[null]
    });
  }

  submitSearch() {
    this.refresh();
  }
  pageChange() {
    this.refresh();
  }
  edit(title: TemplateRef<{}>, content: TemplateRef<{}>,interview:hr) {    
    var query = {
      InterviewId:interview.interviewId,
    }
    this._eipHrInterviewService.GetInterviewDetail(query).subscribe(result => {      
      this.isNewUser = false;
      this.editedInterview = result;
      this.editForm = this._formBuilder.group({
        interviewId: [result['interviewId']],
        candidate: [result['userName']],
        dept: [result['dept']],
        noticeDate: [result['noticeDate']],  
        interviewDate: [result['interviewDate']],  
        interviewer: [result['interviewer']],
        place: [result['place']],
        // replyDate: [result['replyDate']],
        // checkDate: [result['checkDate']],  
        validDate: [result['validDate']],        
      });
      this.interviewDate=result['interviewDate'];
      this.noticeDate=result['noticeDate'];
      this.validDate=result['validDate'];
      //console.info(result);
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
      //interviewId: [null],
      candidate: [null, [Validators.required]],
      dept: [null, [Validators.required]],
      noticedate: [null, [Validators.required]],
      interviewdate: [null, [Validators.required]],
      interviewer: [null, [Validators.required]],
      place: [null, [Validators.required]],
      // replydate: [null],
      // checkdate: [null],
      validdate: [null, [Validators.required]],
    });
    //this.initList();
  }
  initList() {   
    var querylist = {
      page: 1,
      size: 999
    }
    this._eipHrInterviewService.GetCandidateList(querylist).subscribe(result => {
      console.log(result);
      this.candidateList = result["data"];
    });   
  }
  add(title: TemplateRef<{}>, content: TemplateRef<{}>) {
    this.initList();
    this.initaddForm(); 
    this.tplModal = this._modalService.create({
      nzTitle: title,
      nzContent: content,
      nzFooter: null,
      nzClosable: true,
      nzMaskClosable: false
    });
  }
  delete(title: TemplateRef<{}>, content: TemplateRef<{}>,interview:hr){
    var data = {
      interviewId:interview.interviewId,
      candidate:interview.userName,
      interviewDate:interview.i_date.toString(),
      dept:interview.dept,
    }
    console.log(data);

    this.tplModal = this._modalService.confirm({
      nzTitle: '你確定要刪除此面試申請嗎？<br/><br/>應聘者：'+interview.userName+'<br/>面試職位：'+interview.dept+'<br/>面試時段：'+interview.i_date,
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
    this._eipHrInterviewService.DeleteList(data).subscribe((result) => {
      alert("刪除成功");
      this.refresh();
      this.tplModal.close();
    });
  }
  interviewDatechange(result:Date)
  {
    this.interviewDate=`${this.editForm.value['interviewDate'].getFullYear()}-${this.editForm.value['interviewDate'].getMonth() + 1}-${this.editForm.value['interviewDate'].getDate()} ${this.editForm.value['interviewDate'].getHours()}:${this.editForm.value['interviewDate'].getMinutes()}`;
    console.log(this.interviewDate);
  }
  noticeDatechange(result:Date)
  {
    this.noticeDate = `${this.editForm.value['noticeDate'].getFullYear()}-${this.editForm.value['noticeDate'].getMonth() + 1}-${this.editForm.value['noticeDate'].getDate()}`;
    console.log(this.noticeDate);
  }
  validDatechange(result:Date)
  {
    this.validDate = `${this.editForm.value['validDate'].getFullYear()}-${this.editForm.value['validDate'].getMonth() + 1}-${this.editForm.value['validDate'].getDate()}`; 
    console.log(this.validDate);
  }

  submitEdit(title: TemplateRef<{}>, content: TemplateRef<{}>,HR:hr) {
    for (const i in this.editForm.controls) {
      this.editForm.controls[i].markAsDirty();
      this.editForm.controls[i].updateValueAndValidity();
    }    
    console.info(this.validDate);
    if (this.editForm.valid) {
      let interview: any = {};
      interview.interviewId = this.editForm.value['interviewId'];   
      interview.dept = this.editForm.value['dept'];
      
      interview.noticeDate = this.noticeDate;
      interview.interviewDate = this.interviewDate;//this.editForm.value['interviewDate'];
      interview.validDate = this.validDate;
      // interview.noticeDate = `${this.editForm.value['noticeDate'].getFullYear()}-${this.editForm.value['noticeDate'].getMonth() + 1}-${this.editForm.value['noticeDate'].getDate()}`;
      // interview.interviewDate = `${this.editForm.value['interviewDate'].getFullYear()}-${this.editForm.value['interviewDate'].getMonth() + 1}-${this.editForm.value['interviewDate'].getDate()} ${this.editForm.value['interviewDate'].getHours()}:${this.editForm.value['interviewDate'].getMinutes()}`; 
      interview.interviewer = this.editForm.value['interviewer'];
      interview.place = this.editForm.value['place'];
      // interview.replyDate = this.editForm.value['replyDate'];
      // interview.checkDate = this.editForm.value['checkDate'];
      // interview.validDate = `${this.editForm.value['validDate'].getFullYear()}-${this.editForm.value['validDate'].getMonth() + 1}-${this.editForm.value['validDate'].getDate()}`; 
      interview.user = this._storageService.userName;  
      

      this._eipHrInterviewService.update(interview).subscribe(result => {
        this._messageService.success("更新成功！");
        this.tplModal.close();
        this.refresh();
      });
    }
  } 
  submitadd(title: TemplateRef<{}>, content: TemplateRef<{}>,HR:hr) {
    console.info(123);
    for (const i in this.addForm.controls) {
      this.addForm.controls[i].markAsDirty();
      this.addForm.controls[i].updateValueAndValidity();
    }
    if (this.addForm.valid) {
      let interview: any = {};  
      interview.candidate = this.addForm.value['candidate'];
      interview.dept = this.addForm.value['dept'];
      interview.noticedate = this.addForm.value['noticedate'];
      interview.interviewdate = 
      `${this.addForm.value['interviewdate'].getFullYear()}-${this.addForm.value['interviewdate'].getMonth() + 1}-${this.addForm.value['interviewdate'].getDate()} ${this.addForm.value['interviewdate'].getHours()}:${this.addForm.value['interviewdate'].getMinutes()}`;
      interview.interviewer = this.addForm.value['interviewer'];
      interview.place = this.addForm.value['place'];
      // interview.replydate = this.addForm.value['replydate'];
      // interview.checkdate = this.addForm.value['checkdate'];
      interview.validdate = this.addForm.value['validdate'];
      interview.user = this._storageService.userName;  
      
      console.info(interview);

      this._eipHrInterviewService.add(interview).subscribe(() => {
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
      date: this.searchForm.value["date"] == null ? "" : this.searchForm.value["date"],
      page: this.page,
      size: this.size
    }
    //console.log(query);
    this._eipHrInterviewService.GetInterviewList(query).subscribe(result => {
      this.data = result["data"];
      this.total = result["count"];
    });
  }
  sendMail(interview:hr){
    var query = {
      InterviewId:interview.interviewId,
      User:this._storageService.userName,
    }
    this._eipHrInterviewService.SendMail(query).subscribe(result => {   
      this._messageService.success("面試邀請寄送成功");
      this.refresh();
    });
  }
}

