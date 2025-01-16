import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { OrderService } from './order.service';
import { OrderComponent } from './order.component';
import { OrderListComponent } from './order-list/order-list.component';
import { OrderDetailDto } from 'NSwag/nswag-api-restaurant';
import { OrderDetailComponent } from './detail/detail.component';
import { BasketService } from '../basket/basket.service';

/**
 * Contact resolver
 *
 * @param route
 * @param state
 */
const orderResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) =>
{
    const basketService = inject(BasketService);
    const router = inject(Router);

    return basketService.getBasketById(+route.paramMap.get('id'))
        .pipe(
            // Error here means the requested contact is not available
            catchError((error) =>
            {
                // Log the error
                console.error(error);

                // Get the parent url
                const parentUrl = state.url.split('/').slice(0, -1).join('/');

                // Navigate to there
                router.navigateByUrl(parentUrl);

                // Throw an error
                return throwError(error);
            }),
        );
};

/**
 * Can deactivate contacts details
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */
const canDeactivateContactsDetails = (
    component: OrderDetailComponent,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState: RouterStateSnapshot) =>
{
    // Get the next route
    let nextRoute: ActivatedRouteSnapshot = nextState.root;
    while ( nextRoute.firstChild )
    {
        nextRoute = nextRoute.firstChild;
    }

    // If the next state doesn't contain '/contacts'
    // it means we are navigating away from the
    // contacts app
    if ( !nextState.url.includes('/order') )
    {
        // Let it navigate
        return true;
    }

    // If we are navigating to another contact...
    if ( nextRoute.paramMap.get('id') )
    {
        // Just navigate
        return true;
    }

    // Otherwise, close the drawer first, and then navigate
    return component.closeDrawer().then(() => true);
};

export default [
    {
        path     : '',
        component: OrderComponent,
        children : [
            {
                path     : '',
                component: OrderListComponent,
                children : [
                    {
                        path         : ':id',
                        component    : OrderDetailComponent,
                        resolve      : {
                            order  : orderResolver
                        },
                        canDeactivate: [canDeactivateContactsDetails],
                    },
                ],
            },
        ],
    },
] as Routes;
