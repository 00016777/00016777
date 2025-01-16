import { Injectable } from "@angular/core";
import { UserService } from "app/core/user/user.service";
import { BasketClient, BasketDto } from "NSwag/nswag-api-restaurant";
import { BehaviorSubject, map, Observable, switchMap, take } from "rxjs";
import { MealService } from "../meal/meal.service";


@Injectable({providedIn: 'root'})
export class BasketService{

    private _basket: BehaviorSubject<BasketDto | null> = new BehaviorSubject<BasketDto | null>(null);
    /**
     *
     */
    constructor(
        private basketClient: BasketClient,
        private mealService: MealService
    ) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Accessors
    set basket(basket: BasketDto){
        this._basket.next(basket);
    }

    get basket$(): Observable<BasketDto> {
        return this._basket.asObservable();
    }

    getBasket(){
        this.basket$.pipe(
            take(1),
            switchMap((basket) => this.basketClient.getBasket().pipe(
                map((basket) => {
                    
                    this._basket.next(basket);

                    return basket;
                })
            ))
        )
    }

    getBasketById(id: number): Observable<BasketDto>{
        return this.basket$.pipe(
            take(1),
            switchMap((basket) => this.basketClient.getBasketById(id).pipe(
                map((basket) => {
                    
                    this._basket.next(basket);

                    return basket;
                })
            ))
        )
    }

    saveBasket(basketDto: BasketDto): Observable<BasketDto> {
        return this.basket$.pipe(
            take(1),
            switchMap((basket) => this.basketClient.saveBasket(basketDto).pipe(
                map((newBasket) => {
                    
                    this._basket.next(newBasket);
                    
                    return newBasket;
                })
            ))
        );
    }

}