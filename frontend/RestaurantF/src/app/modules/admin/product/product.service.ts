import { Injectable } from "@angular/core";
import { environemnt } from "environments/environment";
import { BasketItemDto, FileParameter, Filter, MealClient, MealDto, MealDtos, ProductDto, ProductsClient } from "NSwag/nswag-api-restaurant";
import { BehaviorSubject, filter, map, Observable, of, switchMap, take, tap, throwError } from "rxjs";

@Injectable({providedIn: 'root'})
export class ProductService{

    private _product: BehaviorSubject<ProductDto | null> = new BehaviorSubject(null);
    private _products: BehaviorSubject<ProductDto[] | null> = new BehaviorSubject(null);
    public products: ProductDto[];
    search: string = "";
    editMode: boolean = false;
    basketItems: BasketItemDto[];
    url: string;

    /**
     *
     */
    constructor(private productClient: ProductsClient) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Accessors
    get products$(): Observable<ProductDto[]> {
        return this._products.asObservable();
    }

    get product$(): Observable<ProductDto> {
        return this._product.asObservable();
    }

    get editMode$(): boolean{
        return this.editMode;
    }

    /**
     * Get items
     */
    getItems(search: string | ""): Observable<ProductDto[]>
    {
        return this.productClient.getAllProducts({filters: JSON.stringify({name: {value: search}}), first: 0, rows: 100, sortField: "", sortOrder: 1} as Filter).pipe(
            tap((response: any) =>
            {
                this._products.next(response);
            }),
        );
    }

    /**
     * Get item by id
     */
    getItemById(id: string): Observable<MealDto>
    {
        return this._products.pipe(
            take(1),
            map((products) =>
            {
                // Find within the folders and files
                const product = [...products].find(value => value.id === +id) || null;

                // Update the item
                this._product.next(product);

                // Return the item
                return product;
            }),
            switchMap((product) =>
            {
                if ( !product )
                {
                    return throwError('Could not found the item with id of ' + id + '!');
                }

                return of(product);
            }),
        );
    }

    /**
     * Update the images of the given meal
     *
     * @param id
     * @param avatar
     */
    uploadImages(id: number, images: File[]): Observable<ProductDto>
    {
        return this._products.pipe(
            take(1),
            switchMap(products => this.productClient.uploadImagesOfProduct(id, this.convertToFileParameters(images))
            .pipe(
                map((updatedProduct) =>
                {
                    // Find the index of the updated contact
                    const indexProduct = this.products?.findIndex(item => item.id === id);

                    // Update the contact
                    products[indexProduct] = updatedProduct;

                    // Update the contacts
                    this._products.next(products);

                    // Return the updated contact
                    return updatedProduct;
                }),
                switchMap(updatedProduct => this.product$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._product.next(updatedProduct);

                        // Return the updated contact
                        return updatedProduct;
                    }),
                )),
            )),
        );
    }

    deleteProductImages(id: number){
        return this._products.pipe(
            take(1),
            switchMap(products => this.productClient.deleteProductImages(id)
            .pipe(
                map((updatedProduct) =>
                {
                    // Find the index of the updated product
                    const indexProduct = this.products?.findIndex(item => item.id === id);

                    // Update the product
                    products[indexProduct] = updatedProduct;

                    // Update the products
                    this._products.next(products);

                    // Return the updated product
                    return updatedProduct;
                }),
                switchMap(updatedProduct => this.product$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._product.next(updatedProduct);

                        // Return the updated contact
                        return updatedProduct;
                    }),
                )),
            )),
        );
    }

    convertToFileParameters(files: File[]): FileParameter[] {
        return files.map(file => ({
          data: file,
          fileName: file.name,
        }));
      }

      /**
     * Create product
     */
    createProduct(): Observable<MealDto>
    {
        return this.products$.pipe(
            take(1),
            switchMap(products => this.productClient.createProduct({id: 0, name: '', description: '', pricePerKG: 0} as ProductDto).pipe(
                map((newProduct) =>
                {
                    // Update the meals with the new meal
                    this.products = [newProduct, ...this.products]
                    this._products.next(this.products);

                    // Return the new meal
                    return newProduct;
                }),
            )),
        );
    }

    /**
     * Update product
     *
     * @param id
     * @param product
     */
    updateProduct(id: number, product: ProductDto): Observable<ProductDto>
    {
        return this.products$.pipe(
            take(1),
            switchMap(products => this.productClient.updateProduct(product).pipe(
                map((updatedProduct) =>
                {
                    // Find the index of the updated product
                    const index = this.products?.findIndex(item => item.id === id);

                    // Update the product
                    this.products[index] = updatedProduct;

                    // Update the products
                    this._products.next(this.products);

                    // Return the updated product
                    return updatedProduct;
                }),
                switchMap(updatedProduct => this.product$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the product if it's selected
                        this._product.next(updatedProduct);

                        // Return the updated product
                        return updatedProduct;
                    }),
                )),
            )),
        );
    }

    /**
     * Delete the meal
     *
     * @param id
     */
    deleteProduct(id: number): Observable<boolean>
    {
        return this.products$.pipe(
            take(1),
            switchMap(products => this.productClient.deleteProduct(id).pipe(
                map((isDeleted: boolean) =>
                {
                    // Find the index of the deleted meal
                    const index = this.products?.findIndex(item => item.id === id);

                    // Delete the meal
                    this.products.splice(index, 1);

                    // Update the meals
                    this._products.next(this.products);

                    // Return the deleted status
                    return isDeleted;
                }),
            )),
        );
    }

    getProductUrl(fileName){
        return environemnt.API_BASE_URL + '/files/products/' + fileName
    }
}