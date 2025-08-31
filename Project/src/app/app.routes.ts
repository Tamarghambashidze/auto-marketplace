import { Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { AboutComponent } from './about/about.component';
import { ServiceComponent } from './service/service.component';
import { WorkComponent } from './work/work.component';
import { CatalogComponent } from './catalog/catalog.component';
import { CarDetailsComponent } from './car-details/car-details.component';
import { SortCarsComponent } from './sort-cars/sort-cars.component';
import { AuthorizationComponent } from './authorization/authorization.component';
import { LogInComponent } from './log-in/log-in.component';
import { ProfileComponent } from './profile/profile.component';
import { SideSectionComponent } from './side-section/side-section.component';

export const routes: Routes = [
    { 
        path: '', 
        component: HomePageComponent,
        children:[
            {path:'About', component:AboutComponent},
            {path:'Services', component:ServiceComponent},
            {path:'Work', component:WorkComponent}
        ] 
    },
    {path:'Catalog', 
        component: CatalogComponent,
        children:[
            {path: ':value', component:SortCarsComponent},
        ]
    },
    {path:'Catalog/Car/:id', component:CarDetailsComponent},
    {path:'registration', component:AuthorizationComponent},
    {path: 'log-in', component:LogInComponent},
    {
        path:'Profile/:id', 
        component:ProfileComponent, 
    },
    {path:'user/:id/:value', component:SideSectionComponent},
    { path: '**', redirectTo: '', pathMatch:'full' }  
];
