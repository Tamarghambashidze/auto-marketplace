import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { CarDetailsService } from '../shared/services/car-details.service';
import { CardsAreaComponent } from "../cards-area/cards-area.component";
import { Car } from '../shared/models/car.model';
import { CarService } from '../shared/services/car.service';
import { SortCarsService } from '../shared/services/sort-cars.service';
import { FormsModule } from '@angular/forms';
import { SortCarsComponent } from "../sort-cars/sort-cars.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-catalog',
  standalone: true,
  imports: [CommonModule, HttpClientModule, CardsAreaComponent, FormsModule, SortCarsComponent, RouterLink],
  templateUrl: './catalog.component.html',
  styleUrls: ['./catalog.component.css']
})
export class CatalogComponent {
  public manufacturers: string[] = [];
  public transmissions: string[] = [];
  public fueltypes: string[] = [];
  public drivetrains: string[] = [];

  public selectedManufacturer: string = '';
  public selectedTransmission: string = '';
  public selectedDrivetrain: string = '';
  public selectedFuelType: string = '';

  public searchQuery: string = '';

  public carList: Car[] = [];
  public filteredCars: Car[] = [];

  // Flag to control visibility of the sort-cars component
  public isFiltersActive: boolean = false;

  constructor(
    private carDetailsService: CarDetailsService,
    private carService: CarService,
    private sortCarsService: SortCarsService,
  ) {
    this.loadCarDetails();
    this.loadCarService();
  }

  private loadCarDetails(): void {
    this.carDetailsService.getManufacturers().subscribe({
      next: (data) => this.manufacturers = data,
      error: (err) => console.error('Error loading manufacturers:', err),
    });

    this.carDetailsService.getTransmissions().subscribe({
      next: (data) => this.transmissions = data,
      error: (err) => console.error('Error loading transmissions:', err),
    });

    this.carDetailsService.getFuelTypes().subscribe({
      next: (data) => this.fueltypes = data,
      error: (err) => console.error('Error loading fuel types:', err),
    });

    this.carDetailsService.getDriveTrains().subscribe({
      next: (data) => this.drivetrains = data,
      error: (err) => console.error('Error loading drivetrains:', err),
    });
  }

  private loadCarService(): void {
    this.carService.getAllCars().subscribe({
      next: (data) => {
        this.carList = data.body || [];
        this.filteredCars = [...this.carList]; // Initially, display all cars
      },
      error: (err) => console.error('Error loading cars:', err),
    });
  }

  // Method to reset filters (except the one being changed)
  resetOtherFilters(except: string): void {
    if (except !== 'manufacturer') this.selectedManufacturer = '';
    if (except !== 'transmission') this.selectedTransmission = '';
    if (except !== 'drivetrain') this.selectedDrivetrain = '';
    if (except !== 'fuelType') this.selectedFuelType = '';
  }

  // Method to filter cars based on selected criteria
  public filterCars(): void {
    let filtered = this.carList;

    if (this.selectedManufacturer) {
      filtered = filtered.filter(car => car.manufacturer === this.selectedManufacturer);
    }

    if (this.selectedTransmission) {
      filtered = filtered.filter(car => car.details?.transmission === this.selectedTransmission);
    }

    if (this.selectedDrivetrain) {
      filtered = filtered.filter(car => car.details?.driveTrain === this.selectedDrivetrain);
    }

    if (this.selectedFuelType) {
      filtered = filtered.filter(car => car.details?.fuelType === this.selectedFuelType);
    }

    this.filteredCars = filtered;

    // Check if filters are applied
    this.isFiltersActive = !!(this.selectedManufacturer || this.selectedTransmission || this.selectedDrivetrain || this.selectedFuelType);
  }

  // Method to sort cars by price
  public sortCarsByPrice(order: 'ascending' | 'descending'): void {
    if (order === 'ascending') {
      this.sortCarsService.orderByAscending().subscribe({
        next: (data) => {
          this.filteredCars = data;
        },
        error: (err) => console.error('Error sorting by price ascending:', err),
      });
    } else {
      this.sortCarsService.orderByDescending().subscribe({
        next: (data) => {
          this.filteredCars = data;
        },
        error: (err) => console.error('Error sorting by price descending:', err),
      });
    }
  }

  // Search function
  public searchCar(): void {
    this.isFiltersActive = true;
    this.carService.searchCar(this.searchQuery).subscribe({
      next: (data) => {
        this.filteredCars = data.body || [];
        console.log(this.filteredCars);
      },
      error: (err) => console.error('Error searching car:', err),
    });
    this.searchQuery = ''; // Clear the search query after search
  }

  // Reset a specific filter
  resetSelection(event: Event, filterType: string): void {
    this.resetOtherFilters(filterType);
    this.filterCars(); // Reapply filtering after reset
  }

  public resetAllFilters(): void {
    this.selectedManufacturer = '';
    this.selectedTransmission = '';
    this.selectedDrivetrain = '';
    this.selectedFuelType = '';
    this.searchQuery = '';
    this.filterCars();
  }
  
}
