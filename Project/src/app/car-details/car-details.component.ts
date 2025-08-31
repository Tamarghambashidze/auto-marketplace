import { Component, OnInit } from '@angular/core';
import { CarService } from '../shared/services/car.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from '../shared/models/car.model';
import { CommonModule } from '@angular/common';
import { BuyCarService } from '../shared/services/buy-car.service';
import { FavouritesService } from '../shared/services/favourites.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-car-details',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './car-details.component.html',
  styleUrl: './car-details.component.css'
})
export class CarDetailsComponent implements OnInit {
  public car: Car | null = null;
  public isLoading: boolean = true;
  public errorMessage: string | null = null;

  constructor(
    private carService: CarService,
    private route: ActivatedRoute,
    private router: Router,
    private buyCarService:BuyCarService,
    private favouritesService:FavouritesService,
    private toastrService:ToastrService,
  ) {}

  buyCar(carId: number | undefined): void {
    const item = localStorage.getItem('token');
    
    if (item !== null) {
      const user = JSON.parse(item);
      this.buyCarService.buyCar(user.id, carId || 0).subscribe();
      this.toastrService.success("Successfully bought car");
      this.router.navigate(['/']);
    } else {
      this.toastrService.error("You need to log in first.");
    }
  }
  addToFavourites(carId: number | undefined): void {
    const item = localStorage.getItem('token');
    
    if (item !== null) {
      const user = JSON.parse(item);
      
      this.favouritesService.addCarToFavourites(user.id, carId || 0).subscribe(
        (response) => {
          this.toastrService.success("Successfully bought car");
          this.router.navigate(['/']);
        },
        (error) => {
          this.toastrService.error("Error while adding car to favourites: " + error.message);
        }
      );
    } else {
      this.toastrService.error("You need to log in first.");
    }
  }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const carId = params.get('id');
      const id = Number(carId);

      if (!id || isNaN(id)) {
        this.errorMessage = 'Invalid car ID provided.';
        this.isLoading = false;
        return;
      }

      this.fetchCarDetails(id);
    });
  }

  fetchCarDetails(id: number): void {
    this.carService.getCarById(id).subscribe(
      response => {
        this.car = response.body || null;
        this.isLoading = false;
      },
      error => {
        console.error('Error loading car details:', error);
        this.errorMessage = 'Error fetching car details. Please try again later.';
        this.isLoading = false;
      }
    );
  }

  goBack(): void {
    this.router.navigate(['/Catalog']);
  }

  currentIndex: number = 0; 

  changeImage(direction: number): void {
    const totalImages = 2; 
    this.currentIndex += direction;
    if (this.currentIndex >= totalImages) {
      this.currentIndex = 0;
    } else if (this.currentIndex < 0) {
      this.currentIndex = totalImages - 1;
    }
  }
}
