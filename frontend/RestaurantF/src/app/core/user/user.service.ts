import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AccountClient, UserDto } from 'NSwag/nswag-api-restaurant';
import { map, Observable, of, ReplaySubject, switchMap, tap } from 'rxjs';
import { User } from './user.types';

@Injectable({providedIn: 'root'})
export class UserService
{
    private _user: ReplaySubject<User> = new ReplaySubject<User>(1);
    private _mainRole: ReplaySubject<string> = new ReplaySubject<string>(1);

    /**
     * Constructor
     */
    constructor(private _httpClient: HttpClient,
        private accountClient: AccountClient
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for user
     *
     * @param value
     */
    set user(value: User)
    {
        // Store the value
        this._user.next(value);
    }

    get user$(): Observable<User>
    {
        return this._user.asObservable();
    }

    set mainRole(role: string){
        this._mainRole.next(role);
    }

    get mainRole$(): Observable<string>{
        return this._mainRole.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Get the current logged in user data
     */
    get(): Observable<User>
    {
        return this.accountClient.userProfile().pipe(
            tap((user) =>
            {
                this.user = user;

                return user;
            })
        );
        
    }

    /**
     * Update the user
     *
     * @param user
     */
    update(user: User): Observable<any>
    {
        return this.accountClient.updateUser().pipe(
            map((user) =>
            {
                this._user.next(user);
            }),
        );
    }
}
