import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthUtils } from 'app/core/auth/auth.utils';
import { UserService } from 'app/core/user/user.service';
import { AccountClient, Login, MainRole, Register, TokenModel } from 'NSwag/nswag-api-restaurant';
import { catchError, map, Observable, of, ReplaySubject, switchMap, throwError } from 'rxjs';
import { Roles } from '../Enums/Roles';

@Injectable({providedIn: 'root'})
export class AuthService
{
    private _authenticated: boolean = false;
    private _authenticated$: ReplaySubject<boolean> = new ReplaySubject(1);

    /**
     * Constructor
     */
    constructor(
        private accountClient: AccountClient,
        private _httpClient: HttpClient,
        private _userService: UserService,
    )
    {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    /**
     * Setter & getter for access token
     */
    set accessToken(token: string)
    {
        localStorage.setItem('accessToken', token);
    }

    get accessToken(): string
    {
        return localStorage.getItem('accessToken') ?? '';
    }

    get authenticated$(): Observable<boolean> {
        return this._authenticated$.asObservable();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Public methods
    // -----------------------------------------------------------------------------------------------------

    /**
     * Forgot password
     *
     * @param email
     */
    forgotPassword(email: string): Observable<any>
    {
        return this._httpClient.post('api/auth/forgot-password', email);
    }

    /**
     * Reset password
     *
     * @param password
     */
    resetPassword(password: string): Observable<any>
    {
        return this._httpClient.post('api/auth/reset-password', password);
    }

    /**
     * Sign in
     *
     * @param credentials
     */
    signIn(login: Login): Observable<any>
    {
        // Throw error, if the user is already logged in
        if ( this._authenticated )
        {
            return throwError('User is already logged in.');
        }

        return this.accountClient.login(login).pipe(
            switchMap((response: any) =>
            {
                debugger
                // Store the access token in the local storage
                this.accessToken = response.token;

                // Set the authenticated flag to true
                this._authenticated = true;
                this._authenticated$.next(true);

                // Store the user on the user service
                this.accountClient.userProfile()
                .subscribe((user) => {
                    this._userService.user = user;
                })

                // Return a new observable with the response
                return of(response);
            }),
        );
    }

    changeMainRole(mainRole: Roles): Observable<TokenModel> {
        return this.accountClient
            .chooseMainRole(new MainRole({ role: mainRole }))
            .pipe(
                map(tokenModel => {
                    this.accessToken = tokenModel.token;
                    // this.userService.get();

                    return tokenModel;
                }),
            );
    }

    /**
     * Sign in using the access token
     */
    signInUsingToken(): Observable<any>
    {
        // Sign in using the token
        return this.accountClient.refreshAccessToken().pipe(
            catchError(() =>

                // Return false
                of(false),
            ),
            switchMap((response: any) =>
            {
                // Replace the access token with the new one if it's available on
                // the response object.
                //
                // This is an added optional step for better security. Once you sign
                // in using the token, you should generate a new one on the server
                // side and attach it to the response object. Then the following
                // piece of code can replace the token with the refreshed one.
                if ( response.token )
                {
                    this.accessToken = response.token;
                }

                // Set the authenticated flag to true
                this._authenticated = true;
                this._authenticated$.next(true);

                // Store the user on the user service
                this.accountClient.userProfile()
                .subscribe((user) => {
                    this._userService.user = user;
                })
                // Return true
                return of(true);
            }),
        );
    }

    /**
     * Sign out
     */
    signOut(): Observable<any>
    {
        // Remove the access token from the local storage
        localStorage.removeItem('accessToken');

        // Set the authenticated flag to false
        this._authenticated = false;

        // Return the observable
        return of(true);
    }

    /**
     * Sign up
     *
     * @param user
     */
    signUp(user: Register): Observable<any>
    {
        return this.accountClient.register(user);
    }

    /**
     * Unlock session
     *
     * @param credentials
     */
    unlockSession(credentials: { email: string; password: string }): Observable<any>
    {
        return this._httpClient.post('api/auth/unlock-session', credentials);
    }

    /**
     * Check the authentication status
     */
    check(): Observable<boolean>
    {
        // Check if the user is logged in
        if ( this._authenticated )
        {
            return of(true);
        }

        // Check the access token availability
        if ( !this.accessToken )
        {
            return of(false);
        }

        // Check the access token expire date
        if ( AuthUtils.isTokenExpired(this.accessToken) )
        {
            return of(false);
        }

        // If the access token exists, and it didn't expire, sign in using it
        return this.signInUsingToken();
    }
}
