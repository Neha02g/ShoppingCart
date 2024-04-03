import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-hero',
  templateUrl: './hero.component.html',
  styleUrls: ['./hero.component.css']
})
export class HeroComponent {
[x: string]: any;
query:string = "Coord";

constructor(private router:Router){}

searchProducts(query: string) {
  // Navigate to the search route with the query parameter
  this.router.navigate(['/search'], { queryParams: { q: query } });
}
}
