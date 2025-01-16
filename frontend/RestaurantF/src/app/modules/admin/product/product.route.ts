import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from "@angular/router";
import { ProductService } from "./product.service";
import { catchError, throwError } from "rxjs";
import { ProductDetailComponent } from "./product-detail/product-detail.component";
import { ProductComponent } from "./product.component";
import { ProductListComponent } from "./product-list/product-list.component";


/**
 * 
 *
 * @param route
 * @param state
 */
const mealResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) =>
{
    const productService = inject(ProductService);
    const router = inject(Router);

    return productService.getItems(productService.search).pipe(
        // Error here means the requested meal is not available
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
 * Item resolver
 *
 * @param route
 * @param state
 */
const ProductResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) =>
{
    const productService = inject(ProductService);
    const router = inject(Router);

    return productService.getItemById(route.paramMap.get('id')).pipe(
        // Error here means the requested item is not available
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
 * Can deactivate file manager details
 *
 * @param component
 * @param currentRoute
 * @param currentState
 * @param nextState
 */
const canDeactivateFileManagerDetails = (
    component: ProductDetailComponent,
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

    // If the next state doesn't contain '/file-manager'
    // it means we are navigating away from the
    // file manager app
    if ( !nextState.url.includes('/Product') )
    {
        // Let it navigate
        return true;
    }

    // If we are navigating to another item...
    if ( nextState.url.includes('/details') )
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
        component: ProductComponent,
        children : [
            {
                path     : 'Product/:productId',
                component: ProductListComponent,
                resolve  : {
                    item: mealResolver,
                },
                children : [
                    {
                        path         : 'details/:id',
                        component    : ProductDetailComponent,
                        resolve      : {
                            item: ProductResolver,
                        },
                        canDeactivate: [canDeactivateFileManagerDetails],
                    },
                ],
            },
            {
                path     : '',
                component: ProductListComponent,
                resolve  : {
                    items: () => inject(ProductService).getItems(""),
                },
                children : [
                    {
                        path         : 'details/:id',
                        component    : ProductDetailComponent,
                        resolve      : {
                            item: ProductResolver,
                        },
                        canDeactivate: [canDeactivateFileManagerDetails],
                    },
                ],
            },
        ],
    },
] as Routes;
