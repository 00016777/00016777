/* eslint-disable */
import { FuseNavigationItem } from '@fuse/components/navigation';
import { Roles } from 'app/core/Enums/Roles';

export const defaultNavigation: FuseNavigationItem[] = [
    {
        key: [
            Roles.Admin,
            Roles.Manager,
            Roles.Student
        ],
        id   : 'example',
        translate: 'Example',
        title: 'Example',
        type : 'basic',
        icon : 'heroicons_outline:chart-pie',
        link : '/example'
    }
];
export const compactNavigation: FuseNavigationItem[] = defaultNavigation;
export const futuristicNavigation: FuseNavigationItem[] = defaultNavigation;
export const horizontalNavigation: FuseNavigationItem[] = defaultNavigation;
