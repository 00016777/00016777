import { BooleanInput } from '@angular/cdk/coercion';
import { CommonModule, NgClass, NgIf } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { ActivatedRoute, Router } from '@angular/router';
import { Roles } from 'app/core/Enums/Roles';
import { NavigationService } from 'app/core/navigation/navigation.service';
import { UserService } from 'app/core/user/user.service';
import { User } from 'app/core/user/user.types';
import { AccountClient, MainRole } from 'NSwag/nswag-api-restaurant';
import { Subject, takeUntil } from 'rxjs';

@Component({
    selector       : 'user',
    templateUrl    : './user.component.html',
    encapsulation  : ViewEncapsulation.None,
    changeDetection: ChangeDetectionStrategy.OnPush,
    exportAs       : 'user',
    standalone     : true,
    imports        : [MatButtonModule, MatMenuModule, NgIf, MatIconModule, NgClass, MatDividerModule, CommonModule],
})
export class UserComponent implements OnInit, OnDestroy
{
    /* eslint-disable @typescript-eslint/naming-convention */
    static ngAcceptInputType_showAvatar: BooleanInput;
    /* eslint-enable @typescript-eslint/naming-convention */

    @Input() showAvatar: boolean = true;
    user: User;
    roles: string[];

    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _router: Router,
        private _userService: UserService,
        private accountClient: AccountClient,
        private _navigationService: NavigationService,
        private _activatedRoute: ActivatedRoute,
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        // Subscribe to user changes
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((user: User) =>
            {
                this.user = user;
                this.roles = user.roles;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

    }

    /**
     * On destroy
     */
    ngOnDestroy(): void
    {
        // Unsubscribe from all subscriptions
        this._unsubscribeAll.next(null);
        this._unsubscribeAll.complete();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Update the user status
     *
     * @param status
     */
    updateUserStatus(role: string): void
    {
        debugger
        // Return if user is not available
        if ( !this.user )
        {
            return;
        }

        if(role.toLocaleLowerCase() == 'suplier'){
            this.user.mainRoleId = 2;
            this._userService.user = this.user
        } 
        if(role.toLocaleLowerCase() == 'student'){
            this.user.mainRoleId = 3;
            this._userService.user = this.user
        } 
        if(role.toLocaleLowerCase() == 'manager'){
            this.user.mainRoleId = 1;
            this._userService.user = this.user
        } 

        // change User main role
        this.accountClient.chooseMainRole( {username: this.user?.userName, role: role } as MainRole)
            .subscribe({
                next: _=> {
                    this._userService.get().subscribe();
                    this.accountClient.userProfile()
                    window.location.reload();
                }
            });
        
        this._router.navigate([''])
        
        // window.location.reload();
    }

    getMainRole(role: string){
        return this._navigationService.getMainRole(this.user?.mainRoleId).toLowerCase() === role;
    }

    /**
     * Sign out
     */
    signOut(): void
    {
        this._router.navigate(['/sign-out']);
    }
}
