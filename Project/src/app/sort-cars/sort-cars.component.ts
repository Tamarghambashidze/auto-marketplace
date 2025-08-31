import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Car } from '../shared/models/car.model';

@Component({
  selector: 'app-sort-cars',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './sort-cars.component.html',
  styleUrl: './sort-cars.component.css'
})
export class SortCarsComponent {
    @Input() cars: Car[] = []; 
    constructor(){ }
}
