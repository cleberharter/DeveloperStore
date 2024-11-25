import { Injectable, computed, signal } from '@angular/core';
import { Product } from '../model/product';
import { HttpRequestsService } from './http-requests.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import CartItem from '../model/cartItem';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(
    private httpRequestsService: HttpRequestsService,
    private toastr: ToastrService,
    private router: Router
  ) { }

  cartItems = signal<CartItem[]>([]);

  cartItemsTotal = computed(() => this.cartItems().reduce((acc, curr) => acc + curr.quantity, 0));

  subTotal = computed(() => this.cartItems().reduce((acc, curr) => {
    return acc + (curr.product.price * curr.quantity);
  }, 0));

  addProduct(product: Product): void {
    this.toastr.success(`Product added to shopping cart! `, 'Success');

    const index = this.cartItems().findIndex(item =>
      item.product.name === product.name);

    if (index === -1) {
      this.cartItems.update(items => [...items, { product, quantity: 1 }]);
    } else {
      this.cartItems.update(items =>
        [
          ...items.slice(0, index),
          { ...items[index], quantity: items[index].quantity + 1 },
          ...items.slice(index + 1)
        ]);
    }
  }

  orderCreate(): void {
    const data = {
      userId: "18df26ad-e70e-4162-8be4-d346e47afc7a",
      branch: "Iguatemi Store",
      date: new Date(),
      products: this.cartItems().map(item => { return { productId: item.product.id, quantity: item.quantity }})
    }
            
    this.httpRequestsService.post('/orders', data).subscribe({
      next:(data: any) => {
        if(data.errorMessage) {
          this.toastr.error(`${data.errorMessage}`, 'Error');
        } else {
          this.cartItems.set([])
          this.toastr.success(`${data.message}`, 'Success');
          this.router.navigateByUrl('/');
        }
      }
    });
  }

  removeFromCart(cartItem: CartItem): void {
    this.cartItems.update(items => items.filter(item =>
      item.product.name !== cartItem.product.name));
  }

  updateInCart(cartItem: CartItem, quantity: number): void {
    this.cartItems.update(items =>
      items.map(item => item.product.id === cartItem.product.id ?
        { product: cartItem.product, quantity } : item));
  }

}
