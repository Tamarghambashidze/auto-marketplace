import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { BuyCarService } from '../shared/services/buy-car.service';
import { FavouritesService } from '../shared/services/favourites.service';
import { Car } from '../shared/models/car.model';
import { Transaction } from '../shared/models/transaction.model';
import { CommonModule } from '@angular/common';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-side-section',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './side-section.component.html',
  styleUrl: './side-section.component.css'
})
export class SideSectionComponent implements OnInit {
  public cars: Car[] = [];
  public transactions: Transaction[] = [];
  public value: string = '';

  constructor(
    private route: ActivatedRoute, 
    private buyCarService: BuyCarService, 
    private favouritesService: FavouritesService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (isNaN(id) || id <= 0) {
      console.error("Invalid user ID");
      return;
    }

    this.value = this.route.snapshot.paramMap.get('value') || ''; 

    if (this.value === "Favourites") {
      this.favouritesService.getUserFavourites(id)
        .pipe(take(1))
        .subscribe({
          next: (res) => this.cars = res,
          error: (err) => console.error("Error fetching favourites:", err)
        });
    } else if (this.value === "Transactions") {
      this.buyCarService.getTransactions(id)
        .pipe(take(1))
        .subscribe({
          next: (res) => this.transactions = res.body || [],
          error: (err) => console.error("Error fetching transactions:", err)
        });
    }

  }
}
