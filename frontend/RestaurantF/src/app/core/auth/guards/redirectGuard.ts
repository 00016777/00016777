import { inject } from "@angular/core";
import { CanActivateChildFn, CanActivateFn, Router } from "@angular/router";
import { UserService } from "app/core/user/user.service";
import { map, of, switchMap } from "rxjs";


export const RedirectGuard: CanActivateFn | CanActivateChildFn = (route, state) =>
{
    const router: Router = inject(Router);
    // Check the authentication status
    return inject(UserService).user$.pipe(
        map((user) => {
            debugger
            if (user?.mainRoleId === 1 || user?.mainRoleId === 3) {
                // Redirect to Meal page
                router.navigate(['/Meal']);
                return false
            }

            if (user?.mainRoleId === 2) {
                // Redirect to Product page
                router.navigate(['/Product']);
                return false

            }

            // Allow navigation if no redirection
            return true;
        }),
    );
};
