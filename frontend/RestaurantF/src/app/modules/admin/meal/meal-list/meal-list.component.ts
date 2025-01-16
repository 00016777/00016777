import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { ActivatedRoute, Router, RouterLink, RouterOutlet } from '@angular/router';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { Subject, switchMap, takeUntil, tap } from 'rxjs';
import { MealService } from '../meal.service';
import { MealDto, MealDtos } from 'NSwag/nswag-api-restaurant';
import { NgIf, NgFor, AsyncPipe, NgClass, I18nPluralPipe } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { UserService } from 'app/core/user/user.service';
import { FormsModule, ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-meal-list',
  templateUrl: './meal-list.component.html',
  styleUrls: ['./meal-list.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone     : true,
  imports        : [MatSidenavModule, RouterOutlet, NgIf, RouterLink, NgFor, MatButtonModule, MatIconModule, MatTooltipModule, AsyncPipe, FormsModule
    ,RouterOutlet, NgIf, MatFormFieldModule, MatIconModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, NgFor, NgClass, RouterLink, AsyncPipe, I18nPluralPipe
  ],
})
export class MealListComponent implements OnInit, OnDestroy
{
    @ViewChild('matDrawer', {static: true}) matDrawer: MatDrawer;
    searchInputControl: UntypedFormControl = new UntypedFormControl();
    drawerMode: 'side' | 'over';
    selectedMeal: MealDto;
    meals: MealDtos;
    private _unsubscribeAll: Subject<any> = new Subject<any>();

    /**
     * Constructor
     */
    constructor(
        private _activatedRoute: ActivatedRoute,
        private _changeDetectorRef: ChangeDetectorRef,
        private _router: Router,
        public _mealService: MealService,
        private _fuseMediaWatcherService: FuseMediaWatcherService,
        public _userService: UserService,
    )
    {
        _userService.get().subscribe();
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Lifecycle hooks
    // -----------------------------------------------------------------------------------------------------

    /**
     * On init
     */
    ngOnInit(): void
    {
        this._mealService.search = ""
        // Get the items
        this._mealService.meals$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((meals: MealDtos) =>
            {
                this._mealService.meals = meals;
                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Get the item
        this._mealService.meal$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((meal: MealDto) =>
            {
                this.selectedMeal = meal;

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to media query change
        this._fuseMediaWatcherService.onMediaQueryChange$('(min-width: 1440px)')
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((state) =>
            {
                // Calculate the drawer mode
                this.drawerMode = state.matches ? 'side' : 'over';

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });

        // Subscribe to search input field value changes
        this.searchInputControl.valueChanges
        .pipe(
            takeUntil(this._unsubscribeAll),
            tap((q) => { this._mealService.search = q }),
            switchMap(query => 
                // Search
                this._mealService.getItems(this._activatedRoute.snapshot.paramMap.get('mealId'), query),
            ),
        )
        .subscribe();
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
     * On backdrop clicked
     */
    onBackdropClicked(): void
    {
        // Go back to the list
        this._router.navigate(['./'], {relativeTo: this._activatedRoute});

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    /**
     * Track by function for ngFor loops
     *
     * @param index
     * @param item
     */
    trackByFn(index: number, item: any): any
    {
        return item.id || index;
    }

    addMeal() {
        this._mealService.createMeal().subscribe((newMeal) => {
            
            this._router.navigate(['details', newMeal.id], {relativeTo: this._activatedRoute});

            this._mealService.editMode = !this._mealService.editMode;
        });

        this._mealService.getCategories();
    }

    addCategory(){
        this._mealService.createCategory().subscribe((newCategory) => {

            this._router.navigate(['details', newCategory.id], {relativeTo: this._activatedRoute });

            this._mealService.editMode = !this._mealService.editMode;
        })

        this._mealService.getCategories();
    }
}
