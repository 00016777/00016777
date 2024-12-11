import { Injectable } from '@angular/core';
import { take } from 'rxjs/operators';
import { FuseNavigationService, FuseVerticalNavigationComponent, FuseHorizontalNavigationComponent, FuseNavigationItem } from '@fuse/components/navigation';
import { TranslocoService } from '@ngneat/transloco';
import { UserService } from '../user/user.service';
import { User } from '../user/user.types';
import { InitialNavigations } from 'app/mock-api/common/navigation/navigation-provider';
import { Navigation } from './navigation.types';
import { Observable, of, ReplaySubject} from 'rxjs';
import { user } from 'app/mock-api/common/user/data';
import { AccountClient } from 'NSwag/nswag-api-restaurant';
import { Roles } from '../Enums/Roles';


@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    private _navigation: ReplaySubject<Navigation> =
        new ReplaySubject<Navigation>(1);
    private _permittedUrls: ReplaySubject<string[]> = new ReplaySubject<
        string[]
    >(1);
    private _navigationValue: Navigation;

    constructor(
        private userService: UserService,
        private _translocoService: TranslocoService,
        private _fuseNavigationService: FuseNavigationService,
        private accountClient: AccountClient
    ) {
        this.navigation = InitialNavigations.navigations;
    }

    get navigation$(): Observable<Navigation> {
        return this._navigation.asObservable();
    }

    get permittedUrls$(): Observable<string[]> {
        return this._permittedUrls.asObservable();
    }

    set navigation(nav: Navigation) {
        this._navigation.next(nav);
        this._navigationValue = nav;

        this._permittedUrls.next(
            NavigationService.allUrls(this._navigationValue.default),
        );
    }

    /**
     * This is only for appresolver
     */
    get = (): Observable<Navigation> => of(InitialNavigations.navigations);

    updateNavigationByUserPermissions() {
        this.userService.user$.subscribe(_user => {
            let navFiltered: Navigation = this._navigationValue;
            if (navFiltered) {
                navFiltered.compact = this.filterByUserPermission(
                    navFiltered.compact,
                    _user,
                );
                navFiltered.default = this.filterByUserPermission(
                    navFiltered.default,
                    _user,
                );
                navFiltered.futuristic = this.filterByUserPermission(
                    navFiltered.futuristic,
                    _user,
                );
                navFiltered.horizontal = this.filterByUserPermission(
                    navFiltered.horizontal,
                    _user,
                );
                this.navigation = navFiltered;

                this._fuseNavigationService
                    .getMainNavigation()
                    .pipe(take(1))
                    .subscribe({
                        next: (
                            navVComponent:
                                | FuseVerticalNavigationComponent
                                | FuseHorizontalNavigationComponent,
                        ) => {
                            navVComponent.refresh();
                        },
                    });
            }
        });
    }

    // checkStudentPageAvailability(spService: SpService) {
    //     combineLatest([
    //         this.navigation$,
    //         this.userService.user$
    //     ])
    //         .pipe(take(1))
    //         .subscribe({
    //             next: navUser => {
    //                 let nav = navUser[0];
    //                 let user = navUser[1];
    //                 if (!user || !nav?.default) return;

    //                 // console.log(nav.default);



    //                     this.navigation = nav;

    //                     this._fuseNavigationService
    //                         .getMainNavigation()
    //                         .pipe(take(1))
    //                         .subscribe({
    //                             next: (
    //                                 navVComponent:
    //                                     | FuseVerticalNavigationComponent
    //                                     | FuseHorizontalNavigationComponent,
    //                             ) => {
    //                                 navVComponent.refresh();
    //                             },
    //                         });
    //                 }
    //             })
    // }

    private static allUrls(navigations: FuseNavigationItem[]): string[] {
        const urls = [];

        for (const nav of navigations) {
            this.checkOnNavigation(nav, '', urls);
        }

        return urls;
    }

    private static checkOnNavigation(
        navigation: FuseNavigationItem,
        url: string,
        urls: string[],
    ): void {
        url = url?.startsWith('/') ? url.substring(1) : url;

        let _url: string;
        if (navigation.children?.length > 0) {
            for (const nav of navigation.children) {
                _url = url ? url + '/' + nav.link : nav.link;

                this.checkOnNavigation(nav, _url, urls);
            }
        }

        if (url) urls.push(url);
    }

    /**
     * Update the navigation
     *
     * @param lang
     */

    public updateNavigation(lang: string): void {
        // public updateNavigation(): void {
        // For the demonstration purposes, we will only update the Dashboard names
        // from the navigation but you can do a full swap and change the entire
        // navigation data.
        //
        // You can import the data from a file or request it from your backend,
        // it's up to you.

        // Get the component -> navigation data -> item
        this._fuseNavigationService
            .getMainNavigation()
            .pipe(take(1))
            .subscribe({
                next: (
                    navVComponent:
                        | FuseVerticalNavigationComponent
                        | FuseHorizontalNavigationComponent,
                ) => {
                    let copy = { ...this._navigationValue };
                    let array = [
                        copy.default,
                        copy.compact,
                        copy.futuristic,
                        copy.horizontal,
                    ];
                    for (const all of array) {
                        this._changeTitle(all, lang);
                    }

                    this.navigation = copy;
                    navVComponent.refresh();
                },
            });
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Private methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * @private
     */
    private filterByUserPermission(
        items: FuseNavigationItem[],
        _user: User,
    ): FuseNavigationItem[] {
        return items?.map(item => {
            let permitted = true;
        
            if (item.hasOwnProperty('key'))
                permitted = item.key.includes(this.getMainRole(_user?.mainRoleId));

            if (!permitted) return {} as FuseNavigationItem;

            if (permitted && item.children?.length > 0)
                item.children = this.filterByUserPermission(
                    item.children,
                    _user,
                );

            return item;
        });
    }

    private getMainRole(roleId: number): string {
        switch(roleId){
            case 1: return "Admin";
            case 2: return "Manager";
            case 3: return "Suplier";
            case 4: return "Student";
        }
    }

    private _changeTitle(navigation: FuseNavigationItem[], lang: string): void {
        for (const navItem of navigation) {
            if (navItem.children) this._changeTitle(navItem.children, lang);

            if (!navItem?.translate) continue;

            let item: FuseNavigationItem = this._fuseNavigationService.getItem(
                navItem.id,
                navigation,
            );

            if (item) {
                let copy = `${navItem.translate}`;
                const translationKey = `nav.${copy.replace('-', '_')}`;
                const translation = this._translocoService.translate(
                    translationKey,
                    null,
                    lang,
                );

                // Set the title
                if (translation && navItem.id != 'nav.' + translation)
                    item.title = translation;
            }
        }
    }
}
