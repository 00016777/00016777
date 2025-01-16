import { TextFieldModule } from '@angular/cdk/text-field';
import { NgIf, NgFor, NgClass, DatePipe, AsyncPipe } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, Renderer2, ViewContainerRef, ViewEncapsulation } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Overlay } from '@angular/cdk/overlay';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { OrderListComponent } from '../order-list/order-list.component';
import { OrderService } from '../order.service';
import { BasketClient, BasketDto, MealClient, MealDto, OrderDetailDto, OrderDto, OrdersClient, ProductDto, ProductsClient } from 'NSwag/nswag-api-restaurant';
import { BasketService } from '../../basket/basket.service';
import { MealService } from '../../meal/meal.service';
import { take } from 'rxjs';
import { MatDrawerToggleResult } from '@angular/material/sidenav';
import { ProductService } from '../../product/product.service';
import { UserService } from 'app/core/user/user.service';
import { user } from 'app/mock-api/common/user/data';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  imports        : [NgIf, AsyncPipe, MatButtonModule, MatTooltipModule, RouterLink, MatIconModule, NgFor, FormsModule, ReactiveFormsModule, MatRippleModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, NgClass, MatSelectModule, MatOptionModule, MatDatepickerModule, TextFieldModule, DatePipe],
  standalone     : true,
})
export class OrderDetailComponent implements OnInit {


  /**
   * 
   */
  meals: MealDto[];
  products: ProductDto[];
  mealOrderDetails: OrderDetailDto[];
  productOrderDetails: OrderDetailDto[];
  order: OrderDto;
  totalPrice: number = 0;
  /**
   *
   */
  constructor(
    private _ordersListComponent: OrderListComponent,
    private _changeDetectorRef: ChangeDetectorRef,
        public basketService: BasketService,
        private basketClient: BasketClient,
        private mealClient: MealClient,
        private productClient: ProductsClient,
        private mealService: MealService,
        private route: ActivatedRoute,
        private orderService: OrderService,
        private productService: ProductService,
        public userService: UserService,
        private orderClient: OrdersClient
  ) {
    
  }

  get TotalPrice$(){
    return this.totalPrice
  }


  ngOnInit(): void {
    
     // Open the drawer
    this._ordersListComponent.matDrawer.open();

    let id = 0;
    this.route.paramMap.subscribe((params) => {
       id = +params.get('id');
      });

    this.orderClient.getOrderById(id).subscribe((order) => {

            let mealIds: number[] = [];
            let productIds: number[] = [];

            mealIds = order?.orderDetailDtos?.map(od => (od.mealId ?? 0));
            productIds = order?.orderDetailDtos?.map(od => (od.productId ?? 0));

            this.order = order;

            this.mealOrderDetails = order?.orderDetailDtos?.filter(o => o.mealId != null);
            this.productOrderDetails = order?.orderDetailDtos?.filter(o => o.productId != null);

            this.mealClient.getMealsByIds(mealIds).pipe(
              take(1),
            ).subscribe((meals)=> {
              
              if(meals)
                this.meals = meals;

                this.totalPrice += this.meals?.map((x) => {
                  
                  let quantity = this.order?.orderDetailDtos?.find(b => b?.mealId == x.id)?.quantity;
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
                    
                    let quantity = this.order?.orderDetailDtos?.find(b => b?.productId == x.id)?.quantity;
                    let value =( quantity ?? 0) * (x?.pricePerKG ?? 0);
                    
                    return value;
                    
                  }).reduce((acc, curr) => acc += curr, 0);
                }
              })
                
                this._changeDetectorRef.markForCheck();
              });
          });
  }

  trackByFn(index: number, item: any): any
  {
      return item.id || index;
  }

  getMeal(mealId: number){
    return this.meals?.find(m => m.id == mealId);
  }

  getProduct(productId: number){
    return this.products?.find(m => m.id == productId);
  }

  getMealImgUrl(mealId: number): string{
    return this.mealService.getMealUrl(this.meals.find(m => m.id == mealId)?.images[0])
  }

  getProductImgUrl(productId: number): string{
    return this.productService.getProductUrl(this.products.find(m => m.id == productId)?.images[0])
  }

  /**
     * Close the drawer
     */
  closeDrawer(): Promise<MatDrawerToggleResult>
  {
      return this._ordersListComponent.matDrawer.close();
  }

}
