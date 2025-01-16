import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, Routes } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { MealService } from './meal.service';
import { MealDetailsComponent } from './meal-details/meal-details.component';
import { MealComponent } from './meal.component';
import { MealListComponent } from './meal-list/meal-list.component';

/**
 * Folder resolver
 *
 * @param route
 * @param state
 */
const mealResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) =>
{
    const mealManagerService = inject(MealService);
    const router = inject(Router);

    return mealManagerService.getItems(route.paramMap.get('mealId'), mealManagerService.search).pipe(
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
const mealChildrenResolver = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) =>
{
    const mealManagerService = inject(MealService);
    const router = inject(Router);

    return mealManagerService.getItemById(route.paramMap.get('id')).pipe(
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
    component: MealDetailsComponent,
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
    if ( !nextState.url.includes('/Meal') )
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
        component: MealComponent,
        children : [
            {
                path     : 'Meal/:mealId',
                component: MealListComponent,
                resolve  : {
                    item: mealResolver,
                },
                children : [
                    {
                        path         : 'details/:id',
                        component    : MealDetailsComponent,
                        resolve      : {
                            item: mealChildrenResolver,
                        },
                        canDeactivate: [canDeactivateFileManagerDetails],
                    },
                ],
            },
            {
                path     : '',
                component: MealListComponent,
                resolve  : {
                    items: () => inject(MealService).getItems(null,""),
                },
                children : [
                    {
                        path         : 'details/:id',
                        component    : MealDetailsComponent,
                        resolve      : {
                            item: mealChildrenResolver,
                        },
                        canDeactivate: [canDeactivateFileManagerDetails],
                    },
                ],
            },
        ],
    },
] as Routes;
