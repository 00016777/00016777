import { TextFieldModule } from '@angular/cdk/text-field';
import { NgIf, AsyncPipe, NgFor, NgClass, DatePipe } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule, MatOptionModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { BasketItemDto, MealDto, ProductDto } from 'NSwag/nswag-api-restaurant';
import { map, Subject, switchMap, take, takeUntil } from 'rxjs';
import { ProductListComponent } from '../product-list/product-list.component';
import { FuseConfirmationService } from '@fuse/services/confirmation';
import { ProductService } from '../product.service';
import { MatDrawerToggleResult } from '@angular/material/sidenav';
import { FuseAlertService } from '@fuse/components/alert';
import { UserService } from 'app/core/user/user.service';
import { MessagesService } from 'app/layout/common/messages/messages.service';
import { BasketService } from '../../basket/basket.service';

@Component({
  standalone: true,
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports        : [NgIf, AsyncPipe, MatButtonModule, RouterLink, MatIconModule, NgFor, FormsModule, ReactiveFormsModule, MatRippleModule, MatFormFieldModule, MatInputModule, MatCheckboxModule, NgClass, MatSelectModule, MatOptionModule, TextFieldModule, DatePipe],

})
export class ProductDetailComponent {

  @ViewChild('imagesFileInput') private _imagesFileInput: ElementRef;
  
      product: ProductDto;
      productForm: UntypedFormGroup;
      private _unsubscribeAll: Subject<any> = new Subject<any>();
      quantity: number = 0;
      basketItem: BasketItemDto;
      flashMessage: 'success' | 'error' | null = null;
  
      /**
       * Constructor
       */
      constructor(
          private _changeDetectorRef: ChangeDetectorRef,
          private _productListComponent: ProductListComponent,
          private _fuseConfirmationService: FuseConfirmationService,
          public _productService: ProductService,
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
          this._productListComponent.matDrawer.open();
  
          this.productForm = this._formBuilder.group({
              id          : [Number],
              name        : ['', [Validators.required]],
              images      : [[]],
              pricePerKG  : [Number],
              description : [null],
          });
  
          // Get the item
          this._productService.product$
              .pipe(takeUntil(this._unsubscribeAll))
              .subscribe((product: ProductDto) =>
              {
                  // Open the drawer in case it is closed
                  this._productListComponent.matDrawer.open();
  
                  // Get the item
                  this.product = product;
  
                  this.productForm.patchValue({
                      id: product.id,
                      name: product.name,
                      pricePerKG: product?.pricePerKG,
                      description: product?.description,
                      images: product?.images,
                  });
  
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
       * Close the drawer
       */
      closeDrawer(): Promise<MatDrawerToggleResult>
      {
          return this._productListComponent.matDrawer.close();
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
              this._productService.editMode = !this._productService.editMode;
          }
          else
          {
              this._productService.editMode = editMode;
          }
  
          // Mark for check
          this._changeDetectorRef.markForCheck();
      }
  
      getProductParentName(productId: number){
          return this._productService.products.find(x => x.id == productId)?.name;
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
          this._productService.uploadImages(this.product.id, files).subscribe();
      }
  
      /**
       * Remove the image
       */
      removeImages(): void
      {
          this._productService.deleteProductImages(this.product.id).subscribe();
          // Get the form control for 'image'
          const imagesFormControl = this.productForm.get('images');
  
          // Set the images as null
          imagesFormControl.setValue(null);
  
          // Set the file input value as null
          this._imagesFileInput.nativeElement.value = null;
  
          // Update the meal
          this.product.images = null;
      }
  
      /**
       * Update the product
       */
    updateProduct(): void
    {
        // Get the product object
        const meal = this.productForm.getRawValue();

        // Update the product on the server
        this._productService.updateProduct(meal.id, meal).subscribe(() =>
        {
            // Toggle the edit mode off
            this.toggleEditMode(false);
        });
    }
  
      /**
       * Delete the product
       */
      deleteProduct(): void
      {
          // Open the confirmation dialog
          const confirmation = this._fuseConfirmationService.open({
              title  : 'Delete product',
              message: 'Are you sure you want to delete this product? This action cannot be undone!',
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
                  // Get the current product's id
                  const id = this.product.id;
  
                  // Delete the contact
                  this._productService.deleteProduct(id)
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

                    let index = basket?.basketItems.findIndex(x => x.productId == this.product?.id)
                    
                    if(index == -1){
                        // Create a new instance of BasketItemDto
                        const newItem = new BasketItemDto();
                        newItem.productId = this.product?.id;
                        newItem.drinkId = undefined;
                        newItem.mealId = undefined;
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
        if( this.quantity <= 0 )
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
