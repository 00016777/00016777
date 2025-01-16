import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavigationService } from './core/navigation/navigation.service';
import { AuthService } from './core/auth/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { UserService } from './core/user/user.service';

@Component({
    selector   : 'app-root',
    templateUrl: './app.component.html',
    styleUrls  : ['./app.component.scss'],
    standalone : true,
    imports    : [RouterOutlet],
})
export class AppComponent implements OnInit
{
    /**
     * Constructor
     */
    constructor(
        private _userService: UserService,
        private _router: Router,
        private _navigationService: NavigationService,
        private authService: AuthService)
    {
        this.authService.authenticated$
            .pipe(takeUntilDestroyed())
            .subscribe({
                next: _=> {
                    this._navigationService.updateNavigationByUserPermissions();
                }
            })
    }
    ngOnInit(): void {

        this._userService.user$.subscribe((user) => {
            if (user?.mainRoleId === 3) {
                this._router.navigate(['Meal']);
            }
            
            if (user?.mainRoleId === 2) {
                this._router.navigate(['Product']);
            }
        });
    }
}
