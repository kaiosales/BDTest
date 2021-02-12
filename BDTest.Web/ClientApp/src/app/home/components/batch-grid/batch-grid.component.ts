import { trigger, transition, style, animate, group, query, stagger } from '@angular/animations';
import { AfterViewInit, ChangeDetectorRef, Component, Input, OnInit } from '@angular/core';
import { Batch, BatchNumber } from '@models';

@Component({
    selector: 'app-batch-grid',
    templateUrl: 'batch-grid.component.html',
    styleUrls: [
        'batch-grid.component.scss',
        'batch-grid.component.adaptative.scss'
    ],
    animations: [
        trigger('fadeIn', [
            transition('* => loaded', [
              query('app-batch-item:enter', [
                    style({ opacity: '0', transform: 'scale(0.8, 0.8)' }),
                    stagger(100, [
                        group([
                            animate('0.3s 0.2s ease-out', style({ opacity: '1' })),
                            animate('.5s cubic-bezier(.8, -.5, .2, 1.4)', style({ transform: 'scale(1, 1)' }))
                        ])
                    ])
                ], { optional: true })
            ]),
        ]),
    ]
})

export class BatchGridComponent {

    @Input()
    batches: Batch[] = [];

    constructor() { }

    trackById(index: number, batch: Batch) {
        return batch?.id;
    }

    getAnimation() {
        return this.batches.length > 0 ? 'loaded' : '';
    }
}