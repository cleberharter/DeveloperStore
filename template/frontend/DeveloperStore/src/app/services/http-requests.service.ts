import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { AppConfigService } from './app-config.service';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestsService {

  loading = new BehaviorSubject<boolean>(false);

  constructor(
    private http: HttpClient,
    private appConfigService: AppConfigService
  ) { }


  private createObservable<T>(observableFn: () => Observable<T>): Observable<T> {
    this.loading.next(true);
    return observableFn().pipe(
      tap((response) => this.loading.next(false)),
      catchError((error) => {
        this.loading.next(false);
        return throwError(() => error);
      })
    );
  }

  getJson<T>(path: string): Observable<T> {
    return this.createObservable<T>(() => {
      return this.http.get<T>(`${path}`);
    });
  }

  get<T>(url: string, queryParams?: HttpParams): Observable<T> {
    return this.createObservable<T>(() => {
      return this.http.get<T>(`${this.appConfigService.apiBaseUrl}${url}`, { params: queryParams || {} });
    });
  }

  post<T>(url: string, body?: any): Observable<T> {
    return this.createObservable<T>(() => {
      return this.http.post<T>(`${this.appConfigService.apiBaseUrl}${url}`, body);
    });
  }

  delete<T>(url: string, queryParams?: HttpParams): Observable<T> {
    return this.createObservable<T>(() => {
      return this.http.delete<T>(`${this.appConfigService.apiBaseUrl}${url}`, { params: queryParams || {} });
    });
  }
}
