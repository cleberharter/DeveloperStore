import { Component, inject } from '@angular/core';
import { OrderService } from '../../services/order.service';
import CartItem from '../../model/cartItem';

@Component({
  selector: 'app-cart',
  standalone: false,
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartComponent {
  orderService = inject(OrderService);
  cartItems = this.orderService.cartItems;

  remove(product: CartItem) {
    this.orderService.removeFromCart(product)
  }

  onQuantitySelected(product: CartItem, quantity: number, inc: boolean): void {
    let q = inc ? Number(quantity) + 1 : Number(quantity) - 1;
    this.orderService.updateInCart(product, Number(q));
  }

  finalizePurchase(): void {
    this.orderService.orderCreate();
  }
}
