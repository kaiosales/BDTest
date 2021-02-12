import { Component, Input } from '@angular/core';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { RequestFormComponent } from '../request-form/request-form.component';

@Component({
    selector: 'app-add-request',
    templateUrl: 'add-request.component.html',
    styleUrls: ['add-request.component.scss']
})
export class AddRequestComponent {

    @Input()
    enabled: boolean = true;

    constructor(private _bottomSheet: MatBottomSheet) { }

    openBottomSheet(): void {
        this._bottomSheet.open(RequestFormComponent);
    }
}