import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppConfigService {

  private appConfig: any;

  constructor(
    private http: HttpClient
  ) { }

  loadAppConfig(): Observable<any> {
    return this.http.get('assets/appConfig.json').pipe(
      map((data) => {
        this.appConfig = data;
        return data;
      })
    );
  }

  get apiBaseUrl() {
    if (!this.appConfig) {
      throw Error('Config file not loaded!');
    }
    return this.appConfig.apiBaseUrl;
  }
}
