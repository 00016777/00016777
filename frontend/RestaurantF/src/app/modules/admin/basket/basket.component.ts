import { Component, ViewEncapsulation } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
    selector     : 'basket',
    standalone   : true,
    templateUrl  : './basket.component.html',
    encapsulation: ViewEncapsulation.None,
    imports: [RouterOutlet]
})
export class BasketComponent
{
    /**
     * Constructor
     */
    constructor()
    {
    }
}
