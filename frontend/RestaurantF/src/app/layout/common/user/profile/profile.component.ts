import { NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit, ViewEncapsulation } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDrawer } from '@angular/material/sidenav';
import { UserService } from 'app/core/user/user.service';
import { UserDto } from 'NSwag/nswag-api-restaurant';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [MatButtonModule,MatIconModule,NgIf,MatFormFieldModule,MatInputModule, FormsModule]
})
export class ProfileComponent implements OnInit, OnDestroy {

  @Input() drawer: MatDrawer;
    profile: UserDto;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(private _userService: UserService)
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
        // Profile
        this._userService.user$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((profile: UserDto) =>
            {
                this.profile = profile;
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
}
