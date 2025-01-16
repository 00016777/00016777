import { MealDto } from "NSwag/nswag-api-restaurant";

export interface MealDtos {
    meals: MealDto[];
    mealChildren: MealDto[];
    path: any[];
  }
