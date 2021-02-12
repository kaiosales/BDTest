import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '@shared/shared.module';

import { 
    AddRequestComponent, 
    RequestFormComponent, 
    BatchGridComponent, 
    BatchItemComponent 
} from './components';

import { HomeComponent } from './home.component';

@NgModule({
    imports: [
        SharedModule,
        FormsModule,
        ReactiveFormsModule
    ],
    declarations: [
        HomeComponent,
        AddRequestComponent,
        RequestFormComponent,
        BatchGridComponent,
        BatchItemComponent
    ]
})
export class HomeModule { }
