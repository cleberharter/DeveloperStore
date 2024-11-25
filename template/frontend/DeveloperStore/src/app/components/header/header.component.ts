import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { OrderService } from '../../services/order.service';

@Component({
  selector: 'app-header',
  standalone: false,
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit{
  orderService = inject(OrderService);

  constructor(
    private router: Router,
  ) {
    
  }

  ngOnInit(): void {
  }

  pedidos(): void {
    this.router.navigateByUrl('/orders');
  }
}
