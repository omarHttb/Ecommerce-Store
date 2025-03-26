import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../shared/models/products';
import { pagination } from '../shared/models/pagination';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api';
  private http = inject(HttpClient);
  types: string[] = [];
  brands: string[] = [];

  constructor() {}

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();
    if (shopParams.brands && shopParams.brands.length > 0) {
      params = params.append('brands', shopParams.brands.join(','));
    }

    if (shopParams.types && shopParams.types.length > 0) {
      params = params.append('types', shopParams.types.join(','));
    }

    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort);
    }

    params = params.append('PageSize', shopParams.pageSize.toString());
    params = params.append('pageIndex', shopParams.pageNumber.toString());

    console.log(params);

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    return this.http.get<pagination<Product>>(this.baseUrl + '/products', {
      params,
    });
  }

  getTypes() {
    if (this.types.length > 0) return;
    return this.http.get<string[]>(this.baseUrl + '/products/types').subscribe({
      next: (response) => (this.types = response),
      error: (error) => console.log(error),
    });
  }

  getBrands() {
    if (this.brands.length > 0) return;
    return this.http
      .get<string[]>(this.baseUrl + '/products/brands')
      .subscribe({
        next: (response) => (this.brands = response),
        error: (error) => console.log(error),
      });
  }
}
