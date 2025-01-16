import { AsyncPipe, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BasketDto, BasketItemDto, MealDto } from 'NSwag/nswag-api-restaurant';
import { map, Subject, switchMap, take, takeUntil } from 'rxjs';
import { MealListComponent } from '../meal-list/meal-list.component';
import { MealService } from '../meal.service';
import { MatDrawerToggleResult } from '@angular/material/sidenav';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { TextFieldModule } from '@angular/cdk/text-field';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { UserService } from 'app/core/user/user.service';
import { BasketService } from '../../basket/basket.service';
import { MessagesService } from 'app/layout/common/messages/messages.service';
import { Message } from 'app/layout/common/messages/messages.types';
import { FuseAlertService } from '@fuse/components/alert';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone     : true,
  imports        : [NgIf, AsyncPipe, MatButtonModule, RouterLink, MatIconModule, NgFor, FormsModule, ReactiveFormsModule, MatRippleModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, NgClass, MatSelectModule, MatOptionModule, TextFieldModule, DatePipe],
})
export class MealDetailsComponent {
    
    @ViewChild('imagesFileInput') private _imagesFileInput: ElementRef;

    mealCh: MealDto;
    mealForm: UntypedFormGroup;
    private _unsubscribeAll: Subject<any> = new Subject<any>();
    categories: MealDto[];
    quantity: number = 0;
    basketItem: BasketItemDto;
    flashMessage: 'success' | 'error' | null = null;

