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
import { MatSelectModule} from '@angular/material/select';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';

import { HrUserComponent } from './hr-user/hr-user.component';
import { HrInterviewComponent } from './hr-interview/hr-interview.component';
import { HrDataComponent } from './hr-data/hr-data.component';



@NgModule({
  declarations: [
    HrUserComponent,
    HrInterviewComponent,
    HrDataComponent,
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
    MatSelectModule,
    NzSelectModule,
    NzToolTipModule,
    RouterModule.forChild([
      { path: "hr-user", component: HrUserComponent, canActivate: [LoginGuard] },
      { path: "hr-interview", component: HrInterviewComponent, canActivate: [LoginGuard] },
      { path: "hr-data", component: HrDataComponent, canActivate: [LoginGuard] },
    ]),
  ]
})
export class EipModule { }
