import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';

import { Batch, BatchNumber } from '@models';

@Component({
    selector: 'app-batch-item',
    templateUrl: 'batch-item.component.html',
    styleUrls: ['batch-item.component.scss']
})

export class BatchItemComponent implements OnChanges {

    @Input()
    batch: Batch;
    
    progress: number = 0;

    constructor() { }

    ngOnChanges(changes: SimpleChanges): void {
        const batch = changes.batch.currentValue as Batch;
        this.progress = 0;

        const done = batch.numbers?.filter(n => n.product > 0);

        this.progress = done.length === 0 ? 0 : (done.length * 100) / batch.count;
    }

}