import { Component, OnInit,ViewEncapsulation,ViewChild,TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StorageService } from '../../../services/storage.service';
import { FileService } from '../../../business/file.service';
import { FileInfo } from '../../content-manage/model/fileInfo';

import { HrFormService } from '../../../business/eip/hr-form.service';
import { BuiltinTypeName } from '@angular/compiler';


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
  logonid:string;
  militaryDateS:any;
  militaryDateE:any;
  
  public GUID= this.route.snapshot.queryParamMap.get('GUID');

  constructor(private route: ActivatedRoute,
    private _eipHrFormService: HrFormService,
    private _router: Router,
    private _formBuilder: FormBuilder,
    private _storageService: StorageService, 
    private _fileService: FileService,) { }

  ngOnInit(): void { 
    this.logonid=this._storageService.userName;
    this.workForm = this._formBuilder.group({
      username_ch: [null,[Validators.required]],
      username_en: [null,[Validators.required]],
      birthday: [null,[Validators.required]],
      gender: [null,[Validators.required]],
      birthplace: [null,[Validators.required]],
      bloodtype: [null,[Validators.required]],
      identity: [null,[Validators.minLength(10)]],
      phone: [null],
      cellphone: [null],
      tellphone: [null],
      address_r: [null,[Validators.required]],
      address_m: [null,[Validators.required]],
      family_1: [null],
      family_2: [null],
      family_3: [null],
      family_4: [null],
      family_5: [null],
      family_6: [null],
      military: [null],
      military_reason: [null],
      military_category: [null],
      military_date: [null],
      marriage_ck_1: [null],
      marriage_ck_2: [null],
      marriage_ck_3: [null],
      marriage_ck_4: [null],
      marriage_ck_5: [null],
      marriage_ck_6: [null],
      marriage_date: [null],
      dg_name_1: [null],
      dg_name_2: [null],
      dg_name_3: [null],
      dg_name_4: [null],
      dg_name_5: [null],
      dg_major_1: [null],
      dg_major_2: [null],
      dg_major_3: [null],
      dg_major_4: [null],
      dg_major_5: [null],
      dg_degree_1: [null],
      dg_degree_2: [null],
      dg_degree_3: [null],
      dg_degree_4: [null],
      dg_degree_5: [null],
      dg_year_1: [null],
      dg_year_2: [null],
      dg_year_3: [null],
      dg_year_4: [null],
      dg_year_5: [null],
      dg_grad_1: [null],
      dg_grad_2: [null],
      dg_grad_3: [null],
      dg_grad_4: [null],
      dg_grad_5: [null],
      dg_gradyear_1: [null],
      dg_gradyear_2: [null],
      dg_gradyear_3: [null],
      dg_gradyear_4: [null],
      dg_gradyear_5: [null],
      dg_time_1: [null],
      dg_time_2: [null],
      dg_time_3: [null],
      dg_time_4: [null],
      dg_time_5: [null],
      exp_dept_1: [null],
      exp_dept_2: [null],
      exp_dept_3: [null],
      exp_dept_4: [null],
      exp_dept_5: [null],
      exp_period_1: [null],
      exp_period_2: [null],
      exp_period_3: [null],
      exp_period_4: [null],
      exp_period_5: [null],
      exp_income_m_1: [null],
      exp_income_m_2: [null],
      exp_income_m_3: [null],
      exp_income_m_4: [null],
      exp_income_m_5: [null],
      exp_income_y_1: [null],
      exp_income_y_2: [null],
      exp_income_y_3: [null],
      exp_income_y_4: [null],
      exp_income_y_5: [null],
      exp_position_1: [null],
      exp_position_2: [null],
      exp_position_3: [null],
      exp_position_4: [null],
      exp_position_5: [null],
      exp_boss_1: [null],
      exp_boss_2: [null],
      exp_boss_3: [null],
      exp_boss_4: [null],
      exp_boss_5: [null],
      exp_resign_1: [null],
      exp_resign_2: [null],
      exp_resign_3: [null],
      exp_resign_4: [null],
      exp_resign_5: [null],
      exp_reason_1: [null],
      exp_reason_2: [null],
      exp_reason_3: [null],
      exp_reason_4: [null],
      exp_reason_5: [null],
      fm_title_1: [null],
      fm_title_2: [null],
      fm_title_3: [null],
      fm_title_4: [null],
      fm_title_5: [null],
      fm_title_6: [null],
      fm_name_1: [null],
      fm_name_2: [null],
      fm_name_3: [null],
      fm_name_4: [null],
      fm_name_5: [null],
      fm_name_6: [null],
      fm_age_1: [null],
      fm_age_2: [null],
      fm_age_3: [null],
      fm_age_4: [null],
      fm_age_5: [null],
      fm_age_6: [null],
      fm_education_1: [null],
      fm_education_2: [null],
      fm_education_3: [null],
      fm_education_4: [null],
      fm_education_5: [null],
      fm_education_6: [null],
      fm_job_1: [null],
      fm_job_2: [null],
      fm_job_3: [null],
      fm_job_4: [null],
      fm_job_5: [null],
      fm_job_6: [null],
      sf_q1_1: [null,[Validators.required]],
      sf_q2_1: [null,[Validators.required]],
      sf_q2_2: [null,[Validators.required]],
      sf_q3_1: [null,[Validators.required]],
      sf_q3_2: [null,[Validators.required]],
      sf_q3_3: [null,[Validators.required]],
      sf_q4_ck_1: [null],
      sf_q4_ck_2: [null],
      sf_q4_ck_3: [null],
      sf_q4_3: [null],
      sf_q5_1: [null,[Validators.required]],
      sf_q5_2: [null,[Validators.required]],
      sf_q5_3: [null,[Validators.required]],
      sf_q5_4: [null],
      sf_q6_1: [null],
      sf_q6_2: [null],
      sf_q7_1: [null],
      sf_q7_2: [null],
      sf_q7_3: [null],
      sf_q7_4: [null],
      sf_q8_ck_1: [null],
      sf_q8_ck_2: [null],
      sf_q8_ck_3: [null],
      sf_q8_3: [null],
      sf_q8_ck_4: [null],
      sf_q8_4: [null],
      sf_q8_ck_5: [null],
      sf_q9_ck_1: [null],
      sf_q9_ck_2: [null],
      sf_q9_2: [null],
      sf_q9_3: [null],
      sf_q9_ck_4: [null],
      sf_q9_ck_5: [null],
      wm_name_1: [null],
      wm_name_2: [null],
      wm_dept_1: [null],
      wm_dept_2: [null],
      wm_title_1: [null],
      wm_title_2: [null],
      wm_address_1: [null],
      wm_address_2: [null],
      wm_phone_1: [null],
      wm_phone_2: [null],
    }); 
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
        // if (this.logonid!=null)
        // {
          this.getData();
        //}
      }
    });
  }
  work(){
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
    this._eipHrFormService.GetWorkData(query).subscribe(result => {
      if (result["candidateId"]!=0)
      {
        this.workForm = this._formBuilder.group({
          username_ch: [result['usernameCh']],
          username_en: [result['usernameEn']],
          birthday: [result['birthday']],
          gender: [result['gender']],
          birthplace: [result['birthplace']],
          identity: [result['identity']],
          phone: [result['phone']],
          cellphone: [result['cellphone']],
          tellphone: [result['tellphone']],
          bloodtype: [result['bloodtype']],
          address_r: [result['addressR']],
          address_m: [result['addressM']],
          family_1: [result['family1']],
          family_2: [result['family2']],
          family_3: [result['family3']],
          family_4: [result['family4']],
          family_5: [result['family5']],
          family_6: [result['family6']],
          military: [result['military']],
          military_reason: [result['militaryReason']],
          military_category: [result['militaryCategory']],
          military_date: [result['militaryDateS'],result['militaryDateE']],   
          marriage_ck_1: [result['marriageCk1']],
          marriage_ck_2: [result['marriageCk2']],
          marriage_ck_3: [result['marriageCk3']],
          marriage_ck_4: [result['marriageCk4']],
          marriage_ck_5: [result['marriageCk5']],
          marriage_ck_6: [result['marriageCk6']],
          marriage_date: [result['marriageDate']],
          dg_name_1: [result['dgName1']],
          dg_name_2: [result['dgName2']],
          dg_name_3: [result['dgName3']],
          dg_name_4: [result['dgName4']],
          dg_name_5: [result['dgName5']],
          dg_major_1: [result['dgMajor1']],
          dg_major_2: [result['dgMajor2']],
          dg_major_3: [result['dgMajor3']],
          dg_major_4: [result['dgMajor4']],
          dg_major_5: [result['dgMajor5']],
          dg_degree_1: [result['dgDegree1']],
          dg_degree_2: [result['dgDegree2']],
          dg_degree_3: [result['dgDegree3']],
          dg_degree_4: [result['dgDegree4']],
          dg_degree_5: [result['dgDegree5']],
          dg_year_1: [result['dgYear1']],
          dg_year_2: [result['dgYear2']],
          dg_year_3: [result['dgYear3']],
          dg_year_4: [result['dgYear4']],
          dg_year_5: [result['dgYear5']],
          dg_grad_1: [result['dgGrad1']],
          dg_grad_2: [result['dgGrad2']],
          dg_grad_3: [result['dgGrad3']],
          dg_grad_4: [result['dgGrad4']],
          dg_grad_5: [result['dgGrad5']],
          dg_gradyear_1: [result['dgGradyear1']],
          dg_gradyear_2: [result['dgGradyear2']],
          dg_gradyear_3: [result['dgGradyear3']],
          dg_gradyear_4: [result['dgGradyear4']],
          dg_gradyear_5: [result['dgGradyear5']],
          dg_time_1: [result['dgTime1']],
          dg_time_2: [result['dgTime2']],
          dg_time_3: [result['dgTime3']],
          dg_time_4: [result['dgTime4']],
          dg_time_5: [result['dgTime5']],
          exp_dept_1: [result['expDept1']],
          exp_dept_2: [result['expDept2']],
          exp_dept_3: [result['expDept3']],
          exp_dept_4: [result['expDept4']],
          exp_dept_5: [result['expDept5']],
          exp_period_1: [result['expPeriod1']],
          exp_period_2: [result['expPeriod2']],
          exp_period_3: [result['expPeriod3']],
          exp_period_4: [result['expPeriod4']],
          exp_period_5: [result['expPeriod5']],
          exp_income_m_1: [result['expIncomeM1']],
          exp_income_m_2: [result['expIncomeM2']],
          exp_income_m_3: [result['expIncomeM3']],
          exp_income_m_4: [result['expIncomeM4']],
          exp_income_m_5: [result['expIncomeM5']],
          exp_income_y_1: [result['expIncomeY1']],
          exp_income_y_2: [result['expIncomeY2']],
          exp_income_y_3: [result['expIncomeY3']],
          exp_income_y_4: [result['expIncomeY4']],
          exp_income_y_5: [result['expIncomeY5']],
          exp_position_1: [result['expPosition1']],
          exp_position_2: [result['expPosition2']],
          exp_position_3: [result['expPosition3']],
          exp_position_4: [result['expPosition4']],
          exp_position_5: [result['expPosition5']],
          exp_boss_1: [result['expBoss1']],
          exp_boss_2: [result['expBoss2']],
          exp_boss_3: [result['expBoss3']],
          exp_boss_4: [result['expBoss4']],
          exp_boss_5: [result['expBoss5']],
          exp_resign_1: [result['expResign1']],
          exp_resign_2: [result['expResign2']],
          exp_resign_3: [result['expResign3']],
          exp_resign_4: [result['expResign4']],
          exp_resign_5: [result['expResign5']],
          exp_reason_1: [result['expReason1']],
          exp_reason_2: [result['expReason2']],
          exp_reason_3: [result['expReason3']],
          exp_reason_4: [result['expReason4']],
          exp_reason_5: [result['expReason5']],
          fm_title_1: [result['fmTitle1']],
          fm_title_2: [result['fmTitle2']],
          fm_title_3: [result['fmTitle3']],
          fm_title_4: [result['fmTitle4']],
          fm_title_5: [result['fmTitle5']],
          fm_title_6: [result['fmTitle6']],
          fm_name_1: [result['fmName1']],
          fm_name_2: [result['fmName2']],
          fm_name_3: [result['fmName3']],
          fm_name_4: [result['fmName4']],
          fm_name_5: [result['fmName5']],
          fm_name_6: [result['fmName6']],
          fm_age_1: [result['fmAge1']],
          fm_age_2: [result['fmAge2']],
          fm_age_3: [result['fmAge3']],
          fm_age_4: [result['fmAge4']],
          fm_age_5: [result['fmAge5']],
          fm_age_6: [result['fmAge6']],
          fm_education_1: [result['fmEducation1']],
          fm_education_2: [result['fmEducation2']],
          fm_education_3: [result['fmEducation3']],
          fm_education_4: [result['fmEducation4']],
          fm_education_5: [result['fmEducation5']],
          fm_education_6: [result['fmEducation6']],        
          fm_job_1: [result['fmJob1']],
          fm_job_2: [result['fmJob2']],
          fm_job_3: [result['fmJob3']],
          fm_job_4: [result['fmJob4']],
          fm_job_5: [result['fmJob5']],
          fm_job_6: [result['fmJob6']],
          sf_q1_1: [result['sfQ11']],
          sf_q2_1: [result['sfQ21']],
          sf_q2_2: [result['sfQ22']],
          sf_q3_1: [result['sfQ31']],
          sf_q3_2: [result['sfQ32']],
          sf_q3_3: [result['sfQ33']],
          sf_q4_ck_1: [result['sfQ4Ck1']],
          sf_q4_ck_2: [result['sfQ4Ck2']],
          sf_q4_ck_3: [result['sfQ4Ck3']],
          sf_q4_3: [result['sfQ43']],
          sf_q5_1: [result['sfQ51']],
          sf_q5_2: [result['sfQ52']],
          sf_q5_3: [result['sfQ53']],
          sf_q5_4: [result['sfQ54']],
          sf_q6_1: [result['sfQ61']],
          sf_q6_2: [result['sfQ62']],
          sf_q7_1: [result['sfQ71']],
          sf_q7_2: [result['sfQ72']],
          sf_q7_3: [result['sfQ73']],
          sf_q7_4: [result['sfQ74']],
          sf_q8_ck_1: [result['sfQ8Ck1']],
          sf_q8_ck_2: [result['sfQ8Ck2']],
          sf_q8_ck_3: [result['sfQ8Ck3']],
          sf_q8_3: [result['sfQ83']],
          sf_q8_ck_4: [result['sfQ8Ck4']],
          sf_q8_4: [result['sfQ84']],
          sf_q8_ck_5: [result['sfQ8Ck5']],
          sf_q9_ck_1: [result['sfQ9Ck1']],
          sf_q9_ck_2: [result['sfQ9Ck2']],
          sf_q9_2: [result['sfQ92']],
          sf_q9_3: [result['sfQ93']],
          sf_q9_ck_4: [result['sfQ9Ck4']],
          sf_q9_ck_5: [result['sfQ9Ck5']],
          wm_name_1: [result['wmName1']],
          wm_name_2: [result['wmName2']],
          wm_dept_1: [result['wmDept1']],
          wm_dept_2: [result['wmDept2']],
          wm_title_1: [result['wmTitle1']],
          wm_title_2: [result['wmTitle2']],
          wm_address_1: [result['wmAddress1']],
          wm_address_2: [result['wmAddress2']],
          wm_phone_1: [result['wmPhone1']],
          wm_phone_2: [result['wmPhone2']],
        });
      }
      console.log(result);
    });
  }
  send(){
    this.checkdata();
    if(this.workForm.valid)
    {
      if(this.workForm.value['military_date']!=null)
      {
        this.militaryDateS=this.workForm.value['military_date'][0];
        this.militaryDateE=this.workForm.value['military_date'][1];
      }
      else
      {
        this.militaryDateS=null;
        this.militaryDateE=null;
      }      
      var work ={
        GUID : this.route.snapshot.queryParamMap.get('GUID'),
        usernameCh: this.workForm.value['username_ch'],
        usernameEn: this.workForm.value['username_en'],
        birthday: this.workForm.value['birthday'],
        gender: this.workForm.value['gender'],
        birthplace: this.workForm.value['birthplace'],
        identity: this.workForm.value['identity'],
        phone: this.workForm.value['phone'],
        cellphone: this.workForm.value['cellphone'],
        tellphone: this.workForm.value['tellphone'],
        bloodtype: this.workForm.value['bloodtype'],
        addressR: this.workForm.value['address_r'],
        addressM: this.workForm.value['address_m'],
        family1: this.workForm.value['family_1'],
        family2: this.workForm.value['family_2'],
        family3: this.workForm.value['family_3'],
        family4: this.workForm.value['family_4'],
        family5: this.workForm.value['family_5'],
        family6: this.workForm.value['family_6'],
        military: this.workForm.value['military'],
        militaryReason: this.workForm.value['military_reason'],
        militaryCategory: this.workForm.value['military_category'],
        militaryDateS: this.militaryDateS,
        militaryDateE: this.militaryDateE,
        marriageCk1: this.workForm.value['marriage_ck_1'],
        marriageCk2: this.workForm.value['marriage_ck_2'],
        marriageCk3: this.workForm.value['marriage_ck_3'],
        marriageCk4: this.workForm.value['marriage_ck_4'],
        marriageCk5: this.workForm.value['marriage_ck_5'],
        marriageCk6: this.workForm.value['marriage_ck_6'],
        marriageDate: this.workForm.value['marriage_date'],
        dgName1: this.workForm.value['dg_name_1'],
        dgName2: this.workForm.value['dg_name_2'],
        dgName3: this.workForm.value['dg_name_3'],
        dgName4: this.workForm.value['dg_name_4'],
        dgName5: this.workForm.value['dg_name_5'],
        dgMajor1: this.workForm.value['dg_major_1'],
        dgMajor2: this.workForm.value['dg_major_2'],
        dgMajor3: this.workForm.value['dg_major_3'],
        dgMajor4: this.workForm.value['dg_major_4'],
        dgMajor5: this.workForm.value['dg_major_5'],
        dgDegree1: this.workForm.value['dg_degree_1'],
        dgDegree2: this.workForm.value['dg_degree_2'],
        dgDegree3: this.workForm.value['dg_degree_3'],
        dgDegree4: this.workForm.value['dg_degree_4'],
        dgDegree5: this.workForm.value['dg_degree_5'],
        dgYear1: this.workForm.value['dg_year_1'],
        dgYear2: this.workForm.value['dg_year_2'],
        dgYear3: this.workForm.value['dg_year_3'],
        dgYear4: this.workForm.value['dg_year_4'],
        dgYear5: this.workForm.value['dg_year_5'],
        dgGrad1: this.workForm.value['dg_grad_1'],
        dgGrad2: this.workForm.value['dg_grad_2'],
        dgGrad3: this.workForm.value['dg_grad_3'],
        dgGrad4: this.workForm.value['dg_grad_4'],
        dgGrad5: this.workForm.value['dg_grad_5'],
        dgGradyear1: this.workForm.value['dg_gradyear_1'],
        dgGradyear2: this.workForm.value['dg_gradyear_2'],
        dgGradyear3: this.workForm.value['dg_gradyear_3'],
        dgGradyear4: this.workForm.value['dg_gradyear_4'],
        dgGradyear5: this.workForm.value['dg_gradyear_5'],
        dgTime1: this.workForm.value['dg_time_1'],
        dgTime2: this.workForm.value['dg_time_2'],
        dgTime3: this.workForm.value['dg_time_3'],
        dgTime4: this.workForm.value['dg_time_4'],
        dgTime5: this.workForm.value['dg_time_5'],
        expDept1: this.workForm.value['exp_dept_1'],
        expDept2: this.workForm.value['exp_dept_2'],
        expDept3: this.workForm.value['exp_dept_3'],
        expDept4: this.workForm.value['exp_dept_4'],
        expDept5: this.workForm.value['exp_dept_5'],
        expPeriod1: this.workForm.value['exp_period_1'],
        expPeriod2: this.workForm.value['exp_period_2'],
        expPeriod3: this.workForm.value['exp_period_3'],
        expPeriod4: this.workForm.value['exp_period_4'],
        expPeriod5: this.workForm.value['exp_period_5'],
        expIncomeM1: this.workForm.value['exp_income_m_1'],
        expIncomeM2: this.workForm.value['exp_income_m_2'],
        expIncomeM3: this.workForm.value['exp_income_m_3'],
        expIncomeM4: this.workForm.value['exp_income_m_4'],
        expIncomeM5: this.workForm.value['exp_income_m_5'],
        expIncomeY1: this.workForm.value['exp_income_y_1'],
        expIncomeY2: this.workForm.value['exp_income_y_2'],
        expIncomeY3: this.workForm.value['exp_income_y_3'],
        expIncomeY4: this.workForm.value['exp_income_y_4'],
        expIncomeY5: this.workForm.value['exp_income_y_5'],
        expPosition1: this.workForm.value['exp_position_1'],
        expPosition2: this.workForm.value['exp_position_2'],
        expPosition3: this.workForm.value['exp_position_3'],
        expPosition4: this.workForm.value['exp_position_4'],
        expPosition5: this.workForm.value['exp_position_5'],
        expBoss1: this.workForm.value['exp_boss_1'],
        expBoss2: this.workForm.value['exp_boss_2'],
        expBoss3: this.workForm.value['exp_boss_3'],
        expBoss4: this.workForm.value['exp_boss_4'],
        expBoss5: this.workForm.value['exp_boss_5'],
        expResign1: this.workForm.value['exp_resign_1'],
        expResign2: this.workForm.value['exp_resign_2'],
        expResign3: this.workForm.value['exp_resign_3'],
        expResign4: this.workForm.value['exp_resign_4'],
        expResign5: this.workForm.value['exp_resign_5'],
        expReason1: this.workForm.value['exp_reason_1'],
        expReason2: this.workForm.value['exp_reason_2'],
        expReason3: this.workForm.value['exp_reason_3'],
        expReason4: this.workForm.value['exp_reason_4'],
        expReason5: this.workForm.value['exp_reason_5'],
        fmTitle1: this.workForm.value['fm_title_1'],
        fmTitle2: this.workForm.value['fm_title_2'],
        fmTitle3: this.workForm.value['fm_title_3'],
        fmTitle4: this.workForm.value['fm_title_4'],
        fmTitle5: this.workForm.value['fm_title_5'],
        fmTitle6: this.workForm.value['fm_title_6'],
        fmName1: this.workForm.value['fm_name_1'],
        fmName2: this.workForm.value['fm_name_2'],
        fmName3: this.workForm.value['fm_name_3'],
        fmName4: this.workForm.value['fm_name_4'],
        fmName5: this.workForm.value['fm_name_5'],
        fmName6: this.workForm.value['fm_name_6'],
        fmAge1: this.workForm.value['fm_age_1'],
        fmAge2: this.workForm.value['fm_age_2'],
        fmAge3: this.workForm.value['fm_age_3'],
        fmAge4: this.workForm.value['fm_age_4'],
        fmAge5: this.workForm.value['fm_age_5'],
        fmAge6: this.workForm.value['fm_age_6'],
        fmEducation1: this.workForm.value['fm_education_1'],
        fmEducation2: this.workForm.value['fm_education_2'],
        fmEducation3: this.workForm.value['fm_education_3'],
        fmEducation4: this.workForm.value['fm_education_4'],
        fmEducation5: this.workForm.value['fm_education_5'],
        fmEducation6: this.workForm.value['fm_education_6'],
        fmJob1: this.workForm.value['fm_job_1'],
        fmJob2: this.workForm.value['fm_job_2'],
        fmJob3: this.workForm.value['fm_job_3'],
        fmJob4: this.workForm.value['fm_job_4'],
        fmJob5: this.workForm.value['fm_job_5'],
        fmJob6: this.workForm.value['fm_job_6'],
        sfQ11: this.workForm.value['sf_q1_1'],
        sfQ21: this.workForm.value['sf_q2_1'],
        sfQ22: this.workForm.value['sf_q2_2'],
        sfQ31: this.workForm.value['sf_q3_1'],
        sfQ32: this.workForm.value['sf_q3_2'],
        sfQ33: this.workForm.value['sf_q3_3'],
        sfQ4Ck1: this.workForm.value['sf_q4_ck_1'],
        sfQ4Ck2: this.workForm.value['sf_q4_ck_2'],
        sfQ4Ck3: this.workForm.value['sf_q4_ck_3'],
        sfQ43: this.workForm.value['sf_q4_3'],
        sfQ51: this.workForm.value['sf_q5_1'],
        sfQ52: this.workForm.value['sf_q5_2'],
        sfQ53: this.workForm.value['sf_q5_3'],
        sfQ54: this.workForm.value['sf_q5_4'],
        sfQ61: this.workForm.value['sf_q6_1'],
        sfQ62: this.workForm.value['sf_q6_2'],
        sfQ71: this.workForm.value['sf_q7_1'],
        sfQ72: this.workForm.value['sf_q7_2'],
        sfQ73: this.workForm.value['sf_q7_3'],
        sfQ74: this.workForm.value['sf_q7_4'],
        sfQ8Ck1: this.workForm.value['sf_q8_ck_1'],
        sfQ8Ck2: this.workForm.value['sf_q8_ck_2'],
        sfQ8Ck3: this.workForm.value['sf_q8_ck_3'],
        sfQ83: this.workForm.value['sf_q8_3'],
        sfQ8Ck4: this.workForm.value['sf_q8_ck_4'],
        sfQ84: this.workForm.value['sf_q8_4'],
        sfQ8Ck5: this.workForm.value['sf_q8_ck_5'],
        sfQ9Ck1: this.workForm.value['sf_q9_ck_1'],
        sfQ9Ck2: this.workForm.value['sf_q9_ck_2'],
        sfQ92: this.workForm.value['sf_q9_2'],
        sfQ93: this.workForm.value['sf_q9_3'],
        sfQ9Ck4: this.workForm.value['sf_q9_ck_4'],
        sfQ9Ck5: this.workForm.value['sf_q9_ck_5'],
        wmName1: this.workForm.value['wm_name_1'],
        wmName2: this.workForm.value['wm_name_2'],
        wmDept1: this.workForm.value['wm_dept_1'],
        wmDept2: this.workForm.value['wm_dept_2'],
        wmTitle1: this.workForm.value['wm_title_1'],
        wmTitle2: this.workForm.value['wm_title_2'],
        wmAddress1: this.workForm.value['wm_address_1'],
        wmAddress2: this.workForm.value['wm_address_2'],
        wmPhone1: this.workForm.value['wm_phone_1'],
        wmPhone2: this.workForm.value['wm_phone_2'],
      }

      this._eipHrFormService.SendWork(work).subscribe(result => {
        alert("儲存成功");
        // window.close();
      });
    }
    else
    {
      for (const i in this.workForm.controls) {
        this.workForm.controls[i].markAsDirty();
        this.workForm.controls[i].updateValueAndValidity();
      }
    }
  }
  print(){
    var query = {
      Guid: this.route.snapshot.queryParamMap.get('GUID'),
    }
    this._eipHrFormService.PrintWork(query).subscribe(result => {
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
  checkdata(){
    if (this.workForm.value['military']==null)
    {
      alert("兵役未填");
      return;    
    }
    if (this.workForm.value['sf_q1_1']==null)
    {
      alert("自我評述Q1未填");
      return;    
    }
    if (this.workForm.value['sf_q2_1']==null || this.workForm.value['sf_q2_2']==null)
    {
      alert("自我評述Q2未填");
      return;    
    }
    if (this.workForm.value['sf_q3_1']==null || this.workForm.value['sf_q3_2']==null || this.workForm.value['sf_q3_3']==null)
    {
      alert("自我評述Q3未填");
      return;    
    }
    if (this.workForm.value['sf_q5_1']==null || this.workForm.value['sf_q5_2']==null || this.workForm.value['sf_q5_3']==null)
    {
      alert("自我評述Q5未填");
      return;    
    }

  }

}
