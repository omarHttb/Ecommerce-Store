import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/products';
import { pagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  title = 'Frontend';
  products: Product[] = [];

  ngOnInit(): void {
    this.http.get<pagination<Product>>(this.baseUrl + '/products').subscribe({
      next: (response) => (this.products = response.data),
      error: (error) => console.log(error),
      complete: () => console.log('Request has completed'),
    });
  }
}
