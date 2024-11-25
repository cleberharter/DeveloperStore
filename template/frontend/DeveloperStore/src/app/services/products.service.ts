import { HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../model/product';
import { HttpRequestsService } from './http-requests.service';
import { Observable, map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(
    private httpRequestsService: HttpRequestsService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  get(): Observable<Product[]> {
    return this.httpRequestsService.getJson('/assets/products.json').pipe(
      map((data: Product[] | any) => {
        if (data.errorMessage) {
          this.toastr.error(`${data.errorMessage}`, 'Error');
          return [];
        } else {
          return data.filter((product: { quantity: number; }) => product.quantity > 0);
        }
      })
    );
  }

  getById(id: number) {
    const params = new HttpParams()
    .set('id', id);
    return this.httpRequestsService.get("/products", params).pipe(
      map((data: Product[] | any) => {
        if (data.errorMessage) {
          this.toastr.error(`${data.errorMessage}`, 'Error');
        } else {
          return data[0];
        }
      })
    );
  }

  delete(data: Product) {
    const params = new HttpParams()
    .set('id', data.id);
    this.httpRequestsService.delete('/products', params).subscribe({
      next: (data: any) => {
        if (data.errorMessage) {
          this.toastr.error(`${data.errorMessage}`, 'Error');
        } else {
          this.toastr.success(`${data.message}`, 'Success');
          this.router.navigate(['products']);
        }
      }
    });
  }
}
