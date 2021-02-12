import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaterialModule } from './material.module';

import { LoadingComponent } from './components';

@NgModule({
    imports: [
        CommonModule,
        MaterialModule
    ],
    exports: [
        CommonModule,
        MaterialModule,
        LoadingComponent
    ],
    declarations: [
        LoadingComponent
    ]
})
export class SharedModule { }