    /**
     * Constructor
     */
    constructor(
        private _changeDetectorRef: ChangeDetectorRef,
        private _mealListComponent: MealListComponent,
        private _fuseConfirmationService: FuseConfirmationService,
        public _mealService: MealService,
        private _formBuilder: UntypedFormBuilder,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        public _userService: UserService,
        private basketService: BasketService,
        private messageService: MessagesService,
        private fuseAlerService: FuseAlertService
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
        // Open the drawer
        this._mealListComponent.matDrawer.open();

        this.mealForm = this._formBuilder.group({
            id          : [Number],
            name        : ['', [Validators.required]],
            images      : [[]],
            price       : [null],
            description : [null],
            parentId    : [null],
            isCategory  : [false]
        });

        // Get the item
        this._mealService.meal$
            .pipe(takeUntil(this._unsubscribeAll))
            .subscribe((mealCh: MealDto) =>
            {
                // Open the drawer in case it is closed
                this._mealListComponent.matDrawer.open();

                // Get the item
                this.mealCh = mealCh;

                this.mealForm.patchValue({
                    id: mealCh.id,
                    name: mealCh.name,
                    price: mealCh?.price,
                    description: mealCh?.description,
                    parentId: mealCh?.parentId,
                    images: mealCh?.images,
                    isCategory: mealCh.isCategory
                });

                // Mark for check
                this._changeDetectorRef.markForCheck();
            });
        
        this._mealService.getCategories();
        //Get categories
        this._mealService.categories$.subscribe((categories) => {
            if(categories)
            this.categories = categories.filter(x => x.id != this.mealCh?.id)
        })
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
     * Close the drawer
     */
    closeDrawer(): Promise<MatDrawerToggleResult>
    {
        return this._mealListComponent.matDrawer.close();
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

    /**
     * Toggle edit mode
     *
     * @param editMode
     */
    toggleEditMode(editMode: boolean | null = null): void
    {
        if ( editMode === null )
        {
            this._mealService.editMode = !this._mealService.editMode;
        }
        else
        {
            this._mealService.editMode = editMode;
        }

        // Mark for check
        this._changeDetectorRef.markForCheck();
    }

    getMealParentName(parentId: number){
        return this._mealService.meals?.meals.find(x => x.id == parentId)?.name;
    }

    /**
     * Upload avatar
     *
     * @param fileList
     */
    uploadImages(fileList: FileList): void
    {
        // Return if canceled
        if ( !fileList.length )
        {
            return;
        }

        const allowedTypes = ['image/jpeg', 'image/png'];
        let files: File[] = [];
        for(var i = 0; i < fileList.length; i++){
            
            const file = fileList[i];
    
            // Return if the file is not allowed
            
            if ( !allowedTypes.includes(file.type) )
            {
                return;
            }

            files.push(file)
        }

        // Upload the images
        this._mealService.uploadImages(this.mealCh.id, files).subscribe();
    }

    /**
     * Remove the image
     */
    removeImages(): void
    {
        this._mealService.deleteMealImages(this.mealCh.id).subscribe();
        // Get the form control for 'image'
        const imagesFormControl = this.mealForm.get('images');

        // Set the images as null
        imagesFormControl.setValue(null);

        // Set the file input value as null
        this._imagesFileInput.nativeElement.value = null;

        // Update the meal
        this.mealCh.images = null;
    }

    /**
     * Update the meal
     */
    updateMeal(): void
    {
        // Get the contact object
        const meal = this.mealForm.getRawValue();

        // Update the contact on the server
        this._mealService.updateMeal(meal.id, meal).subscribe(() =>
        {
            // Toggle the edit mode off
            this.toggleEditMode(false);
        });
    }

    /**
     * Update the category
     */
    updateCategory(): void
    {
        // Get the meal object
        const meal = this.mealForm.getRawValue();

        // Update the meal on the server
        this._mealService.updateCategory(meal.id, meal).subscribe(() =>
        {
            // Toggle the edit mode off
            this.toggleEditMode(false);
        });
    }

    /**
     * Delete the meal
     */
    deleteMeal(): void
    {
        // Open the confirmation dialog
        const confirmation = this._fuseConfirmationService.open({
            title  : 'Delete meal',
            message: 'Are you sure you want to delete this meal? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        // Subscribe to the confirmation dialog closed action
        confirmation.afterClosed().subscribe((result) =>
        {
            // If the confirm button pressed...
            if ( result === 'confirmed' )
            {
                // Get the current meal's id
                const id = this.mealCh.id;

                // // Get the next/previous meal's id
                // const currentMealIndex = this._mealService.meals.meals.findIndex(item => item.id === id);
                // const nextMealIndex = currentMealIndex + ((currentMealIndex === (this._mealService.meals.meals.length - 1)) ? -1 : 1);
                // const nextMealId = (this._mealService.meals.meals.length === 1 && this._mealService.meals.meals[0].id === id) ? null : this._mealService.meals.meals[nextMealIndex].id;

                // Delete the contact
                this._mealService.deleteMeal(id)
                    .subscribe((isDeleted) =>
                    {
                        // Return if the contact wasn't deleted...
                        if ( !isDeleted )
                        {
                            return;
                        }

                        this._router.navigate(['../../'], {relativeTo: this._activatedRoute});
                        
                        // Toggle the edit mode off
                        this.toggleEditMode(false);
                    });

                // Mark for check
                this._changeDetectorRef.markForCheck();
            }
        });

    }


    /**
     * Delete the category
     */
    deleteCategory(): void
    {
        // Open the confirmation dialog
        const confirmation = this._fuseConfirmationService.open({
            title  : 'Delete category',
            message: 'Are you sure you want to delete this category? This action cannot be undone!',
            actions: {
                confirm: {
                    label: 'Delete',
                },
            },
        });

        // Subscribe to the confirmation dialog closed action
        confirmation.afterClosed().subscribe((result) =>
        {
            // If the confirm button pressed...
            if ( result === 'confirmed' )
            {
                // Get the current meal's id
                const id = this.mealCh.id;

                // Delete the contact
                this._mealService.deleteCategory(id)
                    .subscribe((isDeleted) =>
                    {
                        // Return if the contact wasn't deleted...
                        if ( !isDeleted )
                        {
                            return;
                        }

                        this._router.navigate(['../../'], {relativeTo: this._activatedRoute});
                        
                        // Toggle the edit mode off
                        this.toggleEditMode(false);
                    });

                // Mark for check
                this._changeDetectorRef.markForCheck();
            }
        });

    }

    addBasket(): void {

        if(this.quantity == 0)
            return;

        this.basketService.basket$.pipe(
            take(1),
            map((basket) => {
                if (basket) {
                    if (!basket.basketItems) {
                        basket.basketItems = [];
                    }
                    let index = basket?.basketItems.findIndex(x => x.mealId == this.mealCh?.id)
                    
                    if(index == -1){   
                        // Create a new instance of BasketItemDto
                        const newItem = new BasketItemDto();
                        newItem.mealId = this.mealCh?.id;
                        newItem.drinkId = undefined;
                        newItem.basketId = basket.id;
                        newItem.quantity = this.quantity;   
                        basket.basketItems.push(newItem);

                    } else {
                        basket.basketItems[index].quantity += this.quantity;
                        basket.basketItems[index].changed = true;
                    }

                    basket.lastUpdated = new Date();
                }
                
                return basket;
            }),
            switchMap((basket) => this.basketService.saveBasket(basket))
        ).subscribe({
            next: () => this.showFlashMessage("success")
        });  
    }
    
    

    increaseItem(){
        this.quantity += 1;
    }

    decreaseItem(){
        if(this.quantity <= 0)
            return;

        this.quantity -= 1;
    }


    showFlashMessage(type: 'success' | 'error'): void
    {
        // Show the message
        this.flashMessage = type;

        // Mark for check
        this._changeDetectorRef.markForCheck();

        // Hide it after 3 seconds
        setTimeout(() =>
        {
            this.flashMessage = null;

            // Mark for check
            this._changeDetectorRef.markForCheck();
        }, 3000);
    }
}
