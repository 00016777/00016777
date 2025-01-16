import { NgFor, NgIf, NgClass, CurrencyPipe, AsyncPipe, CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit, Signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatRippleModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { TranslocoModule } from '@ngneat/transloco';
import { NgApexchartsModule } from 'ng-apexcharts';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { BasketService } from '../basket.service';
import { map, switchMap, take } from 'rxjs';
import { BasketClient, BasketDto, BasketItemDto, MealClient, MealDto, OrderDto, OrdersClient, OrderStatus, ProductDto, ProductsClient } from 'NSwag/nswag-api-restaurant';
import { MealService } from '../../meal/meal.service';
import { OrderService } from '../../order/order.service';
import { Router } from '@angular/router';
import { ProductService } from '../../product/product.service';
import { UserService } from 'app/core/user/user.service';



@Component({
  standalone: true,
  selector: 'basket-list',
  templateUrl: './basket-list.component.html',
  styleUrls: ['./basket-list.component.scss'],
  imports: [TranslocoModule, CommonModule, CurrencyPipe, AsyncPipe, MatIconModule, MatButtonModule, MatRippleModule, MatMenuModule, MatTabsModule, MatButtonToggleModule, NgApexchartsModule, NgFor, NgIf, MatTableModule, NgClass, CurrencyPipe]
})
export class BasketListComponent 
{

  meals: MealDto[];
  products: ProductDto[];
  basket: BasketDto;
  totalPrice: number = 0;
  isBought: boolean = false;
  mealBasketItems: BasketItemDto[] = [];
  productBasketItems: BasketItemDto[] = [];
  /**
   *
   */
  constructor(
    public basketService: BasketService,
    private basketClient: BasketClient,
    private mealClient: MealClient,
    private productClient: ProductsClient,
    private mealService: MealService,
    private productService: ProductService,
    private orderService: OrderService,
    public userService: UserService,
    private _changeDetectorRef: ChangeDetectorRef,
    private router: Router) {
      
      this.basketClient.getBasket().subscribe((basket) => {
        this.basketService.basket = basket;

        let mealIds: number[] = [];
        let productIds: number[] = [];
        mealIds = basket?.basketItems?.map(b => b?.mealId ?? 0);
        productIds = basket?.basketItems?.map(b => b?.productId ?? 0);

        this.mealBasketItems = basket?.basketItems.filter(b => b.mealId != null)
        this.productBasketItems = basket?.basketItems.filter(b => b.productId != null)

        this.basket = basket;
        
        this.mealClient.getMealsByIds(mealIds).pipe(
          take(1),
        ).subscribe((meals)=> {
          
          if(meals)
            this.meals = meals;
          
            this.totalPrice += this.meals?.map((x) => {
              
              let quantity = this.basket?.basketItems?.find(b => b?.mealId == x.id)?.quantity;
              let value =( quantity ?? 0) * (x?.price ?? 0);
              
              return value;

            }).reduce((acc, curr) => acc += curr, 0);

        });

        this.productClient.getProductsByIds(productIds).pipe(
          take(1),
        ).subscribe((products)=> {
          
          if(products)
            this.products = products;
          
            this.userService.user$.subscribe((user) => {

              if(user?.mainRoleId != 3){

                this.totalPrice += this.products?.map((x) => {
                  
                  let quantity = this.basket?.basketItems?.find(b => b?.productId == x.id)?.quantity;
                  let value =( quantity ?? 0) * (x?.pricePerKG ?? 0);
                  
                  return value;
                  
                }).reduce((acc, curr) => acc += curr, 0);
              }
            })

        });
      });
  }

  get TotalPrice$(){
      return this.totalPrice
  }

  /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
  trackByFn(index: number, item: any): any
  {
      return item.id || index;
  }

  getMeal(mealId?: number){
    return this.meals?.find(m => m.id == (mealId ?? 0));
  }

  getProduct(productId?: number){
    return this.products?.find(p => p.id == (productId ?? 0));
  }

  getMealImgUrl(mealId: number): string{
    return this.mealService.getMealUrl(this.meals.find(m => m.id == mealId)?.images[0])
  }

  getProductImgUrl(productId: number): string{
    return this.productService.getProductUrl(this.products.find(m => m.id == productId)?.images[0])
  }

  removeItem(basketItemId: number){

      this.basketService.basket$.pipe(
        take(1),
        map((basket) => {

          let index = basket?.basketItems.findIndex(x => x.id == basketItemId);

          basket?.basketItems.splice(index, 1);

          this.mealBasketItems = basket?.basketItems.filter(b => b.mealId != null)
          this.productBasketItems = basket?.basketItems.filter(b => b.productId != null)

          this.basket = basket;
          this.totalPrice = 0;

          this.totalPrice += this.meals?.map((x) => {
              
            let quantity = this.basket?.basketItems?.find(b => b?.mealId == x.id)?.quantity;
            let value =( quantity ?? 0) * (x?.price ?? 0);
            
            return value;
    
          }).reduce((acc, curr) => acc += curr, 0);
          
          this.totalPrice += this.products?.map((x) => {
              
            let quantity = this.basket?.basketItems?.find(b => b?.productId == x.id)?.quantity;
            let value =( quantity ?? 0) * (x?.pricePerKG ?? 0);
            
            return value;
    
          }).reduce((acc, curr) => acc += curr, 0);
          
          return basket;
        }),
        switchMap((basket) => this.basketService.saveBasket(basket))
      ).subscribe();
  }

  buy(){

    let order: OrderDto = new OrderDto();

    order.orderDate = new Date();
    order.status = OrderStatus.Processing;
    order.orderDetailDtos = [...this.basket.basketItems];
    
    this.basketClient.setInActiveBasketItem(this.basket.id).subscribe();
    
    setTimeout(() => {
      this.basketService.basket = this.basket.basketItems = undefined;
      this.router.navigate(['../order'])

    }, 2000);


    this.orderService.saveOrder(order).subscribe({
      next: () => {this.isBought = true; setTimeout(() => {
        this.isBought = false
      }, 1000);}
    });
  }   
}
