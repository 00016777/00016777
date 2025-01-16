import { Injectable } from "@angular/core";
import { environemnt } from "environments/environment";
import { BasketItemDto, FileParameter, MealClient, MealDto, MealDtos } from "NSwag/nswag-api-restaurant";
import { BehaviorSubject, filter, map, Observable, of, switchMap, take, tap, throwError } from "rxjs";

@Injectable({providedIn: 'root'})
export class MealService{

    private _meal: BehaviorSubject<MealDto | null> = new BehaviorSubject(null);
    private _meals: BehaviorSubject<MealDtos | null> = new BehaviorSubject(null);
    private _categories: BehaviorSubject<MealDto[] | null> = new BehaviorSubject(null);
    public meals: MealDtos;
    search: string = "";
    editMode: boolean = false;
    categories: MealDto[];
    basketItems: BasketItemDto[];
    url: string;

    /**
     *
     */
    constructor(private mealClient: MealClient) {
    }

    // -----------------------------------------------------------------------------------------------------
    // @ Accessors
    // -----------------------------------------------------------------------------------------------------

    // Accessors
    get meals$(): Observable<MealDtos> {
        return this._meals.asObservable();
    }

    get meal$(): Observable<MealDto> {
        return this._meal.asObservable();
    }

    get editMode$(): boolean{
        return this.editMode;
    }

    get categories$(){
        return this._categories.asObservable();
    }

    /**
     * Get items
     */
    getItems(parentId: string | null = null, search: string | ""): Observable<MealDto[]>
    {
        return this.mealClient.getMealAndChildren(+parentId, search).pipe(
            tap((response: any) =>
            {
                this._meals.next(response);
            }),
        );
    }

    /**
     * Get item by id
     */
    getItemById(id: string): Observable<MealDto>
    {
        return this._meals.pipe(
            take(1),
            map((meals) =>
            {
                // Find within the folders and files
                const meal = [...meals.meals, ...meals.mealChildren].find(value => value.id === +id) || null;

                // Update the item
                this._meal.next(meal);

                // Return the item
                return meal;
            }),
            switchMap((meal) =>
            {
                if ( !meal )
                {
                    return throwError('Could not found the item with id of ' + id + '!');
                }

                return of(meal);
            }),
        );
    }

