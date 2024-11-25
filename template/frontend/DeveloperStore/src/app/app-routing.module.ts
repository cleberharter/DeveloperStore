import { NgModule } from '@angular/core';
import { RouterModule, RouterLink, Routes } from '@angular/router';
import { ProductCardComponent } from './layout/product-card/product-card.component';
import { CartComponent } from './layout/cart/cart.component';

const routes: Routes = [
  { path: 'cart', title: 'Shopping Cart', component: CartComponent },
  { path: 'products', title: 'Products List', component: ProductCardComponent, data: { secured: false} },
  { path: '', redirectTo:'products', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes), RouterLink],
  exports: [RouterModule]
})
export class AppRoutingModule { }
