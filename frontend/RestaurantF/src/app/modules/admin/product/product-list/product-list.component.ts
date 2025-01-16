import { NgIf, NgFor, AsyncPipe, NgClass, I18nPluralPipe } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { FormsModule, ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatDrawer, MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterOutlet, RouterLink, ActivatedRoute, Router } from '@angular/router';
import { ProductDto } from 'NSwag/nswag-api-restaurant';
import { Subject, switchMap, takeUntil, tap } from 'rxjs';
import { ProductService } from '../product.service';
import { FuseMediaWatcherService } from '@fuse/services/media-watcher';
import { UserService } from 'app/core/user/user.service';

@Component({
  standalone: true,
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  encapsulation  : ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports        : [MatSidenavModule, RouterOutlet, NgIf, RouterLink, NgFor, MatButtonModule, MatIconModule, MatTooltipModule, AsyncPipe, FormsModule
    ,RouterOutlet, NgIf, MatFormFieldModule, MatIconModule, MatInputModule, FormsModule, ReactiveFormsModule, MatButtonModule, NgFor, NgClass, RouterLink, AsyncPipe, I18nPluralPipe
  ]
})
export class ProductListComponent implements OnInit, OnDestroy {
  
  @ViewChild('matDrawer', {static: true}) matDrawer: MatDrawer;
      searchInputControl: UntypedFormControl = new UntypedFormControl();
      drawerMode: 'side' | 'over';
      selectedProduct: ProductDto;
      meals: ProductDto[];
      private _unsubscribeAll: Subject<any> = new Subject<any>();
  
      /**
       * Constructor
       */
      constructor(
          private _activatedRoute: ActivatedRoute,
          private _changeDetectorRef: ChangeDetectorRef,
          private _router: Router,
          public _productService: ProductService,
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
          this._productService.search = ""
          // Get the items
          this._productService.products$
              .pipe(takeUntil(this._unsubscribeAll))
              .subscribe((products: ProductDto[]) =>
              {
                  this._productService.products = products;
                  // Mark for check
                  this._changeDetectorRef.markForCheck();
              });
  
          // Get the item
          this._productService.product$
              .pipe(takeUntil(this._unsubscribeAll))
              .subscribe((product: ProductDto) =>
              {
                  this.selectedProduct = product;
  
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
              tap((q) => { this._productService.search = q }),
              switchMap(query => 
                  // Search
                  this._productService.getItems(query),
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
  
      addProduct() {
          this._productService.createProduct().subscribe((newProduct) => {
              
              this._router.navigate(['details', newProduct.id], {relativeTo: this._activatedRoute});
  
              this._productService.editMode = !this._productService.editMode;
          });
  
      }
}
