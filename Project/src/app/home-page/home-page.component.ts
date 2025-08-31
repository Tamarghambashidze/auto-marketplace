import { Component, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { HeroComponent } from "../hero/hero.component";
import { ServiceComponent } from "../service/service.component";
import { AboutComponent } from "../about/about.component";
import { WorkComponent } from "../work/work.component";

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [HeroComponent, ServiceComponent, AboutComponent, WorkComponent],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements AfterViewInit {

  constructor(private router: Router) { }

  ngAfterViewInit() {
    const observer = new IntersectionObserver(
      (entries) => {
        entries.forEach(entry => {
          if (entry.isIntersecting) {
            const sectionId = entry.target.id;

            if (sectionId === 'hero') {
              this.router.navigate(['/']);
            } 
            else if (sectionId === 'services') {
              this.router.navigate(['/Services']);
            } 
            else if (sectionId === 'about') {
              this.router.navigate(['/About']);
            } 
            else if (sectionId === 'work') {
              this.router.navigate(['/Work']);
            }
          }
        });
      },
      { threshold: 0.5 }
    );

    observer.observe(document.getElementById('hero')!);
    observer.observe(document.getElementById('services')!);
    observer.observe(document.getElementById('about')!);
    observer.observe(document.getElementById('work')!);
  }
}
