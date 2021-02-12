import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, interval, Subscription } from 'rxjs';
import { flatMap, map, switchMap, tap } from 'rxjs/operators';

import { Batch, StatusEnum } from '@models';
import { ProcessService } from '@services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit, OnDestroy {

  private _onCreatedSubscription$: Subscription;
  private _refreshSubscription$: Subscription;

  hasRequest: boolean = false;
  isLoading: boolean = false;
  batches: Batch[] = [];

  constructor(private _service: ProcessService) {}

  ngOnInit(): void {
    this._onCreatedSubscription$ = this._service.onCreateRequest$.subscribe(() => {
      this.batches = [];
      this.startRefresh();
    });
  }

  ngOnDestroy(): void {
    this._onCreatedSubscription$?.unsubscribe();
    this._refreshSubscription$?.unsubscribe();
  }

  private startRefresh(): void {
    this.hasRequest = true;

    this._refreshSubscription$ = interval(2000).pipe(
      tap(() => this.isLoading = true),
      flatMap(() => this._service.getProcessState())
    )
    .subscribe((data: Batch[]) => {
      this.isLoading = false;
      this.batches = data;
      this.checkAllCompleted();
    });
  }

  private checkAllCompleted(): void {
    this.hasRequest = this.batches.some(batch => batch.status != StatusEnum.Done);
    if (!this.hasRequest)
      this._refreshSubscription$.unsubscribe();
  }

}
