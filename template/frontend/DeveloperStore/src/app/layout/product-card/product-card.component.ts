import { AfterViewInit, Component, inject, OnInit } from '@angular/core';
import { Product } from '../../model/product';
import { ProductsService } from '../../services/products.service';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-product-card',
  standalone: false,
  
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss'
})

export class ProductCardComponent implements OnInit, AfterViewInit {
  products: Product[] | any;
  orderService = inject(OrderService);

  constructor(
    private productsService: ProductsService
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productsService.get().subscribe((products: Product[]) => {
      this.products = products;
    });
  }

  addToCart(product: Product) {
    this.orderService.addProduct(product);
  }
}
