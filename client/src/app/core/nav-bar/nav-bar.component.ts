import { Observable } from 'rxjs';
import { Basket, IBasket } from './../../shared/models/basket';
import { BasketService } from './../../basket/basket.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {
  Basket$: Observable<IBasket>;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    this.Basket$ = this.basketService.basket$;
  }

}
