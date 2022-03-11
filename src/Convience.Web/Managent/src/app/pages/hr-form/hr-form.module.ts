import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LoginGuard } from 'src/app/guards/login.guard';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { AppCommonModule } from '../app-common/app-common.module';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';

import { HrFormConsentComponent } from './hr-form-consent/hr-form-consent.component';
import { HrFormImformationComponent } from './hr-form-imformation/hr-form-imformation.component';
import { HrFormWorkComponent } from './hr-form-work/hr-form-work.component';
import { HrFormErrorComponent } from './hr-form-error/hr-form-error.component';



@NgModule({
  declarations: [
    HrFormConsentComponent,
    HrFormImformationComponent,
    HrFormWorkComponent,
    HrFormErrorComponent,
  ],
  imports: [
    CommonModule,
    NzPaginationModule,
    NzFormModule,
    NzCardModule,
    NzTableModule,
    NzInputModule,
    AppCommonModule,
    NzButtonModule,
    NzIconModule,
    NzDatePickerModule,
    NzTimePickerModule,
    NzCheckboxModule,
    NzSelectModule,
    NzToolTipModule,
    RouterModule.forChild([
      { path: "hr-form-consent", component: HrFormConsentComponent},
      { path: "hr-form-imformation", component: HrFormImformationComponent},
      { path: "hr-form-work", component: HrFormWorkComponent},
      { path: "hr-form-error", component: HrFormErrorComponent},
    ]),
  ]
})
export class HrFormModule { }
