import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavigationService } from './core/navigation/navigation.service';
import { AuthService } from './core/auth/auth.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
    selector   : 'app-root',
    templateUrl: './app.component.html',
    styleUrls  : ['./app.component.scss'],
    standalone : true,
    imports    : [RouterOutlet],
})
export class AppComponent
{
    /**
     * Constructor
     */
    constructor(
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
}