    /**
     * Update the images of the given meal
     *
     * @param id
     * @param avatar
     */
    uploadImages(id: number, images: File[]): Observable<MealDto>
    {
        return this._meals.pipe(
            take(1),
            switchMap(meals => this.mealClient.uploadImagesOfMeal(id, this.convertToFileParameters(images))
            .pipe(
                map((updatedMealDto) =>
                {
                    // Find the index of the updated contact
                    const indexMeal = this.meals?.meals.findIndex(item => item.id === id);
                    const indexChildren = this.meals?.mealChildren.findIndex(item => item.id === id);

                    // Update the contact
                    meals.meals[indexMeal] = updatedMealDto;
                    meals.mealChildren[indexChildren] = updatedMealDto;

                    // Update the contacts
                    this._meals.next(meals);

                    // Return the updated contact
                    return updatedMealDto;
                }),
                switchMap(updatedMealDto => this.meal$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._meal.next(updatedMealDto);

                        // Return the updated contact
                        return updatedMealDto;
                    }),
                )),
            )),
        );
    }

    deleteMealImages(id: number){
        return this._meals.pipe(
            take(1),
            switchMap(meals => this.mealClient.deleteMealImages(id)
            .pipe(
                map((updatedMealDto) =>
                {
                    // Find the index of the updated contact
                    const indexMeal = this.meals?.meals.findIndex(item => item.id === id);
                    const indexChildren = this.meals?.mealChildren.findIndex(item => item.id === id);

                    // Update the contact
                    meals.meals[indexMeal] = updatedMealDto;
                    meals.mealChildren[indexChildren] = updatedMealDto;

                    // Update the contacts
                    this._meals.next(meals);

                    // Return the updated contact
                    return updatedMealDto;
                }),
                switchMap(updatedMealDto => this.meal$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._meal.next(updatedMealDto);

                        // Return the updated contact
                        return updatedMealDto;
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
     * Create meal
     */
    createMeal(): Observable<MealDto>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.createMeal({id: 0, name: '', description: '', isCategory: false} as MealDto).pipe(
                map((newMeal) =>
                {
                    // Update the meals with the new meal
                    this.meals.mealChildren = [newMeal, ...this.meals?.mealChildren]
                    this._meals.next(this.meals);

                    // Return the new meal
                    return newMeal;
                }),
            )),
        );
    }

    /**
     * Create category
     */
    createCategory(): Observable<MealDto>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.createMeal({id: 0, name: '', description: '', isCategory: true} as MealDto).pipe(
                map((newCategory) =>
                {
                    // Update the meals with the new meal
                    this.meals.meals = [newCategory, ...this.meals?.meals]
                    this._meals.next(this.meals);

                    if(newCategory?.parentId)
                        // Get the items
                        this.mealClient.getMealAndChildren(newCategory.parentId,"")
                            .subscribe((meals) => {
                                this.meals = meals
                        });

                    // Return the new meal
                    return newCategory;
                }),
            )),
        );
    }

    /**
     * Update meal
     *
     * @param id
     * @param meal
     */
    updateMeal(id: number, meal: MealDto): Observable<MealDto>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.updateMeal(meal).pipe(
                map((updatedMeal) =>
                {
                    // Find the index of the updated contact
                    const index = this.meals?.mealChildren.findIndex(item => item.id === id);

                    // Update the contact
                    this.meals.mealChildren[index] = updatedMeal;

                    // Update the contacts
                    this._meals.next(this.meals);

                    // Return the updated contact
                    return updatedMeal;
                }),
                switchMap(updatedMeal => this.meal$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._meal.next(updatedMeal);

                        // Return the updated contact
                        return updatedMeal;
                    }),
                )),
            )),
        );
    }

    /**
     * Update category
     *
     * @param id
     * @param category
     */
    updateCategory(id: number, category: MealDto): Observable<MealDto>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.updateMeal(category).pipe(
                map((updatedCategory) =>
                {
                    // Find the index of the updated category
                    const index = this.meals?.meals.findIndex(item => item.id === id);

                    // Update the category
                    this.meals.meals[index] = updatedCategory;

                    // Update the categories
                    this._meals.next(this.meals);

                    if(updatedCategory?.parentId)
                    // Get the items
                    this.mealClient.getMealAndChildren(updatedCategory.parentId,"")
                        .subscribe((meals) => {
                            this.meals = meals
                    });

                    // Return the updated category
                    return updatedCategory;
                }),
                switchMap(updatedCategory => this.meal$.pipe(
                    take(1),
                    filter(item => item && item.id === id),
                    tap(() =>
                    {
                        // Update the contact if it's selected
                        this._meal.next(updatedCategory);

                        // Return the updated contact
                        return updatedCategory;
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
    deleteMeal(id: number): Observable<boolean>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.deleteMeal(id).pipe(
                map((isDeleted: boolean) =>
                {
                    // Find the index of the deleted meal
                    const index = this.meals?.mealChildren.findIndex(item => item.id === id);

                    // Delete the meal
                    this.meals.mealChildren.splice(index, 1);

                    // Update the meals
                    this._meals.next(this.meals);

                    // Return the deleted status
                    return isDeleted;
                }),
            )),
        );
    }

    /**
     * Delete the category
     *
     * @param id
     */
    deleteCategory(id: number): Observable<boolean>
    {
        return this.meals$.pipe(
            take(1),
            switchMap(meals => this.mealClient.deleteMeal(id).pipe(
                map((isDeleted: boolean) =>
                {
                    // Find the index of the deleted meal
                    const index = this.meals?.meals.findIndex(item => item.id === id);

                    // Delete the meal
                    this.meals.meals.splice(index, 1);

                    // Update the meals
                    this._meals.next(this.meals);

                    // Return the deleted status
                    return isDeleted;
                }),
            )),
        );
    }

    /**
     * 
     */
    getCategories(){
        return this.mealClient.getCategories().subscribe((categories) => {
            if(categories)
                this._categories.next(categories)
        })
    }

    getMealUrl(fileName){
        return environemnt.API_BASE_URL + '/files/meals/' + fileName
    }
}