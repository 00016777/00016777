import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { debounceTime, map, Subject, switchMap, takeUntil } from 'rxjs';
import { OrderService } from '../order.service';
import { OrderDto, OrdersClient } from 'NSwag/nswag-api-restaurant';
import { FormsModule, ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { NgIf, NgFor, NgTemplateOutlet, NgClass, AsyncPipe, CurrencyPipe, DatePipe, CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatOptionModule, MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { UserService } from 'app/core/user/user.service';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-order-list',
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.scss'],
  imports: [MatSidenavModule,CommonModule, RouterOutlet, NgIf, MatFormFieldModule, MatIconModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, NgFor, NgClass, RouterLink, AsyncPipe, DatePipe, MatProgressBarModule, MatFormFieldModule, MatIconModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, MatSortModule, NgFor, NgTemplateOutlet, MatPaginatorModule, NgClass, MatSlideToggleModule, MatSelectModule, MatOptionModule, MatCheckboxModule, MatRippleModule, AsyncPipe, CurrencyPipe]
})
export class OrderListComponent implements OnInit {

    @ViewChild('matDrawer', {static: true}) matDrawer: MatDrawer;
    drawerMode: 'side' | 'over';

    private _unsubscribeAll: Subject<any> = new Subject<any>();
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    isLoading: boolean = false;
    selectedOrder: OrderDto | null = null;
    orders: OrderDto[];

    statuses: {key: number, value: string }[] = [
      { key: 0, value: "Pending"    },
      { key: 1, value: "Processing" },
      { key: 2, value: "Completed"  },
      { key: 3, value: "Cancelled"  }
    ];

    /**
     *
     */
    constructor(
      public userService: UserService,
      public orderService: OrderService,
      public orderClient: OrdersClient,
      private _changeDetectorRef: ChangeDetectorRef,
      private _router: Router,
      private _activatedRoute: ActivatedRoute,) {
    }

    ngOnInit(): void
    {
      let userL;
      this.userService.user$.subscribe((user) => {

        userL = user

        if(user.mainRoleId == 1 || user?.mainRoleId == 2){
          this.orderService.getAllOrders();
        }
        else this.orderService.getOrdersByUser();
      });

      this.orderService.orders$
        .pipe(takeUntil(this._unsubscribeAll))
        .subscribe((orders: OrderDto[]) => {

          orders = orders?.sort((a, b) => {
            const dateA = new Date(a.orderDate).getTime();
            const dateB = new Date(b.orderDate).getTime();
            return dateB - dateA;
        });

        if(userL?.mainRoleId == 2){
          orders = orders.filter(o => o.orderDetailDtos.some(od => od.productId != null && od.mealId == null))
        }

        if(userL?.mainRoleId == 3){
          orders = orders.filter(o => o.orderDetailDtos.some(od => od.mealId != null && od?.productId == null))
        }

        this.orderService.orders = orders
        this.orders = orders;

          this._changeDetectorRef.markForCheck();

        });

        this.searchInputControl.valueChanges
            .pipe(
                takeUntil(this._unsubscribeAll),
                debounceTime(300),
              )
            .subscribe((search) => {
              this.orders = this.orderService.orders
              .filter(x => x.name.toLocaleLowerCase().includes(search.toLocaleLowerCase()))

              this._changeDetectorRef.markForCheck();
            });

            // Subscribe to MatDrawer opened change
        this.matDrawer.openedChange.subscribe((opened) =>
          {
              if ( !opened )
              {
                  // Remove the selected contact when drawer closed
                  this.selectedOrder = null;

                  // Mark for check
                  this._changeDetectorRef.markForCheck();
              }
          });
    }

    trackByFn(index: number, item: any): any
    {
        return item.id || index;
    }

    statusMap: { [key: number]: string } = {
      0: 'Pending',
      1: 'Processing',
      2: 'Completed',
      3: 'Cancelled',
    };

    getStatusLabel(status: number): string {
      return this.statusMap[status] || '';
    }

    selectRow(order: OrderDto): void {
      this.selectedOrder = order;

      this._router.navigate([order.id], { relativeTo: this._activatedRoute });

    }

    onStatusChange(value: number, order: OrderDto): void {

      order.status = value;

      this.orderService.saveOrder(order).subscribe();
    }

    preventRowSelection(event: MouseEvent): void {
      event.stopPropagation(); // Prevents the click event from bubbling up to the row
    }

    notProduct(order: OrderDto){
      return !order?.orderDetailDtos.some(x => x.productId != null && x.mealId == null);
    }

}
