import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { ApiPrefixInterceptor } from './interceptors';
import { NavComponent } from './components';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ProcessService } from '@services';

@NgModule({
    imports: [
        SharedModule,
        HttpClientModule
    ],
    declarations: [
        NavComponent
    ],
    exports: [
        NavComponent
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ApiPrefixInterceptor,
            multi: true
        },
        ProcessService
    ]
})
export class CoreModule { }