import { Component, OnInit } from '@angular/core';
import { Car } from '../shared/models/car.model';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CarService } from '../shared/services/car.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cards-area',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './cards-area.component.html',
  styleUrls: ['./cards-area.component.css']
})
export class CardsAreaComponent implements OnInit {
  cars: Car[] = [];
  searchQuery: string = '';
  currentPage: number = 1;
  pageSize: number = 10;
  totalPages: number = 1;
  totalCars: number = 0; // To store total number of cars from the backend

  constructor(
    private carService: CarService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.searchQuery = params['query'] || '';
      this.currentPage = +params['page'] || 1;
      this.pageSize = +params['pageSize'] || 10;
      this.fetchCars();
    });
  }

  fetchCars(): void {
    this.carService.getCarPaginated(this.currentPage, this.pageSize).subscribe(
      (data) => {
        this.cars = data.body || []; 
        this.totalPages = Math.ceil(this.totalCars / this.pageSize); 
      },
      (error) => {
        console.error('Error fetching cars:', error);
      }
    );

    this.carService.getAllCars().subscribe({
      next: res => this.totalCars = res.body?.length || 0
    })
  }

  nextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.router.navigate(['/Catalog'], {
        queryParams: { page: this.currentPage, query: this.searchQuery, pageSize: this.pageSize }
      });
      this.fetchCars();
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.router.navigate(['/Catalog'], {
        queryParams: { page: this.currentPage, query: this.searchQuery, pageSize: this.pageSize }
      });
      this.fetchCars();
    }
  }

  onPageSizeChange(): void {
    this.currentPage = 1;
    this.router.navigate(['/Catalog'], {
      queryParams: { page: this.currentPage, query: this.searchQuery, pageSize: this.pageSize }
    });
    this.fetchCars();
  }
}
