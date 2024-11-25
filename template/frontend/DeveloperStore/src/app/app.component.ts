import { AfterContentChecked, AfterContentInit, ChangeDetectorRef, Component } from '@angular/core';
import { HttpRequestsService } from './services/http-requests.service';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.scss'
})

export class AppComponent implements AfterContentChecked{

  loading : any;
  constructor(
    private httpRequestsService: HttpRequestsService,
    private cdRef: ChangeDetectorRef
  ) {
  }

  ngAfterContentChecked(): void {
    this.loading = this.httpRequestsService.loading;
    this.cdRef.detectChanges();
  } 
}
