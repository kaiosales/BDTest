import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { createRequest, Batch } from '@models';
import { Observable, Subject, Subscriber, Subscription } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable()
export class ProcessService {

  onCreateRequest$: Observable<createRequest>;
  private _onCreateRequestSubscriber: Subscriber<createRequest>;

  onClearState$: Observable<void>;
  private _onClearStateSubscriber: Subscriber<void>;

  constructor(private _http: HttpClient) {
    this.onCreateRequest$ = new Observable(subscriber => this._onCreateRequestSubscriber = subscriber);
    this.onClearState$ = new Observable(subscriber => this._onClearStateSubscriber = subscriber);
  }

  getProcessState() {
    return this._http.get<Array<Batch>>('/process');
  }

  createRequest(data: createRequest) {
    return this._http.post('/process', data).pipe(
      tap(() => this._onCreateRequestSubscriber?.next(data))
    );
  }

  clearProcessState() {
    this._onClearStateSubscriber?.next();
    return this._http.delete('/process');
  }
  
}