/* eslint-disable */
import { FuseNavigationItem } from '@fuse/components/navigation';
import { Roles } from 'app/core/Enums/Roles';

export const defaultNavigation: FuseNavigationItem[] = [
    {
        key: [
            Roles.Manager,
            Roles.Student
        ],
        id   : 'Meal',
        translate: 'Meal',
        title: 'Meals',
        type : 'basic',
        icon : 'heroicons_outline:chart-pie',
        link : '/Meal'
    },
    {
        key: [
            Roles.Manager,
            Roles.Student
        ],
        id   : 'basket',
        translate: 'Basket',
        title: 'Basket',
        type : 'basic',
        icon : 'heroicons_outline:shopping-cart',
        link : '/basket'
    },
    {
        key: [
            Roles.Manager,
            Roles.Student,
            Roles.Suplier
        ],
        id   : 'order',
        translate: 'Orders',
        title: 'Orders',
        type : 'basic',
        icon : 'heroicons_outline:clipboard-document-check',
        link : '/order'
    },
    {
        key: [
            Roles.Manager,
            Roles.Suplier
        ],
        id   : 'product',
        translate: 'Products',
        title: 'Products',
        type : 'basic',
        icon : 'heroicons_outline:truck',
        link : '/Product'
    },
];
export const compactNavigation: FuseNavigationItem[] = defaultNavigation;
export const futuristicNavigation: FuseNavigationItem[] = defaultNavigation;
export const horizontalNavigation: FuseNavigationItem[] = defaultNavigation;
