import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { concatMap, tap } from 'rxjs/operators';

import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatSnackBar } from '@angular/material/snack-bar';

import { createRequest } from '@models';
import { ProcessService } from '@services';

@Component({
    selector: 'app-request-form',
    templateUrl: 'request-form.component.html',
    styleUrls: ['request-form.component.scss']
})

export class RequestFormComponent implements OnInit {

    requested: boolean = false;

    requestForm: FormGroup;

    get batchCountControl() {
        return this.requestForm.get('batchCount');
    }

    get numberPerBatchControl() {
        return this.requestForm.get('numberPerBatch');
    }

    constructor(
        private _service: ProcessService, 
        private _bottomSheet: MatBottomSheet, 
        private _snackBar: MatSnackBar
    ) { }

    ngOnInit() { 
        this
            .initFormControls()
            .setFormValues();
    }

    private initFormControls(): RequestFormComponent {

        this.requestForm = new FormGroup({
            batchCount: new FormControl(),
            numberPerBatch: new FormControl(),
        });

        return this;
    }

    private setFormValues(): RequestFormComponent {
        this.requestForm.patchValue({
            batchCount: 1,
            numberPerBatch: 1
        });
    
        return this;
    }

    private getFormValues(): createRequest {
        const formModel = this.requestForm.getRawValue();

        return <createRequest>{
            batches: formModel.batchCount,
            numberCount: formModel.numberPerBatch
        };
    }

    public cancel(): void {
        this._bottomSheet.dismiss();
    }

    public createRequest(): void {
        this.requested = true;
        
        const request = this.getFormValues();
        this._service.clearProcessState().pipe(
            concatMap(() => this._service.createRequest(request))
        ).subscribe(() =>
        {
            this._bottomSheet.dismiss();
            this._snackBar.open(`${request.batches} batch(es) created with ${request.numberCount} number(s) each`, 'DISMISS', { duration: 2000 });
        });
    }
}