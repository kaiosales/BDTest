import { NgModule } from '@angular/core';
import { MatCommonModule } from '@angular/material/core';

import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSliderModule } from '@angular/material/slider';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
    declarations: [],
    imports: [
        MatCommonModule,
        MatGridListModule,
        MatCardModule,
        MatMenuModule,
        MatIconModule,
        MatButtonModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatBottomSheetModule,
        MatInputModule,
        MatFormFieldModule,
        MatExpansionModule,
        MatSliderModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatSnackBarModule
    ],
    providers: [],
    bootstrap: [],
    exports:[
        MatCommonModule,
        MatGridListModule,
        MatCardModule,
        MatMenuModule,
        MatIconModule,
        MatButtonModule,
        MatToolbarModule,
        MatSidenavModule,
        MatListModule,
        MatBottomSheetModule,
        MatInputModule,
        MatFormFieldModule,
        MatExpansionModule,
        MatSliderModule,
        MatProgressBarModule,
        MatProgressSpinnerModule,
        MatSnackBarModule
    ]
})
export class MaterialModule { }
