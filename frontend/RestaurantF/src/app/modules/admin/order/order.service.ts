import { Injectable } from "@angular/core";
import { OrderDto, OrdersClient, UserDto } from "NSwag/nswag-api-restaurant";
import { BehaviorSubject, map, Observable, switchMap, take, tap } from "rxjs";

@Injectable({providedIn:'root'})
export class OrderService{

    private _order: BehaviorSubject<OrderDto | null> = new BehaviorSubject<OrderDto | null>(null);
    private _orders: BehaviorSubject<OrderDto[] | null> = new BehaviorSubject<OrderDto[] | null>([]);
    orders: OrderDto[];
    orderers: UserDto[];
    /**
     *
     */
    constructor(private orderClient: OrdersClient) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Accessors
    get orders$() : Observable<OrderDto[]> {
        return this._orders.asObservable();
    }

    saveOrder(order: OrderDto): Observable<OrderDto>{
        return this.orders$.pipe(
            take(1),
            switchMap((orders) => this.orderClient.saveOrder(order).pipe(
                map((order) => {

                    var index = orders.findIndex(o => o.id == order.id);

                    if(index == -1){
                        
                        orders.push(order)
                    
                    } else{

                        orders[index] = order
                    }

                    this._orders.next(orders)

                    return order;
                })
            ))
        )
    }

    getAllOrders() {
        this.orders$.pipe(
            take(1),
            switchMap(() => this.orderClient.getAllOrders()
                .pipe(
                    map((orders) => {

                        this.orders = orders;
                        this._orders.next(orders);
                        return orders;
                        
                    })
                )
            )
        ).subscribe();
    }
    

    getOrdersByUser(){
        this.orders$.pipe(
            take(1),
            switchMap((orders) => this.orderClient.getOrdersByUser().pipe(
                map((orders) => {

                    this._orders.next(orders);

                    return orders;
                })
            ))
        ).subscribe()
    }
    
}