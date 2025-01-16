import { Routes } from '@angular/router';
import { BasketComponent } from './basket.component';
import { BasketListComponent } from './basket-list/basket-list.component';

export default [
    {
        path     : '',
        component: BasketComponent,
        children:[
            {
                path: '',
                component: BasketListComponent
            }
        ]
    },
] as Routes;
